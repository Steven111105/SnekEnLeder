using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager instance;
    public Transform gridParent;
    GridSlot[] gridSlots;
    public GameObject gridSlotPrefab;

    [ContextMenu("Instantiate Grid Slots")]
    public void InstantiateGridSlots()
    {
        float gridSize = gridParent.GetComponent<RectTransform>().rect.width;
        float gridOffset = gridSize / 2f;
        float slotSize = gridSize / 10f;
        float slotOffset = slotSize / 2f;
        for(int i = 0; i < 100; i++)
        {
            GameObject slot = Instantiate(gridSlotPrefab, gridParent);
            RectTransform rect = slot.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0,0);
            rect.sizeDelta = new Vector2(slotSize, slotSize);
            int row = i / 10;
            if(row % 2 == 0)
            {
                rect.localPosition = new Vector2((i%10) * slotSize + slotOffset - gridOffset, row * slotSize + slotOffset - gridOffset);
            }
            else
            {
                rect.localPosition = new Vector2((9 - (i%10)) * slotSize + slotOffset - gridOffset, row * slotSize + slotOffset - gridOffset);
            }
            slot.GetComponent<GridSlot>().slotIndex = i;
        }
    }

    public void SnapToSlot(GameObject playerObject, int slotIndex)
    {
        playerObject.transform.position = gridSlots[slotIndex].transform.position;
    }
}
