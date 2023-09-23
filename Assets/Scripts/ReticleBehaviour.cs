using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;
using System.Linq;

public class ReticleBehaviour : MonoBehaviour
{
    private GameObject child;
    public DrivingSurfaceManager drivingSurfaceManager;
    public ARPlane currentPlane;
    // Start is called before the first frame update
    void Start()
    {
        //Retrieves the first child GameObject of the current GameObject and assigns it to the Child variable
        child = this.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //screenCenter is calculated as the screen position at the center (0.5f, 0.5f) of the camera's viewport.
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();

        drivingSurfaceManager.arRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinBounds);
        currentPlane = null;

        ARRaycastHit? hit = null;

        if(hits.Count > 0)
        {
            var lockedPlane = drivingSurfaceManager.LockedPlane;
            hit = lockedPlane == null ? hits[0] : hits.SingleOrDefault(x => x.trackableId == lockedPlane.trackableId);
        }
        if (hit.HasValue)
        {
            //CurrentPlane is set to the plane associated with the hit's trackable ID.
            currentPlane = drivingSurfaceManager.arPlaneManager.GetPlane(hit.Value.trackableId);
            transform.position = hit.Value.pose.position;
        }
        child.SetActive(currentPlane != null);
    }
}
