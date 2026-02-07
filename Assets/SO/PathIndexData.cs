using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PathIndexData", menuName = "ScriptableObjects/PathIndexData", order = 1)]
public class PathIndexData : ScriptableObject
{
    public int finishIndex;
    public List<int> pathIndexs;
}
