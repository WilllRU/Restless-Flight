using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private WorldManager man;
    [SerializeField] private EnergyBar energy;

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

    [SerializeField] private float heightGain = 10.0f;

    // The simulated speed of the bird going forward
    // The lower the speed the faster the bird drops
    // Also used to determine where the bird will look
    [SerializeField] private float virtualSpeed = 100f;
    private static float maxSpeed = 500f;

    //[SerializeField] private float glideFactor = 10f;
    private static float turnAmount = 20f;
    private static float rollAmount = 45f;
    #endregion
    // Control Variables
    private Vector2 playerInput;
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
    void Start()
    {
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
    }

    private void BirdMovement()
    {
        Vector2 _birdMove = Vector2.zero;

        switch (BirdState)
        {
            case WingStates.Flap:
                {
                    if (energy.EnergyConsumption(10))
                    {
                        Debug.Log("Flap!");
                        // Increase the height of the bird
                        // Animator will play the bird flap
                        _birdMove.y += heightGain;
                        virtualSpeed -= 2f;
                    }

                    BirdState = WingStates.Flapped;
                    break;
                }
            case WingStates.Flapped:
                {
                    // Keep the height of the bird with gravity having some influence
                    // Animator will transition to a state with the wings spread out

                    _birdMove.y += GlideBehaviour();

                    break;
                }
            case WingStates.Resting:
                {
                    // Close the bird wings and start accelerating the drop
                    virtualSpeed = Mathf.Clamp(virtualSpeed -0.1f,0f, maxSpeed);

                    break;
                }
            default:
                {

                    break;
                }
        }
        
        rb.AddForce(new Vector3(_birdMove.x, _birdMove.y, 0f));
    }

    private void BirdRotation()
    {
        /*
        Vector3 targetAngle = new Vector3(rb.velocity.x, rb.velocity.y, virtualSpeed * 5).normalized;
        transform.rotation = Quaternion.LookRotation(targetAngle);
        */
        float pitchAngle = Mathf.LerpAngle(transform.rotation.eulerAngles.x, Mathf.Clamp(-rb.velocity.y, -70, 80), Time.deltaTime * rotationSpeed);
        float yawAngle = Mathf.LerpAngle(transform.rotation.eulerAngles.y, Mathf.Clamp(playerInput.x * turnAmount, -turnAmount, turnAmount), Time.deltaTime * rotationSpeed);
        float rollAngle = Mathf.LerpAngle(-transform.rotation.eulerAngles.z, Mathf.Clamp(playerInput.x * rollAmount, -rollAmount, rollAmount), Time.deltaTime * rotationSpeed * 0.5f);
        transform.rotation = Quaternion.Euler(pitchAngle, 0f, -rollAngle);

    }


    private float GlideBehaviour()
    {
        float lift = 0.0f;

        


        
        

        virtualSpeed = Mathf.Clamp(virtualSpeed - rb.velocity.y,0,maxSpeed);
        float delta = (virtualSpeed / maxSpeed) * 0.2f;

        lift = glideCurve.Evaluate(delta);

        lift *= heightGain;

        
        if (rb.velocity.y > -1.0f)
        {
            virtualSpeed -= 0.4f;
            lift *= 0.8f;
        }
        


        /*
        if (rb.velocity.y < -4.8f)
        {
            virtualSpeed += 20f;
            lift += 50f;
        }
        if (virtualSpeed > 50f && rb.velocity.y < 4.8f)
        {
            lift += 2f;
            virtualSpeed -= 3f;
        }
        else
        {
            if (virtualSpeed >= 50f)
            {
                lift += 5f;
                virtualSpeed -= 1;
            }
            else
            {
                lift += 4f;
            }

        }
        */
        
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
