using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;
    [Serializable]
    public struct DialogPacks
    {
        public int indexAvatar;
        public string dialogLines;
    }
    public DialogPacks[] dialogPacks;
    public AvatarDatabase avatarDatabase;
    public Image avatarImage;
    public TextMeshProUGUI dialogText;
    public bool isInDialog;
    int index;

    void Start()
    {
        instance = this;
        isInDialog = true;
        StartCoroutine(StartDialog());
    }

    IEnumerator StartDialog()
    {
        yield return  new WaitUntil(() => TransitionManager.instance.isInTransition == false);
        index = 0;
        ShowDialog(index);
        Time.timeScale = 0f;
    }

    void Update()
    {
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && isInDialog && !TransitionManager.instance.isInTransition)
        {
            index++;
            if(index >= dialogPacks.Length && isInDialog)
            {
                // Dialog Finished
                isInDialog = false;

                Time.timeScale = 1f;
                if(SceneManager.GetActiveScene().name == "Prolog")
                {
                    TransitionManager.instance.FadeColor("Gameplay", new Color(0, 0, 0, 0), new Color(0, 0, 0, 1));
                    return;
                }
                else if(SceneManager.GetActiveScene().name == "Epilog")
                {
                    TransitionManager.instance.FadeColor("MainMenu", new Color(0, 0, 0, 0), new Color(0, 0, 0, 1));
                    return;   
                }
            }
            else
            {
                ShowDialog(index);
            }
        }
    }
    public void ShowDialog(int index)
    {
        avatarImage.sprite = avatarDatabase.avatar[dialogPacks[index].indexAvatar - 1];
        dialogText.text = dialogPacks[index].dialogLines;
    }
}
