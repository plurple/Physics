using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereToPlaneCollider : MonoBehaviour
{
    Vector3 normalToHorizontalPlane = new Vector3(0, 1, 0);
    public Vector3 arbitaryPoint;
       
    float DistanceBetweenSphereAndPlane(float angleBetweenPAndPlane, Vector3 p)
    {
        return Mathf.Sin(angleBetweenPAndPlane * Mathf.Deg2Rad) * Vector3.Magnitude(p);
    }

    public void SphereToPlaneCollision(ref float velocityModifier)
    {
        Vector3 startPosition = gameObject.transform.position;
        Vector3 velocity = gameObject.GetComponent<EulersSolver>().velocity;
        float radiusOfSphere = transform.localScale.x / 2.0f;
        if (HelperFunctions.GetAngle(normalToHorizontalPlane, -velocity) < 90.0f)
        {
            //vectorBetweenPointToSphere
            Vector3 p = startPosition - arbitaryPoint;
            float angleBetweenNormalAndP = HelperFunctions.GetAngle(normalToHorizontalPlane, p);
            float angleBetweenPAndPlane = 90.0f - angleBetweenNormalAndP;
            float angleBetweenVAndMinusN = HelperFunctions.GetAngle(-normalToHorizontalPlane, velocity);
            float d = DistanceBetweenSphereAndPlane(angleBetweenPAndPlane, p);
            float distanceToContact = (d - radiusOfSphere) / Mathf.Cos(angleBetweenVAndMinusN * Mathf.Deg2Rad);
            if (distanceToContact < float.Epsilon) distanceToContact = 0.0f;
            if (distanceToContact <= velocity.magnitude * (1.0f / 60.0f))
            {
                velocityModifier = distanceToContact / (velocity.magnitude * (1.0f / 60.0f));
            }
        }
    }
}
