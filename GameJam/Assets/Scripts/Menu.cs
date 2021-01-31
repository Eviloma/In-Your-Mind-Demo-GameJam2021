using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void Close()
    {
        Application.Quit();
    }

    public void OpenGameJam()
    {
        Application.OpenURL("https://globalgamejam.org/2021/games/your-mind-7");
    }

    public void OpenItchIO()
    {
        Application.OpenURL("https://higherrorua.itch.io/in-your-mind");
    }
}
