using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CrashManager : MonoBehaviour
{
    private RaftHealth raftHealth;
    public List<GameObject> crashes;

    private float damageInterval = 20f;
    private int crashCount = 0;
    private List<int> remainIdx;

    private void Start()
    {
        raftHealth = GetComponent<RaftHealth>();
        remainIdx = new List<int>();
        for (int i = 0; i < crashes.Count; i++)
            remainIdx.Add(i);
    }

    public void Generate()
    {
        int targetCount = Math.Min((int)Math.Ceiling((raftHealth.startingHealth - raftHealth.health) / damageInterval), crashes.Count);
        while(crashCount < targetCount)
        {
            int idx = remainIdx[UnityEngine.Random.Range(0, remainIdx.Count)];
            crashes[idx].SetActive(true);
            remainIdx.Remove(idx);
            crashCount++;
        }
    }

    public void Remove(GameObject crash)
    {
        int idx = -1;
        for (int i = 0; i < crashes.Count; i++)
        {
            if (crash == crashes[i])
            {
                idx = i;
                break;
            }
        }
        if (idx != -1)
        {
            crashes[idx].SetActive(false);
            remainIdx.Add(idx);
            crashCount--;
            raftHealth.RestoreHealth(damageInterval);
        }
    }
}
