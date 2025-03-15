using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class SC_ItemData : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;
    public Sprite icon;
    public bool isConsumable;
    public int hungerValue;
}
