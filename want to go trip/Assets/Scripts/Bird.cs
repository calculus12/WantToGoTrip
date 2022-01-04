using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    void Update()
    {
        // "-Vector3.up" can be changed when apply asset
        transform.Translate(-Vector3.up * EnvironmentManager.instance.birdSpeed * Time.deltaTime);

        if (transform.position.z < EnvironmentManager.instance.endPosZ)
        {
            EnvironmentManager.instance.DeactivateBird(gameObject);
        }
    }
}
