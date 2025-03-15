using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SC_HotBarController : MonoBehaviour
{
    public GameObject hotBar;
    public GameObject slotPrefab;
    public int slotCount = 3;

    private SC_ItemLibrary itemLibrary;

    private Key[] hotBarKeys;
    private void Awake()
    {
        itemLibrary = FindObjectOfType<SC_ItemLibrary>();

        hotBarKeys= new Key[slotCount];
        for(int i = 0; i < slotCount; i++)
        {
            hotBarKeys[i] = i < 9 ? (Key)((int)Key.Digit1 + i) : Key.Digit0;
        }
    }

    private void Update()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (Keyboard.current[hotBarKeys[i]].wasPressedThisFrame)
            {
                UseItemInSlot(i);
            }
        }
    }

    void UseItemInSlot(int index)
    {
        SC_Slot slot = hotBar.transform.GetChild(index).GetComponent<SC_Slot>();
        if (slot.currentItem != null)
        {
            SC_Item item = slot.currentItem.GetComponent<SC_Item>();
            item.UseItem();
        }
    }
    public List<SC_InvSaveData> GetHotBarItems()
    {
        List<SC_InvSaveData> hotbarDatas = new List<SC_InvSaveData>();
        foreach (Transform slotTransform in hotBar.transform)
        {
            SC_Slot slot = slotTransform.GetComponent<SC_Slot>();
            if (slot.currentItem != null)
            {
                SC_Item item = slot.currentItem.GetComponent<SC_Item>();
                hotbarDatas.Add(new SC_InvSaveData { itemID = item.itemData.ID, slotIndex = slotTransform.GetSiblingIndex() });
            }
        }
        return hotbarDatas;

    }
    public void SetHotBarItems(List<SC_InvSaveData> inventorySaveData)
    {
        foreach (Transform child in hotBar.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, hotBar.transform);
        }

        foreach (SC_InvSaveData data in inventorySaveData)
        {
            if (data.slotIndex < slotCount)
            {
                SC_Slot slot = hotBar.transform.GetChild(data.slotIndex).GetComponent<SC_Slot>();
                GameObject itemPrefab = itemLibrary.GetItemPrefab(data.itemID);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                }

            }
        }
    }
    public void ClearHotbar()
    {
        foreach (Transform slot in hotBar.transform)
        {
            SC_Slot slotPlace = slot.GetComponent<SC_Slot>();
            if (slotPlace.currentItem != null)
            {
                Destroy(slotPlace.currentItem);
                slotPlace.currentItem = null;
            }
        }
    }
}
