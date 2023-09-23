using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    public GameObject CarPrefab;
    public ReticleBehaviour reticleBehaviour;
    public DrivingSurfaceManager drivingSurfaceManager;
    public CarBehaviour carBehaviour;

    private bool WasTapped()
    {
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        if(Input.touchCount == 0)
        {
            return false;
        }

        var touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return false;

        return true;
    }

    private void Update()
    {
        if (CarPrefab ==null && WasTapped() && reticleBehaviour.currentPlane != null)
        {
            var obj = GameObject.Instantiate(CarPrefab);
            carBehaviour = obj.GetComponent<CarBehaviour>();
            carBehaviour.reticleBehaviour = reticleBehaviour;
            carBehaviour.transform.position = reticleBehaviour.transform.position;
            drivingSurfaceManager.LockPlane(reticleBehaviour.currentPlane);
        }
    }
}
