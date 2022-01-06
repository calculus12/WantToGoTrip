using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMove : MonoBehaviour
{
    [SerializeField] GameObject[] terrains;
    [SerializeField] float speed;
    [SerializeField] float startPosZ;
    [SerializeField] float endPosZ;
    int targetIdx;
    GameObject targetTerrain;

    void Awake()
    {
        targetIdx = terrains.Length - 1;
        targetTerrain = terrains[targetIdx];
    }

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (targetTerrain.transform.position.z <= endPosZ)
        {
            // Move target terrain to the starting point
            Vector3 tmp = targetTerrain.transform.position;
            tmp.z = startPosZ;
            targetTerrain.transform.position = tmp;

            // Change target terrain to next terrain
            targetIdx = targetIdx > 0 ? targetIdx - 1 : terrains.Length - 1;
            targetTerrain = terrains[targetIdx];
        }
    }
}
