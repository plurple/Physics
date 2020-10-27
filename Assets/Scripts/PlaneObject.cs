using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneObject : MonoBehaviour
{
    public Vector3 normal;

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
