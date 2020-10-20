using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereToSphereCollider : MonoBehaviour
{
    public GameObject otherSphere;

    public void SphereToSphereCollision(ref float velocityModifier)
    {
        Vector3 vectorBetweenCenters = otherSphere.transform.position - transform.position;
        Vector3 velocity = gameObject.GetComponent<EulersSolver>().velocity;
        float otherSphereRadius = otherSphere.transform.localScale.x / 2.0f;
        float thisSphereRadius = transform.localScale.x / 2.0f;
        float angleBetweenCentersAndVelocity = HelperFunctions.GetAngle(vectorBetweenCenters, velocity);
        float distanceBetweenSphereCentersAtClosest = Mathf.Sin(angleBetweenCentersAndVelocity * Mathf.Deg2Rad) * vectorBetweenCenters.magnitude;
        if(distanceBetweenSphereCentersAtClosest < otherSphereRadius + thisSphereRadius)
        {
            float distanceBetweenCollisionPointAndClosestPoint = Mathf.Sqrt(HelperFunctions.Squared(otherSphereRadius + thisSphereRadius) - HelperFunctions.Squared(distanceBetweenSphereCentersAtClosest));
            float distanceToCollision = Mathf.Cos(angleBetweenCentersAndVelocity * Mathf.Deg2Rad) * vectorBetweenCenters.magnitude - distanceBetweenCollisionPointAndClosestPoint;
            if (distanceToCollision < float.Epsilon) distanceToCollision = 0.0f;
            if (distanceToCollision <= velocity.magnitude * (1.0f / 60.0f))
            {
                velocityModifier = distanceToCollision / (velocity.magnitude * (1.0f / 60.0f));
            }
        }
    }
}
