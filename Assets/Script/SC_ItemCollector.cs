using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_ItemCollector : MonoBehaviour
{
    private SC_InvController invController;
    // Start is called before the first frame update
    void Start()
    {
        invController = FindObjectOfType<SC_InvController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            SC_Item item = collision.GetComponent<SC_Item>();
            if (item != null)
            {
                bool itemAdded = invController.AddItem(collision.gameObject);

                if (itemAdded)
                {
                    item.PickUp();
                    Destroy(collision.gameObject);
                }
            }
        }
    }


}
