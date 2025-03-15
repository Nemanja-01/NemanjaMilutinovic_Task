using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_InvController : MonoBehaviour
{
    private SC_ItemLibrary itemLibrary;
    public SC_ItemData itemData;
    public GameObject invPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        itemLibrary = FindObjectOfType<SC_ItemLibrary>();
        //for (int i = 0; i < slotCount; i++)
        //{
        //    SC_Slot slot = Instantiate(slotPrefab, invPanel.transform).GetComponent<SC_Slot>();
        //    if (i < itemPrefabs.Length)
        //    {
        //        GameObject item = Instantiate(itemPrefabs[i], slot.transform);
        //        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //        slot.currentItem = item;
        //    }
        //}
    }

    public bool AddItem(GameObject itemPrefab)
    {
        foreach (Transform slotTransform in invPanel.transform)
        {
            SC_Slot slot = slotTransform.GetComponent<SC_Slot>();
            if (slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slotTransform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newItem;
                return true;
            }
        }
        Debug.Log("Inv is full!!");
        return false;
    }

    public List<SC_InvSaveData> GetInventroyItems()
    {
        List<SC_InvSaveData> invSaveDatas = new List<SC_InvSaveData>();
        foreach(Transform slotTransform in invPanel.transform)
        {
            SC_Slot slot = slotTransform.GetComponent<SC_Slot>();
            if (slot.currentItem != null)
            {
                SC_Item item = slot.currentItem.GetComponent<SC_Item>();
                invSaveDatas.Add(new SC_InvSaveData { itemID = item.itemData.ID, slotIndex = slotTransform.GetSiblingIndex() });
            }
        }
        return invSaveDatas;

    }
    public void SetInvItems(List<SC_InvSaveData> inventorySaveData)
    {
        foreach(Transform child in invPanel.transform)
        {
            Destroy(child.gameObject);
        }    

        for(int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, invPanel.transform);
        }

        foreach(SC_InvSaveData data in inventorySaveData)
        {
            if(data.slotIndex < slotCount)
            {
                SC_Slot slot = invPanel.transform.GetChild(data.slotIndex).GetComponent<SC_Slot>();
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
    public void ClearInventory()
    {
        foreach(Transform slot in invPanel.transform)
        {
            SC_Slot slotPlace = slot.GetComponent<SC_Slot>();
            if(slotPlace.currentItem != null) 
            {
                Destroy(slotPlace.currentItem);
                slotPlace.currentItem=null;
            }
        }
    }
}
