using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SC_PlayerStats : MonoBehaviour
{
    public int currentHunger = 0;
    public int maxHunger = 5;

    public static SC_PlayerStats Instance;
    private SC_InvController invController;
    private SC_HotBarController hotBarController;

    public Slider hungerSlider;
    public TMP_Text restartText;
    public TMP_Text winText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = currentHunger;
        winText.gameObject.SetActive(false);
        invController = FindObjectOfType<SC_InvController>();
        hotBarController = FindObjectOfType<SC_HotBarController>();
    }
    public void IncreseHunger(int value)
    {
        currentHunger += value;
        hungerSlider.value = currentHunger;

        if (currentHunger >= maxHunger)
        {
            EndGame();
        }
    }
    public void EndGame()
    {
        winText.text = "You survived!!!";
        winText.gameObject.SetActive(true);
        restartText.text = "Press R to Restart";
        restartText.gameObject.SetActive(true);

        SC_MusicManager.PauseBackgroundMusic();
        Time.timeScale = 0;
        AudioListener.pause = true;
    }
    private void Update()
    {
        if (Time.timeScale == 0 && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void RestartGame()
    {
        if (invController != null)
        { 
            invController.ClearInventory(); 
        }
        if (hotBarController != null)
        {
            hotBarController.ClearHotbar();
        }
        SC_MusicManager.PlayBcgMusic(true);
        Time.timeScale = 1; 
        AudioListener.pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
