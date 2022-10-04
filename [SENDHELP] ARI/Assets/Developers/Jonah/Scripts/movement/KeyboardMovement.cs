using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardMovement : MonoBehaviour
{
    float x;
    float z;

    public CharacterController controller;

    public float speed = 15f;
    public float g = -9.8f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool grounded;
    public bool dialogue = true;

    Vector3 v;

    
    void Update()
    {

        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (grounded && v.y < 0)
        {
            v.y = -1f;
        }

        if (dialogue == true)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            v.y += g * Time.deltaTime;

            controller.Move(v * Time.deltaTime);
        }
       
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log(hit.collider.name);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1, Color.white);
            Debug.Log("Did not Hit");
        }
    }
}
