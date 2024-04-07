using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportObjects : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform teleportToPos;
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
            pos.x = teleportToPos.position.x - 10f;
            rigid.position = pos;

        }
    }
}
