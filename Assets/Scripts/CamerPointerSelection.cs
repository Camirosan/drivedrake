using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamerPointerSelection : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
    [SerializeField] private float maxDistancePointer = 4.5f;
    [Range(0, 1)]
    [SerializeField] private float disPointerObject = 0.95f;
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    private SceneManager SceneManager;
    //private readonly string interactableTag = "interactable";
    private readonly string dragTag1 = "drac1";
    private readonly string dragTag2 = "drac2";
    private readonly string dragTag3 = "drac3";
    private float scaleSize = 0.025f;
    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    private void Start()
    {
        GazeSelect.Instance.OnGazeSelection += GazeSelection;
    }

    private void GazeSelection()
    {
        //_gazedAtObject?.SendMessage("OnPointerClick", null, SendMessageOptions.DontRequireReceiver);
        SceneManager.LoadScene("DrivingDrac");
    }

    public void Update()
    {

        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed
        // at.

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            // GameObject detected in front of the camera.
            if ( _gazedAtObject != hit.transform.gameObject)
            {                
                // New GameObject.
                _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
                _gazedAtObject = hit.transform.gameObject;
                _gazedAtObject.SendMessage("OnPointerEnter", null, SendMessageOptions.DontRequireReceiver);
                GazeSelect.Instance.StartGazeSelection();
            }
            if (hit.transform.CompareTag(dragTag1) || hit.transform.CompareTag(dragTag2) || hit.transform.CompareTag(dragTag3))
            {
                switch (hit.transform.tag)
                {
                    case "drac1":
                        PlayerSelectionManager.instance.SetDracSelection(1);
                        break;
                    case "drac2":
                        PlayerSelectionManager.instance.SetDracSelection(2);
                        break;
                    case "drac3":
                        PlayerSelectionManager.instance.SetDracSelection(3);
                        break;
                    default:
                        break;
                }
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
            _gazedAtObject?.SendMessage("OnPointerExit", null, SendMessageOptions.DontRequireReceiver);
            _gazedAtObject = null;
        }

        // Checks for screen touches.
        if (Google.XR.Cardboard.Api.IsTriggerPressed)
        {
            _gazedAtObject?.SendMessage("OnPointerClick", null, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void PointerOnGaze(Vector3 hitPoint)
    {
        float scaleFactor = scaleSize * Vector3.Distance(transform.position, hitPoint);
        pointer.transform.localScale = Vector3.one * scaleFactor;
        pointer.transform.parent.position = CalculatePointerPosition(transform.position, hitPoint, disPointerObject);
    }

    private void PointerOutGaze()
    {
         pointer.transform.localScale = Vector3.one * 0.1f;
         pointer.transform.parent.transform.localPosition = new Vector3(0, 0, maxDistancePointer);
         pointer.transform.parent.parent.transform.rotation = transform.rotation;
        GazeSelect.Instance.CancelGazeSelection();
    }
    private Vector3 CalculatePointerPosition(Vector3 p0, Vector3 p1, float t)
    {
        float x = p0.x + t * (p1.x - p0.x);
        float y = p0.y + t * (p1.y - p0.y);
        float z = p0.z + t * (p1.z - p0.z);

        return new Vector3(x, y, z);
    }
    
}
