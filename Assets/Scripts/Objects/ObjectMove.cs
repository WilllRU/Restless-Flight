using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    // This is set by the WorldManager Script
    public WorldManager worldMan;
    private Rigidbody rb;
    [SerializeField] float objectSpeed = 180f;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        worldMan = FindObjectOfType<WorldManager>();
    }


    private void FixedUpdate()
    {
        UpdatePosition();
    }

    public virtual void UpdatePosition()
    {
        rb.position = new Vector3(worldMan.GetWorldMovement.x, 0f, worldMan.GetWorldMovement.y * 2f) * objectSpeed + rb.position;
    }

}
