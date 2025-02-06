using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapn_Stats : MonoBehaviour
{
    [Header("Settings")]
    public float damage;
    public Transform ShootPoint;
    public float timeForFireStart;
    public float scatter = 0;
    [Header("ShootGun")]
    [SerializeField] bool IsShootGun;
    [SerializeField] int numberBullet;
    [Header("Don't touch")]
    public float timeForFire;

    private void Start()
    {      
        timeForFire = timeForFireStart;
        ShootPoint = gameObject.transform.Find("pointShot");
    }
    public void Shoot(GameObject bulletPrefab)
    {
        bulletPrefab.GetComponent<Bullet>().Damage = damage;

        if(scatter == 0)
        Instantiate(bulletPrefab, ShootPoint.transform.position, ShootPoint.transform.rotation);
        else
        {
            if (IsShootGun)
                ShootGunShoot(numberBullet,bulletPrefab);
            else
            {
                float scatterNow = Random.Range(-scatter, scatter);
                Quaternion angle;
                Instantiate(bulletPrefab, ShootPoint.transform.position,angle = Quaternion.Euler(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z + scatterNow));
            }
        }
           
    }
    void ShootGunShoot(int numberBullet,GameObject bullet)
    {
        bullet.GetComponent<Bullet>().Damage = damage;
        for (int i = 0; i < numberBullet; i++)
        {
            float scatterNow = Random.Range(-scatter, scatter);
            Quaternion angle  ;
            Instantiate(bullet, ShootPoint.transform.position,angle = Quaternion.Euler(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z + scatterNow));
        }
       
    }
}
