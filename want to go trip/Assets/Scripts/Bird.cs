using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float speed;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.z < EnvironmentManager.instance.birdPosMin.z)
        {
            EnvironmentManager.instance.DeactivateBird(gameObject);
        }
    }
}
