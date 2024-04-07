using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObject : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody _rb;
    private Collider _col;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<BirdStateMachine>().BirdDead();
        }
    }
}
