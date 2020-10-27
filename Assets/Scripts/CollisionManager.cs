using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private GameObject[] spheres;
    private GameObject[] planes;


    private void Awake()
    {
        spheres = GameObject.FindGameObjectsWithTag("Sphere");
        planes = GameObject.FindGameObjectsWithTag("Plane");
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        CheckForCollisions();
    }

    void CheckForCollisions()
    {
        for(int i = 0; i < spheres.Length; i++)
        {
            for(int j = i + 1; j < spheres.Length; j++)
            {
                SphereToSphereCollision(spheres[i].GetComponent<SphereObject>(), spheres[j].GetComponent<SphereObject>());
            }
            for(int j = 0; j < planes.Length; j++)
            {
                SphereToPlaneCollision(spheres[i].GetComponent<SphereObject>(), planes[j].GetComponent<PlaneObject>());
            }
        }
    }


    public void SphereToSphereCollision(SphereObject sphere1, SphereObject sphere2)
    {
        Vector3 vectorBetweenCenters = sphere2.transform.position - sphere1.transform.position;
        Vector3 velocity = sphere1.velocity;
        float angleBetweenCentersAndVelocity = HelperFunctions.GetAngle(vectorBetweenCenters, velocity);
        float distanceBetweenSphereCentersAtClosest = HelperFunctions.DistanceBetweenTwoPoints(angleBetweenCentersAndVelocity, vectorBetweenCenters);
        if (distanceBetweenSphereCentersAtClosest <= sphere1.radius + sphere2.radius)
        {
            float distanceBetweenCollisionPointAndClosestPoint = Mathf.Sqrt(HelperFunctions.Squared(sphere1.radius + sphere2.radius) - HelperFunctions.Squared(distanceBetweenSphereCentersAtClosest));
            float distanceToCollision = Mathf.Cos(angleBetweenCentersAndVelocity * Mathf.Deg2Rad) * vectorBetweenCenters.magnitude - distanceBetweenCollisionPointAndClosestPoint;
            if (distanceToCollision < float.Epsilon) distanceToCollision = 0.0f;
            if (distanceToCollision <= velocity.magnitude * Time.deltaTime)
            {
                sphere1.velocityModifier = distanceToCollision / (velocity.magnitude * Time.deltaTime);
            }
        }
    }

    public void SphereToPlaneCollision(SphereObject sphere, PlaneObject plane)
    {
        if (HelperFunctions.GetAngle(plane.normal, -sphere.velocity) < 90.0f)
        {
            Vector3 startPosition = sphere.transform.position;
            Vector3 arbitaryPoint = plane.transform.position;
            //vectorBetweenPointToSphere
            Vector3 p = startPosition - arbitaryPoint;
            float angleBetweenNormalAndP = HelperFunctions.GetAngle(plane.normal, p);
            float angleBetweenPAndPlane = 90.0f - angleBetweenNormalAndP;
            float angleBetweenVAndMinusN = HelperFunctions.GetAngle(-plane.normal, sphere.velocity);
            float d = HelperFunctions.DistanceBetweenTwoPoints(angleBetweenPAndPlane, p);
            float distanceToContact = (d - sphere.radius) / Mathf.Cos(angleBetweenVAndMinusN * Mathf.Deg2Rad);
            if (distanceToContact < float.Epsilon) distanceToContact = 0.0f;
            if (distanceToContact <= sphere.velocity.magnitude * Time.deltaTime)
            {
                sphere.velocityModifier = distanceToContact / (sphere.velocity.magnitude * Time.deltaTime);
                Debug.Log("Collided with plane");
            }
        }
    }
}

