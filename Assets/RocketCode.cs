using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCode : MonoBehaviour
{
    [SerializeField] private float speed = 0f;

    private Vector2 velocity;
    // Update is called once per frame
    void Update()
    {
        Vector2 acceleration = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
        {
            acceleration.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            acceleration.x = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            acceleration.y = 1;
            Debug.Log("W pressed");
        }
        if (Input.GetKey(KeyCode.S))
        {
            acceleration.y = -1;
        }
        

        velocity = velocity + acceleration;
        Debug.Log("Velocit: " + velocity + " Acceleration: " + acceleration);
        transform.position = transform.position + (Vector3)velocity * speed * Time.deltaTime;
    }
}
