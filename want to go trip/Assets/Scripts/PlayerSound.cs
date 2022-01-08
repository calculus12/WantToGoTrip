using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    PlayerState state;
    public AudioSource audioPlayer;
    public AudioClip splashSound;

    private bool afterGrounded;
    void Start()
    {
        state = GetComponent<PlayerState>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (afterGrounded && state.isSurface)
        {
            afterGrounded = false;
            audioPlayer.PlayOneShot(splashSound);
        }
        if (state.isOnRaft)
            afterGrounded = true;
    }
}
