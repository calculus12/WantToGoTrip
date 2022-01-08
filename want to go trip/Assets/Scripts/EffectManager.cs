using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public enum EffectType {splash};

    [SerializeField] GameObject[] effect;
    static EffectManager m_instance;
    Queue<GameObject>[] effectPool;
    Dictionary<GameObject, int> getEffectPoolIdx;

    void Awake() {
        // Signleton
        if (instance != this)
        {
            Destroy(gameObject);
        }

        // Object pool
        effectPool = new Queue<GameObject>[effect.Length];
        getEffectPoolIdx = new Dictionary<GameObject, int>();
        for (int i = 0; i < effect.Length; i++)
        {
            effectPool[i] = new Queue<GameObject>();
            for (int j = 0; j < 10; j++)
            {
                GameObject newEffect = Instantiate(effect[i]);
                getEffectPoolIdx[newEffect] = i;
                effectPool[i].Enqueue(newEffect);
                newEffect.SetActive(false);
            }
        }
    }

    public static EffectManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<EffectManager>();
            }
            return m_instance;
        }
    }

    public void ActivateEffect(EffectType type, Vector3 pos, Quaternion rot, Vector3 size)
    {
        GameObject effect = effectPool[(int)type].Dequeue();
        effect.transform.position = pos;
        effect.transform.rotation = rot;
        effect.transform.localScale = size;
        effect.SetActive(true);
    }

    public void DeactivateEffect(GameObject effect)
    {
        int effectPoolIdx = getEffectPoolIdx[effect];
        effectPool[effectPoolIdx].Enqueue(effect);
        effect.SetActive(false);
    }
}
