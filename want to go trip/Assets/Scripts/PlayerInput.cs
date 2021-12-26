using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour // Manage player's input
{
    public float mouseX { get; private set; }
    public float mouseY { get; private set; }
    public float x { get; private set; }
    public float z { get; private set; }
    public bool space { get; private set; }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        space = Input.GetButtonDown("Jump");
    }
}
