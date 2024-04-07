using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(Collider))]
public class FishObject : MonoBehaviour
{
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
            BarSystem energy = FindObjectOfType<BarSystem>();
            energy.HungerRefill(15);
            energy.EnergyRefill(10);

            Destroy(this.gameObject);
        }
    }
}
