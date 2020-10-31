using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererObject : MonoBehaviour
{
    LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        Color colour = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        lineRenderer.startColor = colour;
        lineRenderer.endColor = colour;
    }

    public void SetLine(Vector3 planePostion)
    {
        
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, planePostion);
    }
}
