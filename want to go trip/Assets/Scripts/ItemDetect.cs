using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetect : MonoBehaviour
{
    public LayerMask woodLayer;
    private bool isNearWood;
    private PlayerInput playerInput;
    [SerializeField] Transform detectTransform;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        WoodInteract wood;
        Collider[] nearWoods = Physics.OverlapSphere(detectTransform.position, 1f, woodLayer);
        if (nearWoods.Length > 0) // detect wood
        {
            wood = nearWoods[0].GetComponent<WoodInteract>();
            UIManager.instance.SetActiveWoodUI(true);
        }
        else
        {
            wood = null;
            UIManager.instance.SetActiveWoodUI(false);
        }

        if (wood != null)
        {
            if (playerInput.f) // if input F then, get wood
                wood.Get();
        }
    }
}
