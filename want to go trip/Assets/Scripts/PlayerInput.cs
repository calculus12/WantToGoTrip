using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
    public bool alpha1 { get; private set; }
    public bool alpha2 { get; private set; }
    public bool alpha3 { get; private set; }
    public bool alpha4 { get; private set; }
    public bool alpha5 { get; private set; }
    public bool alpha6 { get; private set; }
    public bool alpha7 { get; private set; }
    public bool alpha8 { get; private set; }
    public bool alpha9 { get; private set; }
    public bool alpha0 { get; private set; }
    public bool tab { get; private set; }

    [SerializeField] CinemachineFreeLook cinemachineFreeLook;
    float cinemachineOriginXSpeed;
    float cinemachineOriginYSpeed;
    bool cursorConfined;

    void Awake()
    {
        cinemachineOriginXSpeed = cinemachineFreeLook.m_XAxis.m_MaxSpeed;
        cinemachineOriginYSpeed = cinemachineFreeLook.m_YAxis.m_MaxSpeed;   
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.instance.isPlaying)
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            space = Input.GetButton("Jump");
            ctrl = Input.GetKey(KeyCode.LeftControl);
            f = Input.GetKeyDown(KeyCode.F);
            w = Input.GetKeyDown(KeyCode.W);
            a = Input.GetKeyDown(KeyCode.A);
            d = Input.GetKeyDown(KeyCode.D);
            esc = Input.GetKeyDown(KeyCode.Escape);
            alpha1 = Input.GetKeyDown(KeyCode.Alpha1);
            alpha2 = Input.GetKeyDown(KeyCode.Alpha2);
            alpha3 = Input.GetKeyDown(KeyCode.Alpha3);
            alpha4 = Input.GetKeyDown(KeyCode.Alpha4);
            alpha5 = Input.GetKeyDown(KeyCode.Alpha5);
            alpha6 = Input.GetKeyDown(KeyCode.Alpha6);
            alpha7 = Input.GetKeyDown(KeyCode.Alpha7);
            alpha8 = Input.GetKeyDown(KeyCode.Alpha8);
            alpha9 = Input.GetKeyDown(KeyCode.Alpha9);
            alpha0 = Input.GetKeyDown(KeyCode.Alpha0);
            tab = Input.GetKeyDown(KeyCode.Tab);
            
            if (tab)
            {
                cursorConfined = !cursorConfined;
            }
            if (cursorConfined)
            {
                leftClick = false;
                 // If cursor is confined, block camera rotation
                cinemachineFreeLook.m_XAxis.m_MaxSpeed = 0f;
                cinemachineFreeLook.m_YAxis.m_MaxSpeed = 0f;
            }
            else
            {
                leftClick = Input.GetMouseButton(0);
                // If cursor is locked, do not block camera rotation
                cinemachineFreeLook.m_XAxis.m_MaxSpeed = cinemachineOriginXSpeed;
                cinemachineFreeLook.m_YAxis.m_MaxSpeed = cinemachineOriginYSpeed;
            }
        }
    }
}
