using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    static EnvironmentManager m_instance;

    public Vector3 birdPosMin;
    public Vector3 birdPosMax;
    [SerializeField] float birdSpawnIntervalMin;
    [SerializeField] float birdSpawnIntervalMax;
    [SerializeField] GameObject[] birds;
    Queue<GameObject>[] birdPool;
    Dictionary<GameObject, int> getBirdPoolIdx;

    public static EnvironmentManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<EnvironmentManager>();
            }
            return m_instance;
        }
    }

    void Awake()
    {
        // Signleton
        if (instance != this)
        {
            Destroy(gameObject);
        }

        // Create object pool of bird
        birdPool = new Queue<GameObject>[birds.Length];
        getBirdPoolIdx = new Dictionary<GameObject, int>();
        for (int i = 0; i < birds.Length; i++)
        {
            birdPool[i] = new Queue<GameObject>();
            for (int j = 0; j < 10; j++)
            {
                GameObject newBird = Instantiate(birds[i]);
                getBirdPoolIdx[newBird] = i;
                birdPool[i].Enqueue(newBird);
                newBird.SetActive(false);
            }   
        }
    }

    void Start()
    {
        ActivateBird();
    }

    

    public void ActivateBird()
    {
        // Determine what kind of bird will be spawned
        int randPoolIdx = Random.Range(0, birds.Length);
        GameObject bird = birdPool[randPoolIdx].Dequeue();

        // Determine starting point of bird
        float randPosX = Random.Range(birdPosMin.x, birdPosMax.x);
        float randPosY = Random.Range(birdPosMin.y, birdPosMax.y);
        Vector3 startPos = new Vector3(randPosX, randPosY, birdPosMax.z);

        // Determine destination of bird
        randPosX = Random.Range(birdPosMin.x, birdPosMax.x);
        randPosY = Random.Range(birdPosMin.y, birdPosMax.y);
        Vector3 destPos = new Vector3(randPosX, randPosY, birdPosMin.z);        

        // Spawn bird
        bird.transform.position = startPos;
        bird.transform.LookAt(destPos);
        bird.SetActive(true);

        // Reserve next bird
        float randTime = Random.Range(birdSpawnIntervalMin, birdSpawnIntervalMax);
        Invoke("ActivateBird", randTime);
    }

    public void DeactivateBird(GameObject obj)
    {
        int birdPoolIdx = getBirdPoolIdx[obj];
        birdPool[birdPoolIdx].Enqueue(obj);
        obj.SetActive(false);
    }
}
