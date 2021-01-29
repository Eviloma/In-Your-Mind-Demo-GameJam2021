using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator anim;
    private float inputX;
    private GameObject playerSprite;

    private Text interactiveText;
    private Text interactiveFoundtext;

    [SerializeField] private GameObject useKey;
    [SerializeField] private GameObject interactiveTextObject;
    [SerializeField] private GameObject interactiveFoundObject;

    [Range(1, 10)]
    [SerializeField] private float moveSpeed;

    [HideInInspector] public GameObject interactiveObject;

    public void Start()
    {
        player = this.GetComponent<Rigidbody2D>();
        playerSprite = this.transform.GetChild(0).gameObject;
        anim = playerSprite.GetComponent<Animator>();
        interactiveText = interactiveTextObject.transform.GetChild(0).GetComponent<Text>();
        interactiveFoundtext = interactiveFoundObject.transform.GetChild(0).GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        player.velocity = new Vector2(inputX * moveSpeed, player.velocity.y);
    }

    public void ChangeStatusInteractive(bool status)
    {
        useKey.SetActive(status);
        interactiveTextObject.SetActive(status);
    }
    public void ChangeInteractiveText(string text)
    {
        interactiveText.text = text;
    }

    public IEnumerator InteractiveInfoText(string text)
    {
        interactiveFoundObject.SetActive(true);
        interactiveFoundtext.text = text;
        yield return new WaitForSeconds(2);
        interactiveFoundObject.SetActive(false);
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
        if (inputX < 0) playerSprite.transform.rotation = new Quaternion(0, 180, 0, 0);
        else if (inputX > 0) playerSprite.transform.rotation = new Quaternion(0, 0, 0, 0);
        anim.SetBool("Walk", (inputX != 0));
    }

    public void Use(InputAction.CallbackContext context)
    {
        if (context.started && interactiveObject != null)
        {
            ChangeStatusInteractive(false);
            if (interactiveObject.CompareTag("Door"))
            {
                interactiveObject.GetComponent<Door>().Open();
            }
            else if (interactiveObject.CompareTag("Search"))
            {
                interactiveObject.GetComponent<SearchItem>().Search();
            }
        }
    }
}
