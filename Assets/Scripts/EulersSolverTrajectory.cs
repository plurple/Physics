using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EulersSolverTrajectory : MonoBehaviour
{
    public static Vector3 acceleration = new Vector3(0.0f, -9.8f, 0.0f);
    
    public static void EulerSolver(SphereObject sphere)
    {
        sphere.velocity += acceleration * Time.deltaTime;
        Vector3 newPosition = sphere.transform.position + sphere.velocity * Time.deltaTime;
        sphere.transform.position = newPosition;
    }
}
