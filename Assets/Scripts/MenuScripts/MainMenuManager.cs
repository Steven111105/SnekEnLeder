using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public Button playButton;
    public Button quitButton;
    public void Play()
    {
        SceneManager.LoadSceneAsync("Gameplay");
    }

    public void Quit()
    {
        // Call save maybe
        Application.Quit();
    }
}
