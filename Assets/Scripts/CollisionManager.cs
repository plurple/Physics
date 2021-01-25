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
        Vector3 velocitySphere1 = sphere1.velocity * Time.deltaTime;
        Vector3 velocitySphere2 = sphere2.velocity * Time.deltaTime;
        Vector3 velocitySphere1RelativeToSphere2 = velocitySphere1 - velocitySphere2;
        float angleBetweenCentersAndVelocity = HelperFunctions.GetAngle(vectorBetweenCenters, velocitySphere1RelativeToSphere2);

        float distanceBetweenSphereCentersAtClosest = HelperFunctions.DistanceBetweenTwoPoints(angleBetweenCentersAndVelocity, vectorBetweenCenters);
        if (distanceBetweenSphereCentersAtClosest <= sphere1.radius + sphere2.radius)
        {
            float distanceBetweenCollisionPointAndClosestPoint = Mathf.Sqrt(HelperFunctions.Squared(sphere1.radius + sphere2.radius) - HelperFunctions.Squared(distanceBetweenSphereCentersAtClosest));
            float distanceToCollision = Mathf.Cos(angleBetweenCentersAndVelocity * Mathf.Deg2Rad) * vectorBetweenCenters.magnitude - distanceBetweenCollisionPointAndClosestPoint;
            if (distanceToCollision < float.Epsilon) distanceToCollision = 0.0f;
            if (distanceToCollision <= velocitySphere1RelativeToSphere2.magnitude)
            {
                Vector3 normalisedContactDirection = vectorBetweenCenters.normalized;
                float velocityOfSphere1InContactDirection = Vector3.Dot(sphere1.velocity, normalisedContactDirection);
                float velocityOfSphere2InContactDirection = Vector3.Dot(sphere2.velocity, normalisedContactDirection);
                float momentum = (2.0f * (velocityOfSphere1InContactDirection - velocityOfSphere2InContactDirection)) / (sphere1.mass + sphere2.mass);
                sphere1.velocity = sphere1.velocity - momentum * sphere2.mass * normalisedContactDirection;
                sphere2.velocity = sphere2.velocity + momentum * sphere1.mass * normalisedContactDirection;
            }
        }

    }

    public void SphereToPlaneCollision(SphereObject sphere, PlaneObject plane)
    {
        if (HelperFunctions.GetAngle(plane.normal, -sphere.velocity) < 90.0f)
        {
            Vector3 spherePosition = sphere.transform.position;
            Vector3 planePosition = plane.transform.position;
            //vectorBetweenPointToSphere
            Vector3 p0 = spherePosition - planePosition;
            float angleBetweenNormalAndP = HelperFunctions.GetAngle(plane.normal, p0);
            float angleBetweenPAndPlane = 90.0f - angleBetweenNormalAndP;
            float angleBetweenVAndMinusN = HelperFunctions.GetAngle(-plane.normal, sphere.velocity * Time.deltaTime);
            float d = HelperFunctions.DistanceBetweenTwoPoints(angleBetweenPAndPlane, p0);
            float distanceToContact = (d - sphere.radius) / Mathf.Cos(angleBetweenVAndMinusN * Mathf.Deg2Rad);
            if (distanceToContact < float.Epsilon) distanceToContact = 0.0f;
            if (distanceToContact <= sphere.velocity.magnitude * Time.deltaTime)
            {
                Vector3 positionOfContact = spherePosition + sphere.velocity.normalized * distanceToContact - plane.normal.normalized * sphere.radius;
                float onPlane = Vector3.Dot(positionOfContact - planePosition, plane.normal);
                if (onPlane <= 0.0001f && onPlane >= -0.0001f)
                {
                    Vector3 planeLeft = plane.corner1.transform.position - plane.corner2.transform.position;
                    Vector3 planeBottom = plane.corner3.transform.position - plane.corner2.transform.position;
                    float value1 = Vector3.Dot(positionOfContact, planeLeft);
                    float value2 = Vector3.Dot(positionOfContact, planeBottom);
                    float bound1 = Vector3.Dot(plane.corner2.transform.position, planeLeft);
                    float bound2 = Vector3.Dot(plane.corner1.transform.position, planeLeft);
                    float bound3 = Vector3.Dot(plane.corner2.transform.position, planeBottom);
                    float bound4 = Vector3.Dot(plane.corner3.transform.position, planeBottom);
                    if (bound1 <= value1 &&
                        value1 <= bound2 &&
                        bound3 <= value2 &&
                        value2 <= bound4)
                    {
                        Vector3 bounceUnitVector = 2 * plane.normal * Vector3.Dot(plane.normal, -sphere.velocity.normalized) + sphere.velocity.normalized;
                        sphere.velocity = bounceUnitVector * (sphere.velocity.magnitude);
                    }
                    else
                    {
                        Vector3 planeLeftRadius = new Vector3(plane.corner1.transform.position.x + sphere.radius, plane.corner1.transform.position.y, plane.corner1.transform.position.z + sphere.radius) - new Vector3(plane.corner2.transform.position.x + sphere.radius, plane.corner2.transform.position.y, plane.corner2.transform.position.z + sphere.radius);
                        Vector3 planeBottomRadius = new Vector3(plane.corner3.transform.position.x + sphere.radius, plane.corner3.transform.position.y, plane.corner3.transform.position.z + sphere.radius) - new Vector3(plane.corner2.transform.position.x + sphere.radius, plane.corner2.transform.position.y, plane.corner2.transform.position.z + sphere.radius);
                        float value1Radius = Vector3.Dot(positionOfContact, planeLeftRadius);
                        float value2Radius = Vector3.Dot(positionOfContact, planeBottomRadius);
                        float bound1Radius = Vector3.Dot(new Vector3(plane.corner2.transform.position.x + sphere.radius, plane.corner2.transform.position.y, plane.corner2.transform.position.z + sphere.radius), planeLeftRadius);
                        float bound2Radius = Vector3.Dot(new Vector3(plane.corner1.transform.position.x + sphere.radius, plane.corner1.transform.position.y, plane.corner1.transform.position.z + sphere.radius), planeLeftRadius);
                        float bound3Radius = Vector3.Dot(new Vector3(plane.corner2.transform.position.x + sphere.radius, plane.corner2.transform.position.y, plane.corner2.transform.position.z + sphere.radius), planeBottomRadius);
                        float bound4Radius = Vector3.Dot(new Vector3(plane.corner3.transform.position.x + sphere.radius, plane.corner3.transform.position.y, plane.corner3.transform.position.z + sphere.radius), planeBottomRadius);
                        if (bound1Radius <= value1Radius &&
                        value1Radius <= bound2Radius &&
                        bound3Radius <= value2Radius &&
                        value2Radius <= bound4Radius)
                        {
                            //do a bounding box check once written.
                        }
                    }                    
                }
            }
        }
    }
}

