using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SnakeUI : MonoBehaviour
{
    public static SnakeUI instance;
    Slider snakeHPBar;
    public int snakeMaxHP = 15;

    public int snakeCurrentHP;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        snakeHPBar = GetComponentInChildren<Slider>();
        snakeHPBar.gameObject.SetActive(false);
        snakeCurrentHP = snakeMaxHP;
    }

    public void TakeDamage(int damage)
    {
        snakeHPBar.gameObject.SetActive(true);
        snakeCurrentHP -= damage;
        if(snakeCurrentHP < 0)
        {
            snakeCurrentHP = 0;
            GameManager.instance.HandleSnakeKilled();
        }
        snakeHPBar.value = (float)snakeCurrentHP / (float)snakeMaxHP;
    }
}
