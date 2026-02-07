using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;
    Canvas canvas;
    RectTransform rectTransform;
    public GameObject playerGameObject;
    void Awake()
    {
        instance = this;
        playerGameObject = gameObject;
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }
}
