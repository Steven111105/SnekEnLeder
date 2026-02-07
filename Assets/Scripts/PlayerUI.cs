using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static PlayerUI instance;
    Canvas canvas;
    RectTransform rectTransform;
    public int playerDice;
    public bool movedByManager = false;
    public GameObject playerGameObject;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            playerGameObject = gameObject;
            canvas = GetComponentInParent<Canvas>();
            rectTransform = GetComponent<RectTransform>();
            
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Set parent to stop layout group from messing with position   
        GetComponent<Image>().raycastTarget = false;
        movedByManager = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // GetComponent<Image>().raycastTarget = true;
        // if(!movedByManager)
        // {
        //     // Return to original position but dont change turn
        //     GameUIManager.instance.SnapToSlot(gameObject, GameManager.instance.playerPosition);
        //     return;
        // }
        // else
        // {
        //     // GameManager.instance.CheckPlayerDrag(playerDice, );
        // }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
