using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SC_GameController : MonoBehaviour
{
    private string saveLocation;
    private SC_InvController invController;
    private SC_HotBarController hotBarController;
    // Start is called before the first frame update
    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        invController = FindObjectOfType<SC_InvController>();
        hotBarController = FindObjectOfType<SC_HotBarController>();
        LoadGame();
    }

    public void SaveGame()
    {
        SC_SaveData saveData = new SC_SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            invSaveData = invController.GetInventroyItems(),
            hotBarSaveData = hotBarController.GetHotBarItems(),
        };
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SC_SaveData saveData = JsonUtility.FromJson<SC_SaveData>(File.ReadAllText(saveLocation));

            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;
            
            invController.SetInvItems(saveData.invSaveData);
            hotBarController.SetHotBarItems(saveData.hotBarSaveData);
        }
        else
        {
            SaveGame();
        }
    }
    public void QuitGame()
    {
       Application.Quit();
    }
}
