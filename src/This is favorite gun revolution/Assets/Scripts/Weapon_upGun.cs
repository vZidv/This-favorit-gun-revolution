using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Weapon_upGun : MonoBehaviour
{
    [SerializeField] string nameWeapon;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_Control player_Con = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();

        if (collision.CompareTag("Player") && player_Con.controlType == Player_Control.ControlType.Android)
        {
            Ui_Manager ui_manger = GameObject.Find("Player_Ui").GetComponent<Ui_Manager>();
            ui_manger.UpWeapon_Button.SetActive(true);
            //Button uPbutton = ui_manger.UpWeapon_Button.GetComponent<Button>();
            EventTrigger uPbutton = ui_manger.UpWeapon_Button.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { UpWeapon(); });
            uPbutton.triggers.Add(entry);
        }

        else if (collision.CompareTag("Player") && player_Con.controlType == Player_Control.ControlType.PC)
        {
            //Player_Control player_Con = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
            if (player_Con.controlType == Player_Control.ControlType.PC)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    UpWeapon();
                }
            }

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player_Control player_Con = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
            if (player_Con.controlType == Player_Control.ControlType.PC)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    UpWeapon();
                }
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player_Control player_Con = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
        GameObject uPButton = GameObject.Find("Button_UpWeapon");

        if (collision.CompareTag("Player") && player_Con.controlType == Player_Control.ControlType.Android)
        {
            uPButton.SetActive(false);
        }
    }
    public void UpWeapon()
    {
        Player_Control player_Con = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Control>();
        Player_Weapon player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Weapon>();
        Weapon_imageManager weaponImage = GameObject.Find("Ui_weaponCell").GetComponent<Weapon_imageManager>();
        GameObject uPButton = GameObject.Find("Button_UpWeapon");

        if (player.swapWeaponTime <= 0)
        {
            if (player.weaponSecond == null)
                weaponImage.SwitchWeaponImage(nameWeapon);

            player.SwitchWeapon(nameWeapon);
            Destroy(gameObject);

            if (player_Con.controlType == Player_Control.ControlType.Android)
                uPButton.SetActive(false);

            player.swapWeaponTime = 1f;
        }
    }
}
