using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public float speed;
    public float distance;
    public float lifeTime;
    public LayerMask whatIsSolid;

    [SerializeField] bool IsEnemyBull = false;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if(hitInfo.collider != null) 
        {
            if (hitInfo.collider.CompareTag("Wall"))
                Destroy(gameObject);
            if (!IsEnemyBull)
            {
                if (hitInfo.collider.CompareTag("Enemy"))
                {
                    hitInfo.collider.GetComponent<Enemy_Controller>().GetDamage(Damage);
                    Destroy(gameObject);
                }
            }
            else
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    hitInfo.collider.GetComponent<Player_Control>().GetDamage(Damage);
                    Destroy(gameObject);
                }
            }
        }
    }
}
