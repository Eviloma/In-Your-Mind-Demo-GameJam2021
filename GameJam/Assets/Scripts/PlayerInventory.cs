using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    private GameObject inventory;
    private PlayerInput player;
    private GameObject itemDescription;

    [Range(0, 2)]
    public List<int> openStatus;

    public Sprite questionMark;

    private void Start()
    {
        inventory = this.transform.GetChild(0).gameObject;
        itemDescription = inventory.transform.GetChild(1).gameObject;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }

    public void OpenCloseInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            player.enabled = inventory.activeSelf;
            inventory.SetActive(!inventory.activeSelf);

            if (inventory.activeSelf)
            {
                Text name = itemDescription.transform.GetChild(0).GetChild(0).GetComponent<Text>();
                Image sprite = itemDescription.transform.GetChild(1).GetChild(0).GetComponent<Image>();
                Text description = itemDescription.transform.GetChild(2).GetChild(0).GetComponent<Text>();

                name.text = "";
                sprite.enabled = false;
                description.text = "";

                Transform inventoryCells = inventory.transform.GetChild(0).GetChild(0);

                for (int i = 0; i < inventoryCells.childCount; i++)
                {
                    Image cellImage = inventoryCells.GetChild(i).GetChild(0).GetComponent<Image>();
                    Item item = inventoryCells.GetChild(i).GetComponent<Item>();
                    if (openStatus[i] == 0)
                    {
                        cellImage.sprite = questionMark;
                        cellImage.color = Color.black;
                    }
                    else if (openStatus[i] == 1)
                    {
                        cellImage.sprite = item.itemSprite;
                        cellImage.color = Color.black;
                    }
                    else
                    {
                        cellImage.sprite = item.itemSprite;
                        cellImage.color = Color.white;
                    }
                }
            }
        }
    }

}
