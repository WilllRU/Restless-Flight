using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishObject : ObjectMove
{
    public override void OnTriggerEnter(Collider other)
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
