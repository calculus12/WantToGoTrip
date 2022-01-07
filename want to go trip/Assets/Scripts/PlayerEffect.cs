using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    PlayerState state;
    bool afterGrounded;

    void Awake()
    {
        state = GetComponent<PlayerState>();
    }

    void Update()
    {
        if (afterGrounded && state.isSurface)
        {
            afterGrounded = false;
            Vector3 pos = new Vector3(transform.position.x, 0f, transform.position.z);
            Quaternion rot = Quaternion.identity;
            Vector3 size = Vector3.one * 2.5f;
            EffectManager.instance.ActivateEffect(EffectManager.EffectType.splash, pos, rot, size);
        }
        else if (state.isGrounded)
        {
            afterGrounded = true;
        }
    }
}
