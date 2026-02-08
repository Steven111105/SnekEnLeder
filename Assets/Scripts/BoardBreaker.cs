using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardBreaker : MonoBehaviour
{
    public Image boardPanel;
    public Sprite[] boardSprites;
    Button button;

    int index;
    void Start()
    {
        index = 0;
        button = GetComponent<Button>();
    }

    public void BoardBreak()
    {
        boardPanel.sprite = boardSprites[index];
        index++;
    }
}
