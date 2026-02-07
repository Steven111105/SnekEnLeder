using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogDatabase", menuName = "ScriptableObjects/DialogDatabase")]
public class DialogDatabase : ScriptableObject
{
    public DialogSO[] dialogSO;
}
