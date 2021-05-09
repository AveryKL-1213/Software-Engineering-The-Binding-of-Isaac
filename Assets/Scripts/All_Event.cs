using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class All_Event : MonoBehaviour {
    public void StartGame ( )
    {
        SceneManager.LoadScene ("Game_Fight");
    }
}
