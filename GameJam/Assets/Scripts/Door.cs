using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    private CinemachineConfiner virtualCamera;
    private PlayerController playerController;
    private PlayerInput playerInput;
    private PlayerInventory playerInventory;
    private Animator BlackScreen;
    private AudioSource sfx;
    [SerializeField] private PolygonCollider2D roomTeleport;
    [SerializeField] private Vector3 teleportPosition = new Vector3(0,0,0);
    [SerializeField] private int requireItem = -1;

    [SerializeField] private string interactiveText;
    [Header("SFXs")]
    [SerializeField] private AudioClip openDoor;
    [SerializeField] private AudioClip closedDoor;

    private void Start()
    {
        virtualCamera = GameObject.FindObjectOfType<CinemachineConfiner>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        playerInventory = GameObject.FindGameObjectWithTag("InventorySystem").GetComponent<PlayerInventory>();
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        BlackScreen = GameObject.FindGameObjectWithTag("BlackScreen").GetComponent<Animator>();
        sfx = GameObject.FindGameObjectWithTag("SFX").GetComponent<AudioSource>();
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

    public void Open()
    {
        if (requireItem == -1 || playerInventory.openStatus[requireItem] == 2)
        {
            sfx.clip = openDoor;
            StartCoroutine(Teleport());
        }
        else
        {
            sfx.clip = closedDoor;
            StartCoroutine(playerController.InteractiveInfoText("Двері закриті на ключ"));
        }
        sfx.Play();
    }

    private IEnumerator Teleport()
    {
        playerController.enabled = false;
        playerInput.enabled = false;
        BlackScreen.SetTrigger("Active");
        yield return new WaitForSeconds(1.75f);
        playerController.gameObject.transform.position = teleportPosition;
        virtualCamera.m_BoundingShape2D = roomTeleport;
        BlackScreen.SetTrigger("Active");
        yield return new WaitForSeconds(1.75f);
        playerController.enabled = true;
        playerInput.enabled = true;
    }

}
