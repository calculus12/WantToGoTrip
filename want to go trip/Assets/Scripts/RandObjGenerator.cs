using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandObjGenerator : MonoBehaviour
{
    [SerializeField] RandObj[] randObjs;
    Queue<GameObject>[] randObjPool;
    Dictionary<GameObject, int> getRandObjPoolIdx;

    [SerializeField] int totalWeight = 100;
    int weightSetIdx;

    [SerializeField] float[] scoreBoundarySet;
    int scoreBoundarySetIdx = 1;
    float score; /* test */

    void Awake()
    {
        // Create object pool
        randObjPool = new Queue<GameObject>[randObjs.Length];
        getRandObjPoolIdx = new Dictionary<GameObject, int>();
        for (int i = 0; i < randObjs.Length; i++)
        {
            randObjPool[i] = new Queue<GameObject>();
            for (int j = 0; j < 10; j++)
            {
                GameObject newObj = Instantiate(randObjs[i].prefab);
                getRandObjPoolIdx[newObj] = i;
                randObjPool[i].Enqueue(newObj);
                newObj.SetActive(false);
            }
        }
    }

    void Start()
    {
        StartCoroutine(RandGenerate());
        StartCoroutine(UpdateWeight());
        StartCoroutine(UpdateScore()); /* test */
    }

    IEnumerator RandGenerate()
    {
        while (true)
        {
            ActivateObj();
            float randTime = Random.Range(3f, 5f);
            yield return new WaitForSeconds(randTime);
        }
    }

    void ActivateObj()
    {
        int weight = 0;
        int selectNum = Random.Range(0, totalWeight);

        for (int i = 0; i < randObjs.Length; i++)
        {
            weight += randObjs[i].weightSet[weightSetIdx];
            if (selectNum < weight)
            {
                GameObject obj = randObjPool[i].Dequeue();
                obj.transform.position = new Vector3(Random.Range(-60f, 60f), 0f, 700f);
                obj.SetActive(true);
                break;
            }
        }
    }

    public void DeactivateObj(GameObject obj)
    {
        int objPoolIdx = getRandObjPoolIdx[obj];
        randObjPool[objPoolIdx].Enqueue(obj);
        obj.SetActive(false);
    }

    IEnumerator UpdateWeight()
    {
        // Update weight when player reach certain score
        while (scoreBoundarySetIdx < scoreBoundarySet.Length)
        {
            if (score > scoreBoundarySet[scoreBoundarySetIdx])
            {
                weightSetIdx++;
                scoreBoundarySetIdx++;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator UpdateScore() /* test */
    {
        while (true)
        {
            score++;
            yield return new WaitForSeconds(1f);
        }
    }
}
