using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SnakeAndLadderSO", menuName = "ScriptableObjects/SnakeAndLadderSO")]
public class SnakeAndLadderSO : ScriptableObject
{
    [System.Serializable]
    public struct Information
    {
        public int startPos;
        public int endPos;
    }

    public List<Information> snakes = new List<Information>();
    public List<Information> ladders = new List<Information>();
}
