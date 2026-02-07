using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public bool isPlayed = true;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPlayed)
            {
                gameObject.SetActive(true);
                Time.timeScale = 0f;
                isPlayed = false;
            }
            else
            {
                gameObject.SetActive(false);
                Time.timeScale = 1f;
                isPlayed = true;
            }
        }
    }
    
    public void BackTOMenu()
    {
        // TransitionManager.LoadAsnyc(TransitionManager.SceneEnum.MainMenu);
    }
}
