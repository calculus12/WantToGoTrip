using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] SailingControl sc;

    private void OnEnable()
    {
        sc.enabled = true;
    }

    private void OnDisable()
    {
        sc.enabled = false;
    }
}
