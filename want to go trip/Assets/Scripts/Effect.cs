using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    ParticleSystem particle;

    void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    void Start() {
        particle.Play();
    }

    void Update()
    {
        if (particle.isStopped)
        {
            EffectManager.instance.DeactivateEffect(gameObject);
        }
    }
}
