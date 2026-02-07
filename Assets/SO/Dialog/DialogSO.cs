using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogSO", menuName = "ScriptableObjects/DialogSO")]
public class DialogSO : ScriptableObject
{
    public Sprite avatar;
    public string dialogLines;
}
