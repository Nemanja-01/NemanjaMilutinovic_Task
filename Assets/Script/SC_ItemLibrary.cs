using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ItemLibrary : MonoBehaviour
{
    public List<SC_Item> itemPrefabs;
    private Dictionary<int, GameObject> itemDic;

    // Start is called before the first frame update
    void Start()
    {
        itemDic = new Dictionary<int, GameObject>();

        for (int i = 0; i< itemPrefabs.Count; i++)
        { 
        
            if (itemPrefabs[i] != null && itemPrefabs[i].itemData != null)
            {
                if (itemPrefabs[i].itemData.ID == 0)
                {
                    itemPrefabs[i].itemData.ID = i + 1;
                }
                if (!itemDic.ContainsKey(itemPrefabs[i].itemData.ID))
                {
                    itemDic[itemPrefabs[i].itemData.ID] = itemPrefabs[i].gameObject;
                }
            }      
        }

        //foreach (SC_Item item in itemPrefabs)
        //{
        //    itemDic[item.itemData.ID] = item.gameObject;
        //}
    }

    public GameObject GetItemPrefab(int itemID)
    {
        if (itemDic.TryGetValue(itemID, out GameObject prefab))
        {
            return prefab;
        }
        else
        {
            Debug.LogWarning($"[SC_ItemLibrary] Item with ID {itemID} not found in dictionary!");
            return null;
        }
    }
}
