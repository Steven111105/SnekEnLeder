using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AvatarDatabase", menuName = "ScriptableObjects/AvatarDatabase")]
public class AvatarDatabase : ScriptableObject
{
    public List<Sprite> avatar;
}
