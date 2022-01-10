using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour // Manage player's input
{
    public float mouseX { get; private set; }
    public float mouseY { get; private set; }
    public float horizontal { get; private set; }
    public float vertical { get; private set; }
    public bool space { get; private set; }
    public bool f { get; private set; }
    public bool ctrl { get; private set; }
    public bool leftClick { get; private set; }
    public bool w { get; private set; }
    public bool a { get; private set; }
    public bool d { get; private set; }
    public bool esc { get; private set; }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.instance.isPlaying) {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            space = Input.GetButton("Jump");
            ctrl = Input.GetKey(KeyCode.LeftControl);
            leftClick = Input.GetMouseButton(0);
            f = Input.GetKeyDown(KeyCode.F);
            w = Input.GetKeyDown(KeyCode.W);
            a = Input.GetKeyDown(KeyCode.A);
            d = Input.GetKeyDown(KeyCode.D);
            esc = Input.GetKeyDown(KeyCode.Escape);
        }
    }
}
