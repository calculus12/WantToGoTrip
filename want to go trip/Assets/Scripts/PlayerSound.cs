using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    Movement state;
    public AudioSource audioPlayer;
    public AudioClip splashSound;

    private bool afterGrounded;
    void Start()
    {
        state = GetComponent<Movement>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (afterGrounded && state.isSurface)
        {
            afterGrounded = false;
            audioPlayer.PlayOneShot(splashSound);
        }
        if (state.isGrounded)
            afterGrounded = true;
    }
}
