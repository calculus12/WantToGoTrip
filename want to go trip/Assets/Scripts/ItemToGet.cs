using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToGet : MonoBehaviour
{
    [SerializeField] Item item;

    void Start() {
        StartCoroutine(DetectPlayer());
    }

    IEnumerator DetectPlayer()
    {
        while (Physics.OverlapSphere(transform.position, 0.7f, 1 << 8).Length == 0)
        {
            yield return new WaitForSeconds(0.2f);
        }
        UIManager.instance.AcquireItem(item);
        Destroy(gameObject);
    }
}
