using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public float endPosZ;
    public float birdSpeed;
    public float birdFlyingHeightMin;
    public float birdyFlyingHeightMax;

    [SerializeField] Vector3 leftEnvStartPos;
    [SerializeField] Vector3 rightEnvStartPos;
    [SerializeField] GameObject[] birds;

    static EnvironmentManager m_instance;

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

        // Create bird that is visible as soon as game starts
        ActivateBird();
    }

    public void ActivateBird()
    {
        // Decide what kind of bird will be spawned
        int randPoolIdx = Random.Range(0, birds.Length);
        GameObject bird = birdPool[randPoolIdx].Dequeue();

        // Decide where the bird will be spawned
        float randPosY = Random.Range(birdFlyingHeightMin, birdyFlyingHeightMax);
        float randPosX = Random.Range(leftEnvStartPos.x + 10f, rightEnvStartPos.x - 10f);
        bird.transform.position = new Vector3(randPosX, randPosY, 150f);

        // Decide where the bird will go
        Vector3 temp = bird.transform.rotation.eulerAngles;
        temp.y = Random.Range(60f, 120f);
        bird.transform.rotation = Quaternion.Euler(temp);

        // Spawn
        bird.SetActive(true);

        // Decide when the next bird will be spawned
        float randTime = Random.Range(0, 10f);

        // Reserve spawning
        Invoke("ActivateBird", randTime);
    }

    public void DeactivateBird(GameObject obj)
    {
        int birdPoolIdx = getBirdPoolIdx[obj];
        birdPool[birdPoolIdx].Enqueue(obj);
        obj.SetActive(false);
    }
}
