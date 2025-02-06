using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject block;
    bool ok = false;
    private void Start()
    {
        StartCoroutine(Empty());
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        StartCoroutine(wallSwith(collision));

    }
    IEnumerator wallSwith(Collider2D collider)
    {
        yield return new WaitForSeconds(3f);

        if (collider.CompareTag("Wall"))
        {
            GameObject wall = Instantiate(block, transform.position, transform.rotation);
            wall.transform.parent = gameObject.transform.parent.parent;
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Door"))
            ok = true;
    }
    IEnumerator Empty()
    {
        yield return new WaitForSeconds(4f);
        if (!ok)
        {
            GameObject wall = Instantiate(block, transform.position, transform.rotation);
            wall.transform.parent = gameObject.transform.parent.parent;
            Destroy(gameObject);
        }
    }
}
