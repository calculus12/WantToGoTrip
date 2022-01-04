using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (transform.position.z < EnvironmentManager.instance.endPosZ)
        {
            EnvironmentManager.instance.DeactivateBird(gameObject);
        }
    }
}
