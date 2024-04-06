using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSection : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 direction = -Vector3.forward;
    private WorldManager man;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        man = GetComponentInParent<WorldManager>();

    }

    private void FixedUpdate()
    {
        // Grabs the speed from the WorldManager to allow for dynamic change of speed
        rb.MovePosition(transform.position + direction * Time.deltaTime * man.GetSpeed());
    }
}
