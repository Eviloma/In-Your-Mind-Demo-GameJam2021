using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D player;
    private Animator anim;
    private float inputX;
    private GameObject playerSprite;

    [Range(1, 10)]
    [SerializeField] private float moveSpeed;

    [HideInInspector] public GameObject interactiveObject;

    public void Start()
    {
        player = this.GetComponent<Rigidbody2D>();
        playerSprite = this.transform.GetChild(0).gameObject;
        anim = playerSprite.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        player.velocity = new Vector2(inputX * moveSpeed, player.velocity.y);
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
            if (interactiveObject.CompareTag("Door"))
            {
                interactiveObject.GetComponent<Door>().Open();
            }
        }
    }
}
