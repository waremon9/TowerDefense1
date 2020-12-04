using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    private int level1Scene = 1;

    public void ClickedOnPlay()
    {
        SceneManager.LoadScene(level1Scene);
    }

    public void ClickedOnExit()
    {
        Application.Quit();
    }
}
