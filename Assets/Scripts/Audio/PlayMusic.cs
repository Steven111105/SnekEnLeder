using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public string musicName;

    void Start()
    {
        AudioManager.instance.PlayBGM(musicName);
    }
}
