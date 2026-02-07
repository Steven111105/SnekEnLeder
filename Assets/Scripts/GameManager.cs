using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    
    public void HandleSnakeKilled()
    {
        Debug.Log("Snake has been killed!");
        GameUIManager.instance.HideSnake();

        // Change scene to grapling
        SceneManager.LoadSceneAsync("GrapplingScene");
    }
}
