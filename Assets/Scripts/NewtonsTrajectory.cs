using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewtonsTrajectory : MonoBehaviour
{
    private Vector3 initialPosition;
    public Vector3 velocity;
    public Vector3 acceleration;
    private float time = 0.0f;

    private void Awake()
    {
        initialPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        NewtonsLaw();
    }

    public void NewtonsLaw()
    {
        Vector3 newPosition = new Vector3(0.0f, 0.0f, 0.0f);
        for(int i = 0; i < 3; i++)
        {
            newPosition[i] = initialPosition[i] + (velocity[i] * time) + ((acceleration[i] * time * time) / 2.0f);
        }

        gameObject.transform.position = newPosition;
    }
}
