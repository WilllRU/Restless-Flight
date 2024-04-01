using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{

    static readonly private int maxFlapEnergy = 100;
    private int flapEnergy;
    public int GetEnergy => flapEnergy;

    private Slider energyMeter;

    // Start is called before the first frame update
    void Start()
    {
        flapEnergy = maxFlapEnergy;
        energyMeter = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateEnergy(int val)
    {
        energyMeter.value = val;
    }


    public bool EnergyConsumption(int cost)
    {
        bool flap = flapEnergy >= 10f;
        if (flap)
            flapEnergy -= cost;

        UpdateEnergy(flapEnergy);
        return flap;
    }




}
