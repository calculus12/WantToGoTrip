using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentScript : MonoBehaviour
{
    // 배경 움직임 스크립트
    public float initPos;
    public float endPos;
    public float speed = 3f;
    public GameObject[] environments;

    void Start()
    {

    }


    void FixedUpdate()
    {
        for (int i = 0; i < environments.Length; i++)
        {
            environments[i].transform.Translate(new Vector3(0, 0, -speed)*Time.deltaTime);
            if (environments[i].transform.position.z <= endPos)
            {
                Vector3 temp = environments[i].transform.position;
                temp.z = initPos;
                environments[i].transform.position = temp;
            }
        }
    }
}
