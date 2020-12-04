using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    private int MenuScene = 0;

    public void ClickedOnContinue()
    {
        GameManager.Instance.UnpauseGame();//back to game
    }

    public void ClickedOnMenu()
    {
        Time.timeScale = 1;//"unpause" the game
        SceneManager.LoadScene(MenuScene);//go to menu scene
    }
}
