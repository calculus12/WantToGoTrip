using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public PlayerInput input;
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    private float xRotation = 0f;

    // Update is called once per frame
    void Update()
    {
        float mouseX = input.mouseX * mouseSensitivity * Time.deltaTime;
        float mouseY = input.mouseY * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f , 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
