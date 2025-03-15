using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MenuControl : MonoBehaviour
{
    public GameObject menu;
    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            menu.SetActive(!menu.activeSelf);
        }

    }
}