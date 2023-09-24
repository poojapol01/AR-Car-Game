using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    public ReticleBehaviour reticleBehaviour;
    private float speed = 1.3f;

    private void Update()
    {
        var trackingPosition = reticleBehaviour.transform.position;

        if(Vector3.Distance(trackingPosition, transform.position) < 0.1)
        {
            return;
        }

        //Calculates a rotation (lookRotation) that points from the current object's position to the trackingPosition
        var lookRotation = Quaternion.LookRotation(trackingPosition - transform.position);

        //It smoothly adjusts the object's rotation towards the lookRotation, making the object face the Reticle
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

        //It smoothly moves the object's position towards the trackingPosition at a speed determined by Speed
        transform.position = Vector3.MoveTowards(transform.position, trackingPosition, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        var Package = other.GetComponent<PackageBehaviour>();
        if (Package != null)
        {
            Destroy(other.gameObject);
        }
    }
}
