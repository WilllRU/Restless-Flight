using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{

    static readonly private int maxFlapEnergy = 100;
    private int flapEnergy;
    public float GetEnergy => energyMeter.value;

    private Slider energyMeter;

    // Start is called before the first frame update
    void Awake()
    {
        flapEnergy = maxFlapEnergy;
        energyMeter = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator UpdateEnergy(float val)
    {
        

        // Ends the Loop
        if (energyMeter.value >= energyMeter.maxValue && val >= energyMeter.maxValue)
        {
            yield break;
        }
        float t = 0f;
        float inc = 0.1f;
        float duration = 1f;
        float initialVal = energyMeter.value;


        while (t < duration)
        {
            energyMeter.value = Mathf.Lerp(energyMeter.value, val, t);
            yield return new WaitForSeconds(inc);
            t += inc;
        }

        // Continues to heal
        yield return EnergyConsumption(-5);
    }

    private IEnumerator RefillEnergy()
    {
        yield return new WaitForSeconds(1f);
        EnergyConsumption(-10);
    }

    public bool EnergyConsumption(int cost)
    {
        bool flap = flapEnergy >= 10f;
        if (flap)
            flapEnergy -= cost;

        StartCoroutine(UpdateEnergy(flapEnergy));
        return flap;
    }




}
