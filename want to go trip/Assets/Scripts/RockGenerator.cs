using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] objs;
    Queue<GameObject>[] objPool;
    Dictionary<GameObject, int> getPoolIdx;

    [SerializeField] AnimationCurve[] curveSet;
    [SerializeField] float[] scoreSet;
    int setIdx;

    float intensity;
    [SerializeField] float scaleMin;
    [SerializeField] float scaleMax;
    [SerializeField] float healthMin;
    [SerializeField] float healthMax;
    [SerializeField] float damageMin;
    [SerializeField] float damageMax;
    [SerializeField] float xPosMin;
    [SerializeField] float xPosMax;
    [SerializeField] float zPos;
    [SerializeField] float intervalMin;
    [SerializeField] float intervalMax;

    float score; /* test */

    void Awake()
    {
        // Create object pool
        objPool = new Queue<GameObject>[objs.Length];
        getPoolIdx = new Dictionary<GameObject, int>();
        for (int i = 0; i < objs.Length; i++)
        {
            objPool[i] = new Queue<GameObject>();
            for (int j = 0; j < 10; j++)
            {
                GameObject newObj = Instantiate(objs[i]);
                getPoolIdx[newObj] = i;
                objPool[i].Enqueue(newObj);
                newObj.SetActive(false);
            }
        }
    }

    void Start()
    {
        StartCoroutine(GenerateRock());
        StartCoroutine(UpdateSetIdx());
        StartCoroutine(UpdateScore()); /* test */
    }

    IEnumerator GenerateRock()
    {
        // Random : position, rotation, scale, health, damage, time interval
        // scale, health, damage are affected by intensity
        while (true)
        {
            intensity = curveSet[setIdx].Evaluate(Random.value);
            Debug.Log("Intensity : " + intensity);
            GameObject obj = objPool[Random.Range(0, objs.Length)].Dequeue();
            obj.transform.position = new Vector3(Random.Range(xPosMin, xPosMax), 0f, zPos);
            obj.transform.eulerAngles = new Vector3(0f, Random.Range(0f, 360f), 0f);
            obj.transform.localScale = Vector3.one * Mathf.Lerp(scaleMin, scaleMax, intensity);

            RockInteract rockInteract = obj.GetComponent<RockInteract>();
            rockInteract.startingHealth = Mathf.Lerp(healthMin, healthMax, intensity);
            rockInteract.damageOnRaft = Mathf.Lerp(damageMin, damageMax, intensity);
            obj.SetActive(true);
            
            float randomTime = Random.Range(intervalMin, intervalMax);
            yield return new WaitForSeconds(randomTime);
        }
    }

    public void DeactivateObj(GameObject obj)
    {
        int objPoolIdx = getPoolIdx[obj];
        objPool[objPoolIdx].Enqueue(obj);
        obj.SetActive(false);
    }

    IEnumerator UpdateSetIdx()
    {
        // Update index of curveSet and scoreSet when player reach certain score
        int lastIdx = scoreSet.Length - 1;
        while (setIdx < lastIdx)
        {
            if (score > scoreSet[setIdx + 1])
            {
                setIdx++;
                Debug.Log("score : " + scoreSet[setIdx]);
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
