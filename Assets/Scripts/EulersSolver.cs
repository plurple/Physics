using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EulersSolver : MonoBehaviour
{
    public Vector3 velocity;
    public Vector3 acceleration;
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
        velocity += acceleration * Time.deltaTime;
        velocity *= velocityModifier;
        Vector3 newPosition = gameObject.transform.position + velocity * Time.deltaTime;
        gameObject.transform.position = newPosition;
    }
}
