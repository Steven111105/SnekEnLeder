using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridSlot : MonoBehaviour, IDropHandler
{
    public int slotIndex;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop called on slot: " + slotIndex);
        GameManager.instance.CheckPlayerDrag(PlayerUI.instance.playerDice, slotIndex);
        // Tell PlayerUI they dropped on a tile
        PlayerUI.instance.movedByManager = true;
    }
}
