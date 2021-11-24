using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    [SerializeField] Vector3 cameraChangePos;
    [SerializeField] Vector3 playerChangePos;
    private Camera cam;
    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position += playerChangePos;
            cam.transform.position += cameraChangePos;
        }
    }
}
