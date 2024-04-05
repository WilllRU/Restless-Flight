using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterState : MonoBehaviour
{

    private Collider boxCol;
    [SerializeField] private BarSystem bar;

    private void Awake()
    {
        boxCol = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            RenderSettings.fog = true;
            //ActivateOxygen(true);
        }
        if (other.CompareTag("Player"))
        {
            bar.OxygenCount(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            RenderSettings.fog = false;
            //ActivateOxygen(false);
        }
        if (other.CompareTag("Player"))
        {
            bar.OxygenCount(false);
        }

    }

}
