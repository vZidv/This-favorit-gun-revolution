using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorForBoss : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DoorOpening(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        DoorOpening(collision);
    }

    void DoorOpening(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Player_Control player = col.GetComponent<Player_Control>();
            if (player.controlType == Player_Control.ControlType.PC)
            {
                if (Input.GetKey(KeyCode.E))
                    Destroy(col);
            }
            else if(player.controlType == Player_Control.ControlType.Android)
            {
                Ui_Manager ui = GameObject.Find("Player_Ui").GetComponent<Ui_Manager>();
                ui.ButtonDoorForBoss.GetComponent<Button>().onClick.AddListener(() => Destroy(col));
                ui.ButtonDoorForBoss.SetActive(true);
            }
        }
    }
    private void Destroy(Collider2D col)
    {
        Player_Control player = col.GetComponent<Player_Control>();
        if (player.controlType == Player_Control.ControlType.Android)
        {
            Ui_Manager ui = GameObject.Find("Player_Ui").GetComponent<Ui_Manager>();
            ui.ButtonDoorForBoss.SetActive(false);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }
}
