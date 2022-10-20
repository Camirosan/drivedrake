using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraPointerManager : MonoBehaviour
{
    //[SerializeField] private GameObject pointer;
    [SerializeField] private float maxDistancePointer = 4.5f;
    [Range(0, 1)]
    [SerializeField] private float disPointerObject = 0.95f;

    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;

    private readonly string interactableTag = "interactable";
    private float scaleSize = 0.025f;
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    /*private void Start()
    {
        GazeManager.Instance.OnGazeSelection += GazeSelection;
    }*/

    private void GazeSelection()
    {
        _gazedAtObject?.SendMessage("OnPointerClick", null, SendMessageOptions.DontRequireReceiver);
    }

    //------------------------------

    // Use this for initialization
    public Transform playerTf;
    Vector3 playerVector;


    [Header("Movement Speed")]
    [Min(0.001f), Tooltip("Top speed attainable when moving forward.")]
    public float TopSpeed;
    private Vector3 movementDirection;
    //------------------------------
    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        { 
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                // New GameObject.
                _gazedAtObject?.SendMessage("OnPointerExit");//, null, SendMessageOptions.DontRequireReceiver);
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnter");//, null, SendMessageOptions.DontRequireReceiver);
                //GazeManager.Instance.StartGazeSelection();
            }
            if (hit.transform.CompareTag(interactableTag))
            {
                PointerOnGaze(hit.point);
            }
            else
            {
                PointerOutGaze();
            }
        }
        else
        {
            // No GameObject detected in front of the camera.
            _gazedAtObject?.SendMessage("OnPointerExit");//, null, SendMessageOptions.DontRequireReceiver);
            _gazedAtObject = null;
        }

        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            _gazedAtObject?.SendMessage("OnPointerClick");//, null, SendMessageOptions.DontRequireReceiver);
        }

        //generating movement
        movementDirection = CalculatePointerPosition(transform.position, hit.point, 1.0f); ;
        // moveXIn(movementDirection.x * TopSpeed);
        //moveZIn(TopSpeed);
        playerTf.position += ((moveXIn(movementDirection.x) + moveZIn(movementDirection.z) + moveYIn(movementDirection.y)) - playerTf.position) * TopSpeed;
        Debug.Log(playerTf.position.ToString());
    }

    private void PointerOnGaze(Vector3 hitPoint)
    {
        float scaleFactor = scaleSize * Vector3.Distance(transform.position, hitPoint);
        //pointer.transform.localScale = Vector3.one * scaleFactor;
        //pointer.transform.parent.position = CalculatePointerPosition(transform.position, hitPoint, disPointerObject);
    }

    private void PointerOutGaze()
    {
        //pointer.transform.localScale = Vector3.one * 0.1f;
        //pointer.transform.parent.transform.localPosition = new Vector3(0, 0, maxDistancePointer);
        //pointer.transform.parent.parent.transform.rotation = transform.rotation;
       // GazeManager.Instance.CancelGazeSelection();
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
    //--------------------------------------
}
