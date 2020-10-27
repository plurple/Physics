using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereObject : MonoBehaviour
{
    public float radius;
    public float mass = 10;
    public Vector3 velocity;
    public float velocityModifier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        radius = gameObject.transform.localScale.x * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        EulersSolverTrajectory.EulerSolver(this);
    }
}
