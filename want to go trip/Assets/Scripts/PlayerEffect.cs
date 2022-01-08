using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    private PlayerState state;
    private AudioSource audioPlayer;
    public AudioClip splashSound;
    bool afterGrounded;

    void Awake()
    {
        state = GetComponent<PlayerState>();
        audioPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (afterGrounded && state.isSurface)
        {
            afterGrounded = false;
            audioPlayer.PlayOneShot(splashSound);
            Vector3 pos = new Vector3(transform.position.x, 0f, transform.position.z);
            Quaternion rot = Quaternion.identity;
            Vector3 size = Vector3.one * 2.5f;
            EffectManager.instance.ActivateEffect(EffectManager.EffectType.splash, pos, rot, size);
        }
        else if (state.isOnRaft)
        {
            afterGrounded = true;
        }
    }
}
