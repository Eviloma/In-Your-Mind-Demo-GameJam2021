using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchItem : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerInventory playerInventory;
    [SerializeField] private string interactiveText;

    [SerializeField] private int item = -1;
    [SerializeField] private int tip = -1;
    [SerializeField] private int requireItem = -1;

    private void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        playerInventory = GameObject.FindObjectOfType<PlayerInventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.ChangeStatusInteractive(true);
            playerController.interactiveObject = this.gameObject;
            playerController.ChangeInteractiveText(interactiveText);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController.ChangeStatusInteractive(false);
            playerController.interactiveObject = null;
        }
    }

    public void Search()
    {
        if(requireItem == -1 || playerInventory.openStatus[requireItem] == 2)
        {
            if (item != -1 || tip != -1)
            {
                if (item != -1)
                    playerInventory.openStatus[item] = 2;
                if (tip != -1)
                    playerInventory.openStatus[tip] = 1;
                item = -1;
                tip = -1;
                StartCoroutine(playerController.InteractiveInfoText("Ви знайшли предмет/Підказку"));
            }
            else
            {
                StartCoroutine(playerController.InteractiveInfoText("Пусто..."));
            }
        }
        else
        {
            StartCoroutine(playerController.InteractiveInfoText("У вас немає потрібного інструмента"));
        }
        
    }
}
