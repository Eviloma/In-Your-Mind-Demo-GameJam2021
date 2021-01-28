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
    private Animator BlackScreen;

    [SerializeField] private GameObject useKey;
    [SerializeField] private PolygonCollider2D roomTeleport;
    [SerializeField] private Vector3 teleportPosition = new Vector3(0,0,0);
    [SerializeField] private bool isOpen;

    private void Start()
    {
        virtualCamera = GameObject.FindObjectOfType<CinemachineConfiner>();
        playerController = GameObject.FindObjectOfType<PlayerController>();
        playerInput = GameObject.FindObjectOfType<PlayerInput>();
        BlackScreen = GameObject.FindGameObjectWithTag("BlackScreen").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            useKey.SetActive(true);
            playerController.interactiveObject = this.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            useKey.SetActive(false);
            playerController.interactiveObject = null;
        }
    }

    public void Open()
    {
        if (isOpen)
        {
            StartCoroutine(Teleport());
        }
        
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
