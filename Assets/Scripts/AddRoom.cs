using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    [Header("Walls")]
    public GameObject[] walls;
    public GameObject wallEffect;
    public GameObject door;
    public GameObject[] DoorForBoss;

    [Header("Enemies")]
    public GameObject[] enemiseTypes;
    public Transform[] enemiseSpawners;

    [Header("Powerups")]
    public GameObject healthPotion;

    [HideInInspector] public List<GameObject> enemies;

    private RoomVariants roomVar;
    private bool spawned;
    private bool wallsDestroyed;
    [HideInInspector] public bool IsBossRoom  = false;

    private void Start()
    {
        roomVar = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
        roomVar.rooms.Add(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !spawned)
        {            
            spawned = true;
            if (IsBossRoom)
            {
                int rand = Random.Range(0, roomVar.bossType.Length);
                Instantiate(roomVar.bossType[rand], transform.position, Quaternion.identity);
            }
            else
            {
                int rand = Random.Range(0, 100);
                for (int i = 0; i < enemiseSpawners.Length; i++)
                {

                    if (rand <= 60)
                    {
                        GameObject enemyType = enemiseTypes[Random.Range(0, enemiseTypes.Length)];
                        GameObject enemy = Instantiate(enemyType, enemiseSpawners[i].position, Quaternion.identity) as GameObject;
                        enemy.transform.parent = transform;
                        enemies.Add(enemy);
                    }


                }
                StartCoroutine(CheckEnemies());
            }
        }
        
    }
    public void DoorOn()
    {
        for (int i = 0; i < DoorForBoss.Length; i++)
        {
            if (DoorForBoss[i] != null)
                DoorForBoss[i].SetActive(true);
        }
    }
    IEnumerator CheckEnemies()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        DestroyWalls();
    }
    public void DestroyWalls()
    {
        foreach (GameObject wall in walls)
        {
            if(wall != null)
            {
                Destroy(wall);
            }
        }
        wallsDestroyed = true;
        Debug.Log(wallsDestroyed);
    }
   
}
