using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_Item : MonoBehaviour
{
    public SC_ItemData itemData;

    public virtual void PickUp()
    {
        Sprite itemIcon = GetComponent<Image>().sprite;
        if(SC_ItemUIDisplay.Instance != null )
        {
            SC_ItemUIDisplay.Instance.ShowItemInfo(itemData.Name, itemIcon, itemData.Description);
        }
    }

    public virtual void UseItem()
    {
        SC_SoundManager.Play("Food");
        if (SC_PlayerStats.Instance != null)
        {
            SC_PlayerStats.Instance.IncreseHunger(itemData.hungerValue);
        }

        Destroy(gameObject);
    }
}
