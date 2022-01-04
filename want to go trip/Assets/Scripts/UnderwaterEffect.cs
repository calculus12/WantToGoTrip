using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UnderwaterEffect : MonoBehaviour
{
    public GameObject camera;

    public BoxCollider boundingBox;

    public Volume post;

    public AudioSource oceanSound;

    public AudioSource underwaterSound;

    public Color underwaterColor;

    private bool underWater;

    private bool isPlayingUnderwaterSound;

    public float underwaterFocusDistance = 1f;
    
    // Effects
    private Vignette vg;

    private DepthOfField dof;

    private ColorAdjustments ca;
    // Start is called before the first frame update
    void Start()
    {
        post.profile.TryGet(out dof);
        post.profile.TryGet(out ca);
        post.profile.TryGet(out vg);
    }

    // Update is called once per frame
    void Update()
    {
        if (boundingBox.bounds.Contains(camera.transform.position))
        {
            underWater = true;
        }
        else
        {
            underWater = false;
        }

        if (underWater)
        {
            vg.intensity.value = 0.35f;
            dof.focusDistance.value = underwaterFocusDistance;
            ca.colorFilter.value = underwaterColor;
            if (!isPlayingUnderwaterSound)
            {
                
                oceanSound.Pause();
                underwaterSound.Play();
                isPlayingUnderwaterSound = true;
            }
        }
        else
        {
            vg.intensity.value = 0.292f;
            dof.focusDistance.value = 5f;
            ca.colorFilter.value = Color.white;
            if(isPlayingUnderwaterSound)
            {
                oceanSound.Play();
                underwaterSound.Pause();
                isPlayingUnderwaterSound = false;
            }
            
        }
    }
}
