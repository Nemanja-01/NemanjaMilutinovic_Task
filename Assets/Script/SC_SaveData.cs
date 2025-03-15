using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SC_SaveData
{
    public Vector3 playerPosition;
    public List<SC_InvSaveData> invSaveData;
    public List<SC_InvSaveData> hotBarSaveData;
}
