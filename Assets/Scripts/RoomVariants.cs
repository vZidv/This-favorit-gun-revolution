using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVariants : MonoBehaviour
{
    public GameObject[] topRooms;
    public GameObject[] downRooms;
    public GameObject[] rightRooms;
    public GameObject[] leftRooms;

    public GameObject[] weaponType;
    public GameObject[] bossType;
    [HideInInspector] public List<GameObject> rooms;
    void Start()
    {
        StartCoroutine(RandomSpawner());
    }
    IEnumerator RandomSpawner()
    {
        yield return new WaitForSeconds(5f);
        AddRoom lastRoom = rooms[rooms.Count - 1].GetComponent<AddRoom>();
        int randWeapon = Random.Range(0,weaponType.Length);

        Instantiate(weaponType[randWeapon], rooms[rooms.Count - 2].transform.position, Quaternion.identity);

        lastRoom.IsBossRoom = true;
        lastRoom.DoorOn();
        Debug.Log(rooms[rooms.Count - 1].name);
    }
}
