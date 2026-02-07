using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Image avatarImage;
    public TextMeshProUGUI dialogText;
    public DialogDatabase dialogDatabase;

    public void ShowDialog(int dialogIndex)
    {
        DialogSO dialog = dialogDatabase.dialogSO[dialogIndex];
        avatarImage.sprite = dialog.avatar;
        dialogText.text = dialog.dialogLines;
    }
}
