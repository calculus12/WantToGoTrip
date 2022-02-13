using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodInteract : MonoBehaviour
{
    [SerializeField] Item thisItem;

    public PlayerState playerState;
    public PlayerInput playerInput;

    public void Get()
    {
        UIManager.instance.AcquireItem(thisItem);
        gameObject.SetActive(false);
    }
}
