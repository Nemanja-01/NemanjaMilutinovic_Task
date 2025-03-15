using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_TabControll : MonoBehaviour
{
    public Image[] tabImg;
    public GameObject[] pages;
    // Start is called before the first frame update
    void Start()
    {
        ActiveTab(0);

    }

    public void ActiveTab(int tabNo)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
            tabImg[i].color = Color.white;
        }
        pages[tabNo].SetActive(true);
        tabImg[tabNo].color = Color.gray;
    }

}
