using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneObject : MonoBehaviour
{
    public Vector3 normal;
    public GameObject corner1;
    public GameObject corner2;
    public GameObject corner3;
    public GameObject corner4;


    // Start is called before the first frame update
    void Start()
    {
        normal = gameObject.transform.up;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
