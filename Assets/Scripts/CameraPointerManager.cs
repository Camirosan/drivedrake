using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraPointerManager : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
    [SerializeField] private float maxDistancePointer = 4.5f;
    [Range(0, 1)]
    [SerializeField] private float disPointerObject = 0.95f;
    public GameObject projectile;
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;

    private readonly string interactableTag = "interactable";
    private float scaleSize = 0.025f;
    private bool selecting = false;
    private int dracActive;
    private int maxDracs = 3;
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private float timer;
    private GameObject[] drac = new GameObject[3];
    private void Start()
    {
        GazeManager.Instance.OnGazeSelection += GazeSelection;
        dracActive = PlayerSelectionManager.instance.GetDracSelection();
        Debug.Log($"dragon {dracActive} seleccionado");
        //GameObject.FindGameObjectWithTag($"drac{dracActive}").SetActive(true);
        //drac = GameObject.FindGameObjectWithTag($"drac2");
        for (int i = 0; i < maxDracs; i++)
        {
            drac[i] = GameObject.Find($"drac{i+1}");
            drac[i].SetActive(false);
        }
        //GameObject.Find($"drac{dracActive}").SetActive(true);
        //drac = GameObject.Find($"drac2");
        //drac.SetActive(false);
        drac[dracActive-1].SetActive(true);
    }

    private void GazeSelection()
    {
        //_gazedAtObject?.SendMessage("OnPointerClick", null, SendMessageOptions.DontRequireReceiver);
        //Create proyectile
        //Debug.Log("DESTROY! from camerapointermanager");
        Vector3 position = playerTf.position;
        Quaternion rotation = playerTf.rotation;
        Instantiate(projectile, position, rotation);
    }

    //------------------------------

    // Use this for initialization
    public Transform playerTf;
    Vector3 playerVector;


    [Header("Movement Speed")]
    [Min(0.001f), Tooltip("Fordwars movement speed.")]
    public float TopSpeed;
    private Vector3 movementDirection;
    //------------------------------
    public void Update()
    {

        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.

        timer += Time.deltaTime;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance) && timer >= 3.0f)
        {
            timer = 0;
            // GameObject detected in front of the camera.
            if (hit.transform.gameObject.CompareTag("interactable")) ;// || _gazedAtObject != hit.transform.gameObject)
            {
                
                //Debug.Log($"GameObject detected in front of the camera {hit.point.ToString()}");
                // New GameObject.
                _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnter", null, SendMessageOptions.DontRequireReceiver);
            }
            if (hit.transform.CompareTag(interactableTag) && selecting == false)
            {
                selecting = true;
                PointerOnGaze(hit.point);
                GazeManager.Instance.StartGazeSelection();
            }
            else
            {
                if(selecting == true)
                {
                    selecting = false;
                    PointerOutGaze();
                    //GazeManager.Instance.CancelGazeSelection();
                }
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
            _gazedAtObject = null;
        }

        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            _gazedAtObject?.SendMessage("OnPointerClick", null, SendMessageOptions.DontRequireReceiver);
        }

        //generating movement
        //movementDirection = CalculatePointerPosition(transform.position, hit.point, 1.0f); ;
        //moveXIn(movementDirection.x * TopSpeed);
        //moveZIn(TopSpeed);
        //playerTf.position += ((moveXIn(movementDirection.x) + moveZIn(movementDirection.z) + moveYIn(movementDirection.y)) - playerTf.position) * TopSpeed;
        movementDirection.x = (float)Math.Sin(DegToRad(playerTf.rotation.eulerAngles.y));
        movementDirection.y = -1.0f * (float)Math.Sin(DegToRad(playerTf.rotation.eulerAngles.x));
        movementDirection.z = (float)Math.Cos(DegToRad(playerTf.rotation.eulerAngles.y));
        playerTf.parent.position += (movementDirection * TopSpeed * Time.deltaTime);
        BorderDetection();
        //Debug.Log($"pos = {playerTf.position.ToString()}");
        //Debug.Log($"rot = {playerTf.rotation.eulerAngles.ToString()}");
    }

    private void PointerOnGaze(Vector3 hitPoint)
    {
        /*float scaleFactor = scaleSize * Vector3.Distance(transform.position, hitPoint);
        pointer.transform.localScale = Vector3.one * scaleFactor;
        pointer.transform.parent.position = CalculatePointerPosition(transform.position, hitPoint, disPointerObject);
        Debug.Log("Señalando Camera");*/
    }

    private void PointerOutGaze()
    {
       /* pointer.transform.localScale = Vector3.one * 0.1f;
        pointer.transform.parent.transform.localPosition = new Vector3(0, 0, maxDistancePointer);
        pointer.transform.parent.parent.transform.rotation = transform.rotation;
        GazeManager.Instance.CancelGazeSelection();
        Debug.Log("Deja de señalar camera");*/
    }
    private Vector3 CalculatePointerPosition(Vector3 p0, Vector3 p1, float t)
    {
        float x = p0.x + t * (p1.x - p0.x);
        float y = p0.y + t * (p1.y - p0.y);
        float z = p0.z + t * (p1.z - p0.z);

        return new Vector3(x, y, z);
    }

    //--------------------------------------
    public Vector3 moveXIn(float x)
    {
        return playerVector = new Vector3(x, 0, 0);
    }
    public Vector3 moveZIn(float z)
    {
        return playerVector = new Vector3(0, 0, z);
    }
    public Vector3 moveYIn(float y)
    {
        playerVector = new Vector3(0, y, 0);
        //Debug.Log(playerVector.ToString());
        return playerVector;// = new Vector3(0, y, 0);
    }
    public Vector3 moveOut()
    {
        return playerVector = new Vector3(0, 0, 0);
    }
    public double DegToRad(double value)
    {
        return (float)(value * 3.141516 / 180);
    }
    public void BorderDetection()
    {
        if (playerTf.parent.position.x >= 5)
        {
            playerTf.parent.position = new Vector3(-4.99f, playerTf.parent.position.y, playerTf.parent.position.z);
        }           
        if (playerTf.parent.position.x <= -5)
        {           
            playerTf.parent.position = new Vector3(4.99f, playerTf.parent.position.y, playerTf.parent.position.z);
        }           
        if (playerTf.parent.position.y >= 5.99f)
        {           
            playerTf.parent.position = new Vector3(playerTf.parent.position.x, 0.1f, playerTf.parent.position.z);
        }           
        if (playerTf.parent.position.y <= 0.0f)
        {           
            playerTf.parent.position = new Vector3(playerTf.parent.position.x, 3.99f, playerTf.parent.position.z);
        }           
        if (playerTf.parent.position.z >= 5)
        {           
            playerTf.parent.position = new Vector3(playerTf.parent.position.x, playerTf.parent.position.y, -4.99f);
        }           
        if (playerTf.parent.position.z <= -5)
        {           
            playerTf.parent.position = new Vector3(playerTf.parent.position.x, playerTf.parent.position.y, 4.99f);
        }
    }
    //--------------------------------------
   
}
