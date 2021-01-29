using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [Header("Properies")]
    [SerializeField] private int id;

    [Header("Open Item")]
    public string itemName;
    [TextArea()]
    public string itemDescription;
    public Sprite itemSprite;

    [Header("Closed Item with Tip")]
    [TextArea()]
    public string itemTip;

    private GameObject InventorySystem;
    private GameObject itemDescriptionObject;

    private void Start()
    {
        InventorySystem = GameObject.FindGameObjectWithTag("InventorySystem");
        itemDescriptionObject = InventorySystem.transform.GetChild(0).GetChild(1).gameObject;
    }

    public void View()
    {
        Text name =  itemDescriptionObject.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        Image sprite = itemDescriptionObject.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        Text description = itemDescriptionObject.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        sprite.enabled = true;

        if (InventorySystem.GetComponent<PlayerInventory>().openStatus[id] == 0)
        {
            name.text = "???";
            sprite.sprite = InventorySystem.GetComponent<PlayerInventory>().questionMark;
            sprite.color = Color.white;
            description.text = $"?????";
        }
        else if (InventorySystem.GetComponent<PlayerInventory>().openStatus[id] == 1)
        {
            name.text = "???";
            sprite.sprite = itemSprite;
            sprite.color = Color.black;
            description.text = $"Tip: {itemTip}";
        }
        else
        {
            name.text = itemName;
            sprite.sprite = itemSprite;
            sprite.color = Color.white;
            description.text = $"{itemDescription}";
        }
    }
}
