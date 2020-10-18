using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EulersSolver : MonoBehaviour
{
    public Vector3 initialPosition;
    public Vector3 velocity;
    public Vector3 acceleration;
    public float time = 0.0f;

    private void Start()
    {
        time += 1.0f / 60.0f;
    }

    // Update is called once per frame
    void Update()
    {
        EulerSolver();
    }

    public void EulerSolver()
    {
        velocity = velocity + acceleration * time;
        Vector3 newPosition = gameObject.transform.position + velocity * time;
        gameObject.transform.position = newPosition;
    }
}
