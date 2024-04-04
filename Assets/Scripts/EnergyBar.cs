using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{

    static readonly private int maxFlapEnergy = 100;
    private int flapEnergy;
    public float GetEnergy => energyMeter.value;

    [SerializeField] private Slider energyMeter;
    [SerializeField] private Slider hungerMeter;
    private bool updatingEnergy = false;

    // Start is called before the first frame update
    void Awake()
    {
        flapEnergy = maxFlapEnergy;
        //energyMeter = GetComponent<Slider>();
    }

    /*
    // Update is called once per frame
    void Update()
    {

        //RefillEnergy();
    }


    private void RefillEnergy()
    {

        float inc = 5f * Time.deltaTime;

        energyMeter.value += inc;
        flapEnergy = Mathf.RoundToInt(energyMeter.value);
    }
    */


    private IEnumerator UpdateEnergy()
    {
        // Prevent more than one of this coroutine from running
        if (updatingEnergy || energyMeter.value >= energyMeter.maxValue)
        {
            yield break;
        }

        updatingEnergy = true;

        float inc = 1f * Time.deltaTime;

        while (energyMeter.value < energyMeter.maxValue && hungerMeter.value > 0f)
        {
            energyMeter.value += inc;
            hungerMeter.value -= inc;
            flapEnergy = Mathf.RoundToInt(energyMeter.value);
            yield return null;
        }

        updatingEnergy = false;
        yield return null;
    }


    /*
    private IEnumerator UpdateEnergy(float val)
    {
        
        if (updatingEnergy && val > energyMeter.value)
        {
            yield break;
        }
        updatingEnergy = true;

        float t = 0f;
        float inc = 0.1f;
        float duration = 1f;

        while (t < duration)
        {
            energyMeter.value = Mathf.Lerp(energyMeter.value, val, t);
            yield return new WaitForSeconds(inc);
            t += inc;
        }
        updatingEnergy = false;
        // Ends the Loop
        if (energyMeter.value >= energyMeter.maxValue && val >= energyMeter.maxValue)
        {
            yield break;
        }

        // Continues to heal
        yield return EnergyConsumption(-5);
    }
    */
    /*
    private IEnumerator RefillEnergy()
    {
        EnergyConsumption(-10);
    }
    */
    public void HungerRefill(int refill = 10)
    {
        
    }



    public bool EnergyConsumption(int cost)
    {
        bool flap = flapEnergy >= cost;
        if (flap)
            flapEnergy -= cost;
        energyMeter.value = flapEnergy;
        StartCoroutine(UpdateEnergy());
        Debug.Log("Took The Energy!");
        return flap;
    }




}
