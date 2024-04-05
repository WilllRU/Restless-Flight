using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    // This is set by the WorldManager Script
    public WorldManager worldMan;
    private Rigidbody rb;
    [SerializeField] float objectSpeed = 1f;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        worldMan = FindObjectOfType<WorldManager>();
    }


    private void FixedUpdate()
    {
        rb.AddForce(worldMan.GetWorldMovement.x * objectSpeed,0f, worldMan.GetWorldMovement.y * objectSpeed,ForceMode.VelocityChange);
        //rb.MovePosition(new Vector3(worldMan.GetWorldMovement.x * objectSpeed, 0f, worldMan.GetWorldMovement.y * objectSpeed) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BarSystem energy = FindObjectOfType<BarSystem>();
            energy.HungerRefill();
            energy.EnergyRefill();
            Destroy(this.gameObject);
        }
    }

}
