using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneObject : MonoBehaviour
{
    public Vector3 UnitScale = new Vector3(10.0f, 0.0f, 10.0f);
    public Vector3 normal;
    public Vector3 corner1;
    public Vector3 corner2;
    public Vector3 corner3;
    public Vector3 corner4;


    // Start is called before the first frame update
    void Start()
    {
        Vector3 cornerGetter = Vector3.Scale(gameObject.transform.localScale, UnitScale * 0.5f);
        normal = gameObject.transform.up;
        corner1 = gameObject.transform.position + new Vector3(cornerGetter.x, 0.0f, cornerGetter.z);
        corner2 = gameObject.transform.position + new Vector3(-cornerGetter.x, 0.0f, cornerGetter.z);
        corner3 = gameObject.transform.position + new Vector3(-cornerGetter.x, 0.0f, -cornerGetter.z);
        corner4 = gameObject.transform.position + new Vector3(cornerGetter.x, 0.0f, -cornerGetter.z);
        Debug.Log(corner1);
        Debug.Log(corner2);
        Debug.Log(corner3);
        Debug.Log(corner4);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
