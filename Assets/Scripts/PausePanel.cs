using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{
    public GameObject pausedPanel;

    bool isActivation = false;
    [HideInInspector]
    public float activeSceond = 0.5f;

    PlayerControl player;

    void Start()
    {
        player = GameManager.Instance.player;
    }
    public void OnQuit()
    {
        Debug.Log("1");
    }
    public void OnResume()
    {
        Debug.Log("2");
    }

    public void OnRestart()
    {
        Debug.Log("3");

    }
    void Update()
    {
     //   Debug.Log("this1");
        if (isActivation)
        {
            if (Input.GetKeyDown(KeyCode.Y))//重新开始
            {
                //Player_Hp = 6;
                GameManager.Instance.QuitPauseGame();
                player.PlayerQuitPause();
                isActivation = false;
                pausedPanel.SetActive(false);
                SceneManager.LoadScene("Game_Fight");
            }
            else if((Input.GetKeyDown(KeyCode.Z)))
            {
                Application.Quit();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isActivation)
            {
              //  Debug.Log("this1");
                EnterPanel();
            }
            else 
            {
                Debug.Log("this2");
                ExitPanel();
            }
        }
    }

    public void EnterPanel()
    {
        GameManager.Instance.PauseGame();
        player.PlayerPause();
        isActivation = true;
        pausedPanel.SetActive(true);
    }

    public void ExitPanel()
    {

        GameManager.Instance.QuitPauseGame();
        player.PlayerQuitPause();
        isActivation = false;
        pausedPanel.SetActive(false);

    }



}
