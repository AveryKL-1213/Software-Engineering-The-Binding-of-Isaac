using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public Camera mainCamera;
    public PlayerControl player;
    [HideInInspector]
    private void Start()
    {
        player = GameObject.Find("Manager").GetComponent<PlayerControl>();
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void SwitchScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }
    public void OverloadScene()
    {
        QuitPauseGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
    }
    public void QuitPauseGame()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }
}
