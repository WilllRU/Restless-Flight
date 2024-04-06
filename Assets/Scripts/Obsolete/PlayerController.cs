using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private WorldManager man;
    [SerializeField] private BarSystem energy;

    [SerializeField] private InputAction playerControls;
    [SerializeField] private Animator anim;
    private Rigidbody rb;
    [SerializeField] private AnimationCurve glideCurve;

    #region CharacterVariables
    // Character Variables
    private enum WingStates
    {
        None = 0,
        Resting = 1,
        Flap,
        Flapped,
    }

    private WingStates _birdState = WingStates.None;
    private WingStates BirdState
    {
        get
        {
            return _birdState;
        }

        set
        {
            _birdState = value;
            ChangeAnimation(value);
        }
    }
    private float rotationSpeed = 5.0f;
    [SerializeField] private float heightGain = 200.0f;
    private float flapForce = 300f;
    private bool boost = false;

    // The simulated speed of the bird going forward
    // The lower the speed the faster the bird drops
    // Also used to determine where the bird will look
    //[SerializeField] private float _virtualThrust = 100f;

    private static float maxThrust = 500f;

    [SerializeField] private float curLift = 0f;
    [SerializeField] private float turnForce = 0f;
    [SerializeField] private float curSpeed = 0f;
    private static float liftSpeed = 30f;
    private static float minSpeed = 20f;
    private static float maxSpeed = 100f;
    //[SerializeField] private float glideFactor = 10f;
    private static float turnAmount = 40f;
    private static float rollAmount = 45f;
    #endregion
    // Control Variables
    private Vector2 playerInput;
    public Vector2 PlayerInput => playerInput;
    //private Vector3 birdVelocity;

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private float InputResponse()
    {
        return 0f;
    }



    // Start is called before the first frame update
    void Awake()
    {
        curSpeed = minSpeed;
        rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInput = playerControls.ReadValue<Vector2>();
        if (playerInput.y > 0.2f)
        {
            if ((int)BirdState < (int)WingStates.Flap)
            {
                BirdState = WingStates.Flap;
                anim.SetBool("Flap", true);
            }
        }
        else
        {
            BirdState = WingStates.Resting;
        }
        anim.SetFloat("Vertical", playerInput.y);
    }

    private void FixedUpdate()
    {
        BirdMovement();
        BirdRotation();
        //BirdVelocity();
        anim.SetBool("Climbing", rb.velocity.y > -1.0f);
    }

    private void BirdMovement()
    {
        float birdlift = 0f;

        switch (BirdState)
        {
            case WingStates.Flap:
                {
                    if (energy.EnergyConsumption(15))
                    {
                        Debug.Log("Flap!");
                        // Increase the height of the bird
                        // Animator will play the bird flap
                        birdlift += flapForce;
                        curSpeed -= 2f;
                    }

                    if (anim.GetBool("Flap"))
                    {
                        anim.SetBool("Flap", false);
                    }
                    BirdState = WingStates.Flapped;
                    break;
                }
            case WingStates.Flapped:
                {
                    // Keep the height of the bird with gravity having some influence
                    // Animator will transition to a state with the wings spread out

                    birdlift += GlideBehaviour();
                    break;
                }
            case WingStates.Resting:
                {
                    // Close the bird wings and start accelerating the drop
                    

                    break;
                }
            default:
                {

                    break;
                }
        }
        curSpeed -= 0.2f;

        //Debug.Log(birdlift);
        if (boost)
            curSpeed = Mathf.Clamp(curSpeed, minSpeed, maxSpeed * 5);
        else
            curSpeed = Mathf.Clamp(curSpeed, minSpeed, maxSpeed);

        float birdDir = playerInput.x * turnAmount;

        turnForce = Mathf.Lerp(turnForce, birdDir,Time.deltaTime);
        //turnForce = Mathf.Clamp(turnForce, -turnAmount, turnAmount);


        rb.AddForce(new Vector3(0f, birdlift, 0f));
    }

    // Call this when you want to give the bird a boost
    public IEnumerator FlightBoost(float appliedBoost = 100f)
    {
        boost = true;
        curSpeed += appliedBoost;
        yield return new WaitForSeconds(5f);
        boost = false;
    }

    private void BirdRotation()
    {
        /*
        Vector3 targetAngle = new Vector3(rb.velocity.x, rb.velocity.y, virtualSpeed * 5).normalized;
        transform.rotation = Quaternion.LookRotation(targetAngle);
        */
        float pitchAngle = Mathf.LerpAngle(transform.rotation.eulerAngles.x, Mathf.Clamp(-rb.velocity.y, -70, 80), Time.deltaTime * rotationSpeed);
        //float yawAngle = Mathf.LerpAngle(transform.rotation.eulerAngles.y, Mathf.Clamp(playerInput.x * turnAmount, -turnAmount, turnAmount), Time.deltaTime * rotationSpeed);
        float rollAngle = Mathf.LerpAngle(-transform.rotation.eulerAngles.z, Mathf.Clamp(turnForce, -rollAmount, rollAmount), Time.deltaTime * rotationSpeed * 0.5f);
        transform.rotation = Quaternion.Euler(pitchAngle, 0f, -rollAngle);

    }

    public Vector2 BirdVector()
    {
        Vector2 velocity = new Vector2(turnForce/(turnAmount * 2f), (curSpeed/(maxSpeed * 2f)));
        //Debug.Log(velocity);
        return velocity;
    }

    public float SpeedMagnitude()
    {
        return (curSpeed / maxSpeed) * 0.1f;
    }


    private float GlideBehaviour()
    {
        float lift = 0f;
        float inc = 0.01f;

        curSpeed -= rb.velocity.y * 0.1f;

        float liftScale = (curSpeed - liftSpeed) * inc;


        if (liftScale > 0f)
        {
            lift = heightGain * liftScale;
        }


        return lift;
    }


    private void ChangeAnimation(WingStates wing)
    {
        anim.SetInteger("CurState", (int)wing);
    }


    /*
    private Vector2 GlideMovement()
    {
        
        float pitchInRad = transform.rotation.x * Mathf.Deg2Rad;
        float mappedPitch = Mathf.Sin(pitchInRad) * glideFactor;
        Vector3 glidingForce = Vector3.forward * virtualSpeed;

        virtualSpeed += mappedPitch * Time.deltaTime;
        virtualSpeed = Mathf.Clamp(virtualSpeed, 0, man.GetSpeed());


        return Vector3.zero;
    }
    */

}
