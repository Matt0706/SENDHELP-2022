using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMouseMovement : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Transform player;
    float xRotation = 0f;
    float mouseXAxis;
    float mouseYAxis;

    public Camera cam;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        //KBM Controls
        mouseXAxis = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseYAxis = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseYAxis;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        player.Rotate(Vector3.up * mouseXAxis);
    }
}
