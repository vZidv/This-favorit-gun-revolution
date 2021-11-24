using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Enemy : MonoBehaviour
{
    [SerializeField] Transform ShootPoint;
    public GameObject BulletPrefab;
    public float damage;
    [SerializeField] float timeforFire;
    private float timeforFireStart;
    [Header ("ForBoss")]
    public BossType bossType;
    public float timeBeforFire;
    public enum BossType {ItIsNotBoss,Boss_1}

    private GameObject player;
    void Start()
    {
        BulletPrefab.GetComponent<Bullet>().Damage = damage;
        timeforFireStart = timeforFire;
        player = GameObject.Find("Player");
    }

    
    void Update()
    {
        if (bossType == BossType.ItIsNotBoss)
        {
            if (timeforFire <= 0)
            {
                Vector3 pos = player.transform.position - transform.position;
                float rotate = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
                Quaternion rotateZ = Quaternion.Euler(0, 0, rotate);
                Instantiate(BulletPrefab, transform.position, rotateZ);
                timeforFire = timeforFireStart;
            }
            else
            {
                timeforFire -= Time.deltaTime;
            }
        }
        else if (bossType == BossType.Boss_1)
        {
            if (timeBeforFire <= 0)
            {
                if (timeforFire <= 0)
                {
                    Vector3 pos = player.transform.position - transform.position;
                    float rotate = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
                    Quaternion rotateZ = Quaternion.Euler(0, 0, rotate);

                    Instantiate(BulletPrefab, transform.position, rotateZ);
                    rotateZ = Quaternion.Euler(0, 0, -rotate);
                    Instantiate(BulletPrefab, transform.position, rotateZ);

                    timeforFire = timeforFireStart;
                }
                else
                {
                    timeforFire -= Time.deltaTime;
                }
            }
            else
            {
                timeBeforFire -= Time.deltaTime;
            }
        }
    }
}
