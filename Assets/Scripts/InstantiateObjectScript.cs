using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem.EnhancedTouch;
using System.Collections.Generic;

public class InstantiateObjectScript : MonoBehaviour
{
    public GameObject GameObject;

    public ARPlaneManager planeManager;
    public ARRaycastManager raycastManager;
    private List<ARRaycastHit> ARRaycastHits = new List<ARRaycastHit>();

    private void Awake()
    {
        planeManager = GetComponent<ARPlaneManager>();
        raycastManager = GetComponent<ARRaycastManager>();
    }

    private void OnEnable()
    {
        UnityEngine.InputSystem.EnhancedTouch.TouchSimulation.Enable();
        UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerTouchDetected;

    }

    private void OnDisable()
    {
        UnityEngine.InputSystem.EnhancedTouch.TouchSimulation.Disable();
        UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerTouchDetected;

    }

    void FingerTouchDetected(UnityEngine.InputSystem.EnhancedTouch.Finger fingerTouch)
    {
        if(fingerTouch.index != 0)
        {
            return;
        }

        if(raycastManager.Raycast(fingerTouch.currentTouch.screenPosition, ARRaycastHits, TrackableType.PlaneWithinPolygon))
        {
            foreach(ARRaycastHit hit in ARRaycastHits)
            {
                Pose orientation = hit.pose;
                GameObject spwanedObject = Instantiate(GameObject, orientation.position, orientation.rotation);
            }
        }
    }
}
