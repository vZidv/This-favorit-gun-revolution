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
    private float waitTime = 3f;
    private void Start()
    {
        roomVar = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        Destroy(gameObject, waitTime);
        Invoke("Spawn", 0.3f);
    }
    public void Spawn()
    {
        if (!spawned)
        {
            if(direction == Direction.Top)
            {
                rand = Random.Range(0, roomVar.downRooms.Length);
                Instantiate(roomVar.downRooms[rand], transform.position, Quaternion.identity);
            }
            else if (direction == Direction.Down)
            {
                rand = Random.Range(0, roomVar.topRooms.Length);
                Instantiate(roomVar.topRooms[rand], transform.position, Quaternion.identity);
            }
            else if (direction == Direction.Right)
            {
                rand = Random.Range(0, roomVar.leftRooms.Length);
                Instantiate(roomVar.leftRooms[rand], transform.position, Quaternion.identity);
            }
            else if (direction == Direction.Left)
            {
                rand = Random.Range(0, roomVar.rightRooms.Length);
                Instantiate(roomVar.rightRooms[rand], transform.position, Quaternion.identity);
            }
            spawned = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RoomPoint") && collision.GetComponent<RoomSpawner>().spawned)
        {
            Destroy(gameObject);
        }
    }
}
