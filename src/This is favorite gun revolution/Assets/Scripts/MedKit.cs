using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    [SerializeField] float healthPlus;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
           Player_Control player = collision.GetComponent<Player_Control>();
            player.health += healthPlus;
            GameObject.Find("Player_Ui").GetComponent<Ui_Manager>().UpdateHearts(Mathf.RoundToInt(player.health));

            Destroy(gameObject);
        }
    }
}
