using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCode : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private float drag = 1f;

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
        if(acceleration == Vector2.zero)
        {
            acceleration = new Vector2(0, -0.09f);
        }
        
        acceleration = acceleration.normalized * speed;
        velocity = velocity + acceleration * Time.deltaTime;

        Debug.DrawLine(transform.position, transform.position + (Vector3)velocity, Color.red);

        //Debug.Log("Velocit: " + velocity + " Acceleration: " + acceleration);
        transform.position = transform.position + (Vector3)velocity * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        velocity = velocity / drag;
    }

    //Implement Drag
}
