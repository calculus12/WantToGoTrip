using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] PlayerState state;
    [SerializeField] PlayerInput input;
    [SerializeField] GameObject weaponSlot;
    [SerializeField] Item paddle;
    [SerializeField] Item fishingRod;
    [SerializeField] Item axe;
    Slot[] slots;
    Transform[] equipments;

    void Awake()
    {
        slots = transform.GetComponentsInChildren<Slot>();
        equipments = weaponSlot.GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].equipments = equipments;
        }
    }

    void Start() {
        AcquireItem(paddle);
        AcquireItem(fishingRod);
        AcquireItem(axe);
    }

    void Update() {
        // On-off cursor lock
        if (input.tab)
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (!state.canNotChangeEquipment)
        {
            if (input.alpha1 && slots[0].item != null)
            {
                slots[0].UseItem();
            }
            else if (input.alpha2 && slots[1].item != null)
            {
                slots[1].UseItem();
            }
            else if (input.alpha3 && slots[2].item != null)
            {
                slots[2].UseItem();
            }
            else if (input.alpha4 && slots[3].item != null)
            {
                slots[3].UseItem();
            }
            else if (input.alpha5 && slots[4].item != null)
            {
                slots[4].UseItem();
            }
            else if (input.alpha6 && slots[5].item != null)
            {
                slots[5].UseItem();
            }
            else if (input.alpha7 && slots[6].item != null)
            {
                slots[6].UseItem();
            }
            else if (input.alpha8 && slots[7].item != null)
            {
                slots[7].UseItem();
            }
            else if (input.alpha9 && slots[8].item != null)
            {
                slots[8].UseItem();
            }
            else if (input.alpha0 && slots[9].item != null)
            {
                slots[9].UseItem();
            }
        }
    }

    public void AcquireItem(Item _item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null && slots[i].item.name == _item.name)
            {
                slots[i].IncreaseItemCount();
                return;
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].itemCount == 0)
            {
                slots[i].AddNewItem(_item, 1);
                return;
            }
        }
    }
}
