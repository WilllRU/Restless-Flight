using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObjects : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float teleportToXPos;
    [SerializeField] private Rigidbody rb;
    private BoxCollider col;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Objects"))
        {
            Rigidbody rigid = other.GetComponent<Rigidbody>();
            Vector3 pos = rigid.position;
            pos.x = teleportToXPos;
            rigid.position = pos;

        }
    }
}
