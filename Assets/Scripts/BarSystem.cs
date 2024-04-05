using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarSystem : MonoBehaviour
{

    static readonly private int maxFlapEnergy = 100;
    private int flapEnergy;
    public float GetEnergy => energyMeter.value;

    private float _regenSpeed = 0.7f;

    [SerializeField] private Slider energyMeter;
    [SerializeField] private Slider hungerMeter;
    [SerializeField] private Slider oxygenMeter;
    private bool _updatingEnergy = false;
    private bool _oxygenLoss = false;

    private Coroutine _oxyCoroutine = null;

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
        if (_updatingEnergy || energyMeter.value >= energyMeter.maxValue)
        {
            yield break;
        }

        _updatingEnergy = true;

        float inc = _regenSpeed * Time.deltaTime;

        while (energyMeter.value < energyMeter.maxValue && hungerMeter.value > 0f)
        {
            energyMeter.value += inc;
            hungerMeter.value -= inc;
            flapEnergy = Mathf.RoundToInt(energyMeter.value);
            yield return null;
        }

        _updatingEnergy = false;
        yield return null;
    }

    private IEnumerator UpdateOxygen(bool loss)
    {
        float to = 100f;

        // Lets say 'from' = 68
        float from = oxygenMeter.value;
        float modifier = 1f;

        float inc = 1.2f * Time.deltaTime;

        oxygenMeter.gameObject.SetActive(true);
        if (loss)
        {
            Debug.Log("We're Losing Oxygen!!");
            to = 0f;
            modifier = -1f;
        }
        // 'diff' can be either 32 or -68
        float diff = (to - from) * modifier; 

        // Delta (Change)
        float delta = 0f;

        while (delta < diff)
        {
            oxygenMeter.value += inc * modifier;
            delta += inc;
            yield return null;
        }

        oxygenMeter.gameObject.SetActive(false);

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
    // Called when triggering fish
    public void HungerRefill(int refill = 10)
    {
        hungerMeter.value += refill;
        StartCoroutine(UpdateEnergy());
    }

    public void EnergyRefill(int refill = 5)
    {
        energyMeter.value += refill;
    }

    public void OxygenCount(bool loss)
    {
        if (_oxygenLoss == loss)
        {
            return;
        }
        _oxygenLoss = loss;

        if (_oxyCoroutine != null)
        {
            StopCoroutine(_oxyCoroutine);
        }
        _oxyCoroutine = StartCoroutine(UpdateOxygen(loss));

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
