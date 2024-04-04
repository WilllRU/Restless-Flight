using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterState : MonoBehaviour
{

    private Collider boxCol;

    private void Awake()
    {
        boxCol = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            RenderSettings.fog = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            RenderSettings.fog = false;
        }
    }
}
