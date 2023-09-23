using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DrivingSurfaceManager : MonoBehaviour
{
    public ARPlaneManager arPlaneManager;
    public ARRaycastManager arRaycastManager;
    public ARPlane LockedPlane;
    // Start is called before the first frame update
    void Start()
    {
        arPlaneManager = gameObject.GetComponent<ARPlaneManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //It checks if the LockedPlane is subsumed by another plane (i.e., if it's not a primary plane but part of a larger one).
        //If this is the case, it updates the LockedPlane to be the subsuming (larger) plane. 
        if (LockedPlane?.subsumedBy != null)
        {
            LockedPlane = LockedPlane.subsumedBy;
        }
    }

    public void LockPlane(ARPlane keepPlane)
    {
        var arPlane = keepPlane.GetComponent<ARPlane>();

        foreach(var plane in arPlaneManager.trackables)
        {
            if(plane != arPlane)
            {
                plane.gameObject.SetActive(false);
            }
        }
        LockedPlane = arPlane;
        arPlaneManager.planesChanged += DisableNewPlanes;
    }

    private void DisableNewPlanes(ARPlanesChangedEventArgs args)
    {
        foreach(var plane in args.added)
        {
            plane.gameObject.SetActive(false);
        }
    }
}
