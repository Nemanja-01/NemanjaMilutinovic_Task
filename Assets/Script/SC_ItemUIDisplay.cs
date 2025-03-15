using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SC_ItemUIDisplay : MonoBehaviour
{
    public static SC_ItemUIDisplay Instance {  get; private set; }

    public GameObject infoPrefab;
    public int maxInfo = 2;
    public float infoDuration = 2f;

    private readonly Queue<GameObject> activeInfo = new();

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

    public void ShowItemInfo(string itemName, Sprite itemIcon, string desc)
    {
        GameObject newInfo = Instantiate(infoPrefab, transform);
        TMP_Text[] texts = newInfo.GetComponentsInChildren<TMP_Text>();
        if (texts.Length >= 2)
        {
            texts[0].text = itemName;
            texts[1].text = desc;
        }

        Image itemImage = newInfo.transform.Find("ItemImage")?.GetComponent<Image>();
        if(itemImage)
        {
            itemImage.sprite = itemIcon;
        }
        activeInfo.Enqueue(newInfo);
        if (activeInfo.Count > maxInfo)
        {
            Destroy(activeInfo.Dequeue());
        }

        StartCoroutine(FadeOut(newInfo));
    }

    private IEnumerator FadeOut(GameObject info)
    {
        yield return new WaitForSeconds(infoDuration);
        if (info == null) yield break;

        Destroy(info);

    }
}
