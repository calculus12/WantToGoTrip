using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveByOcean : MonoBehaviour
{
    Rigidbody objectRigidbody;
    public float speed = 3f;
    // Start is called before the first frame update
    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move(speed);
    }

    public void move(float speed)
    {
        objectRigidbody.velocity = new Vector3(0, 0, -speed);
    }
}
