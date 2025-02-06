using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public Direction direction;
   public enum Direction
    {
        Top,
        Down,
        Left,
        Right,
        None
    }

    private RoomVariants roomVar;
    private int rand;
    private bool spawned = false;
    private float waitTime = 5f;
    private GameObject room;
    private void Start()
    {
        roomVar = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.5f);
    }
    public void Spawn()
    {
        if (!spawned)
        {
            spawned = true;
            if (direction == Direction.Top)
            {
                rand = Random.Range(0, roomVar.downRooms.Length);
                room = Instantiate(roomVar.downRooms[rand], transform.position, Quaternion.identity);
            }
            else if (direction == Direction.Down)
            {
                rand = Random.Range(0, roomVar.topRooms.Length);
                room = Instantiate(roomVar.topRooms[rand], transform.position, Quaternion.identity);
            }
            else if (direction == Direction.Right)
            {
                rand = Random.Range(0, roomVar.leftRooms.Length);
                room = Instantiate(roomVar.leftRooms[rand], transform.position, Quaternion.identity);
            }
            else if (direction == Direction.Left)
            {
                rand = Random.Range(0, roomVar.rightRooms.Length);
                room = Instantiate(roomVar.rightRooms[rand], transform.position, Quaternion.identity);
            }   
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RoomPoint") && collision.GetComponent<RoomSpawner>().spawned)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("RoomPoint") && collision.GetComponent<RoomSpawner>().spawned)
        {
            GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>().rooms.Remove(room);
            Destroy(room);
            Destroy(gameObject);
        }
    }
 
}
