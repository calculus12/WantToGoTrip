using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public Item item;
    public Image itemImage;
    public int itemCount;
    [SerializeField] TextMeshProUGUI textCount;
    [SerializeField] Transform player;
    [HideInInspector] public Transform[] equipments;
    Image slotImage;

    [SerializeField] float dropMaxY;

    void Awake()
    {
        slotImage = GetComponent<Image>();
    }

    public void AddNewItem(Item newItem, int count)
    {
        item = newItem;
        itemImage.sprite = newItem.itemImage;
        itemCount = count;
        textCount.text = itemCount.ToString();
        SetImageAlpha(1f);
    }

    public void RemoveItem()
    {
        item = null;
        itemImage.sprite = null;
        itemCount = 0;
        textCount.text = "";
        SetImageAlpha(0f);
    }

    public void IncreaseItemCount()
    {
        itemCount++;
        textCount.text = itemCount.ToString();
    }

    public void DecreaseItemCount()
    {
        itemCount--;
        textCount.text = itemCount.ToString();
        if (itemCount <= 0)
        {
            RemoveItem();
        }
    }

    public void UseItem()
    {
        StartCoroutine(EmphasizeSlot(0.8f, 0.8f, 0f));
        switch (item.itemType)
        {
            case Item.ItemType.Equipment:
                for (int i = 1; i < equipments.Length; i++) // Varable i starts from 1 to exclude itself
                {
                    // Wear new equipment
                    if (equipments[i].name == item.name)
                    {
                        equipments[i].gameObject.SetActive(true);
                    }
                    // Unwear old equipment
                    else if (equipments[i].gameObject.activeSelf)
                    {
                        equipments[i].gameObject.SetActive(false);
                    }
                }
                break;
            case Item.ItemType.Consumption:
                Debug.Log("Activate item (health recovery, satiety recovery, etc...)");
                DecreaseItemCount();
                break;
        }
    }

    void ChangeSlot()
    {
        Item tempItem = item;
        int tempItemCount = itemCount;
        AddNewItem(SlotToDrag.instance.movingSlot.item, SlotToDrag.instance.movingSlot.itemCount);
        if (tempItemCount != 0)
        {
            SlotToDrag.instance.movingSlot.AddNewItem(tempItem, tempItemCount);
        }
        else
        {           
            SlotToDrag.instance.movingSlot.RemoveItem();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null && transform.parent.gameObject.activeSelf)
        {
            SlotToDrag.instance.BeginMove(this, itemImage);
            SlotToDrag.instance.transform.position = eventData.position;
            SetImageRGB(0.5f, 0.5f, 0.5f);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        SlotToDrag.instance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetImageRGB(1f, 1f, 1f);
        if (eventData.position.y > dropMaxY) // drop item
        {
            // If player is wearing equipment, unwear it
            if (item.itemType == Item.ItemType.Equipment)
            {
                for (int i = 1; i < equipments.Length; i++) // Varable i starts from 1 to exclude itself
                {
                    if (equipments[i].name == item.name)
                    {
                        equipments[i].gameObject.SetActive(false);
                    }
                }
            }
            // Drop item
            Instantiate(item.itemObj, player.position + player.forward * 1.5f + Vector3.up * 1.5f, Quaternion.identity);
            DecreaseItemCount();
        }
        SlotToDrag.instance.EndMove();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (SlotToDrag.instance.movingSlot != null)
        {
            ChangeSlot();
        }
    }
    
    void SetImageAlpha(float alpha)
    {
        Color color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;
    }

    void SetImageRGB(float r, float g, float b)
    {
        Color color = itemImage.color;
        color.r = r;
        color.g = g;
        color.b = b;
        itemImage.color = color;
    }

    IEnumerator EmphasizeSlot(float r, float g, float b)
    {
        // Change color
        Color color = slotImage.color;
        float tmpR = color.r;
        float tmpG = color.g;
        float tmpB = color.b;
        color.r = r;
        color.g = g;
        color.b = b;
        slotImage.color = color;
        
        // Return original color
        yield return new WaitForSeconds(0.2f);
        color.r = tmpR;
        color.g = tmpG;
        color.b = tmpB;
        slotImage.color = color;
    }
}
