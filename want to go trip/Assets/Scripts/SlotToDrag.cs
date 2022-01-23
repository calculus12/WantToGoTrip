using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotToDrag : MonoBehaviour
{
    static public SlotToDrag instance;
    public Slot movingSlot;
    Image itemImage;

    void Awake() {
        itemImage = GetComponent<Image>();
        instance = this;
    }

    public void BeginMove(Slot slot, Image image)
    {
        movingSlot = slot;
        itemImage.sprite = image.sprite;
        SetImageAlpha(1f);
    }

    public void EndMove()
    {
        movingSlot = null;
        itemImage.sprite = null;
        SetImageAlpha(0f);
    }

    void SetImageAlpha(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }
}
