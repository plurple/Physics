using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EulersSolver : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 acceleration;
    private float time = 1.0f / 60.0f;
    private float velocityModifier = 1.0f;
    private SphereToPlaneCollider planeCollider;
    private SphereToSphereCollider sphereCollider;

    private void Start()
    {
        planeCollider = GetComponent<SphereToPlaneCollider>();
        sphereCollider = GetComponent<SphereToSphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(planeCollider)
            planeCollider.SphereToPlaneCollision(ref velocityModifier);
        if (sphereCollider)
            sphereCollider.SphereToSphereCollision(ref velocityModifier);

        EulerSolver();
    }

    public void EulerSolver()
    {
        velocity += acceleration * time;
        velocity *= velocityModifier;
        Vector3 newPosition = gameObject.transform.position + velocity * time;
        gameObject.transform.position = newPosition;
    }
}
