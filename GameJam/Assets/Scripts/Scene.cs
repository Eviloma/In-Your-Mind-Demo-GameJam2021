using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    [SerializeField] private Text sub;
    [SerializeField] private PlayerInput input;
    [SerializeField] private AudioSource sfx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        input.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        StartCoroutine(StartScene());
    }

    private IEnumerator StartScene()
    {
        sfx.enabled = false;
        input.enabled = false;
        sub.gameObject.SetActive(true);
        sub.text = "";
        yield return new WaitForSeconds(1);
        sub.text = "Психіатр: Чи стало вам краще, Місіс Коул?";
        yield return new WaitForSeconds(4);
        sub.text = "";
        yield return new WaitForSeconds(1);
        sub.text = "Місіс Коул: Так";
        yield return new WaitForSeconds(4);
        sub.text = "";
        yield return new WaitForSeconds(1);
        sub.text = "The End?";
        yield return new WaitForSeconds(4);
        sub.text = "";
        yield return new WaitForSeconds(1);
        sub.text = "Develop by Eviloma";
        yield return new WaitForSeconds(4);
        sub.text = "";
        yield return new WaitForSeconds(1);
        sub.text = "Special for Ukraine GameJam 2021";
        yield return new WaitForSeconds(4);
        sub.text = "";
        yield return new WaitForSeconds(1);
        sub.text = "Thanks for playing!";
        yield return new WaitForSeconds(5);
        Application.Quit();
    }
}