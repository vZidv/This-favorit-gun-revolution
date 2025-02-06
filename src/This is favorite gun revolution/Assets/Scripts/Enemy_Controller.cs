using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Controller : MonoBehaviour
{
    public float health;
    public float speed;
    public float damage;
    [Header("")]
    [SerializeField] bool isBoss = false;

    public float timeAttack;
    private float timeAttackStart;

    GameObject Player;
    Player_Control playerControl;
    AddRoom room;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerControl = Player.GetComponent<Player_Control>();
        room = GetComponentInParent<AddRoom>();
        timeAttackStart = timeAttack;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Attack(other);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Attack(other);
    }
    void Update()
    {
        gameObject.transform.position =  Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);

        if (timeAttack > 0)
            timeAttack -= Time.deltaTime;
        if (health <= 0)
        {
            if (!isBoss)
            {
                Destroy(gameObject);
                room.enemies.Remove(gameObject);
            }
            else
            {
                Destroy(gameObject);
                RoomVariants roomVar = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
                GameObject.Find("Player_Ui").GetComponent<Ui_Manager>().CoinPlus();
                roomVar.NextLevel();
            }
        }
            
    }
    void Attack(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && timeAttack <= 0)
        {
            playerControl.GetDamage(damage);
            timeAttack = timeAttackStart;
        }
    }
    public void GetDamage(float damage)
    {
        health -= damage;
    }
}
