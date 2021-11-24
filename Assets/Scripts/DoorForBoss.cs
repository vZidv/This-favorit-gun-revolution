using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    Destroy(gameObject);
            }
        }
    }
}
