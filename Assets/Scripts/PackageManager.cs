using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PackageManager : MonoBehaviour
{
    public DrivingSurfaceManager drivingSurfaceManager;
    public PackageBehaviour packageBehaviour;
    public GameObject packagePrefab;

    public static Vector3 RandomInTriangle(Vector3 v1, Vector3 v2)
    {
        float u = Random.Range(0.0f, 1.0f);
        float v = Random.Range(0.0f, 1.0f);
        if (v + u > 1)
        {
            v = 1 - v;
            u = 1 - u;
        }

        return (v1 * u) + (v2 * v);
    }

    public static Vector3 FindRandomLocation(ARPlane plane)
    {
        // Select random triangle in Mesh
        var mesh = plane.GetComponent<ARPlaneMeshVisualizer>().mesh;
        var triangles = mesh.triangles;
        var triangle = triangles[(int)Random.Range(0, triangles.Length - 1)] / 3 * 3;
        var vertices = mesh.vertices;
        var randomInTriangle = RandomInTriangle(vertices[triangle], vertices[triangle + 1]);
        var randomPoint = plane.transform.TransformPoint(randomInTriangle);

        return randomPoint;
    }

    private void SpawnPackage(ARPlane plane)
    {
        var packageObj = GameObject.Instantiate(packagePrefab);
        packageObj.transform.position = FindRandomLocation(plane);

        packageBehaviour = packageObj.GetComponent<PackageBehaviour>();
    }

    private void Update()
    {
        var lockedPlane = drivingSurfaceManager.LockedPlane;
        if(lockedPlane != null)
        {
            
            if(packageBehaviour == null)
            {
                SpawnPackage(lockedPlane);
            }
            var packagePosition = packageBehaviour.gameObject.transform.position;
            packagePosition.Set(packagePosition.x, lockedPlane.center.y, packagePosition.z); 
        }
    }
}
