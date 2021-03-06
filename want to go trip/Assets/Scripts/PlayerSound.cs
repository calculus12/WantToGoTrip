using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    PlayerState state;
    private AudioSource audioPlayer;
    public AudioClip splashSound;

    private bool afterGrounded;
    void Start()
    {
        state = GetComponent<PlayerState>();
        audioPlayer = GetComponent<AudioSource>();
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
