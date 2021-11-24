using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Manager : MonoBehaviour
{
    public GameObject UpWeapon_Button;
    public Image[] hearts;
    [SerializeField] Sprite heartFull;
    [SerializeField] Sprite heartEmpty;

    Player_Control player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
        UpWeapon_Button = GameObject.Find("Button_UpWeapon");
        UpWeapon_Button.SetActive(false);
    }


    public void UpdateHearts(int heartOfNumber)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < heartOfNumber)           
                hearts[i].sprite = heartFull;           
            else
                hearts[i].sprite = heartEmpty;
        }

    }
}
