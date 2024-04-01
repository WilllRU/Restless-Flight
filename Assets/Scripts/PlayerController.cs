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


    // Character Variables
    private enum WingStates
    {
        None = 0,
        Resting = 1,
        Flap,
        Flapped,
    }
    private WingStates birdState = WingStates.None;
    private float rotationSpeed = 5.0f;

    [SerializeField] private float heightGain = 10.0f;

    // The simulated speed of the bird going forward
    // The lower the speed the faster the bird drops
    // Also used to determine where the bird will look
    [SerializeField] private int virtualSpeed = 100;


    //[SerializeField] private float glideFactor = 10f;

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
    }

    // Update is called once per frame
    void Update()
    {
        playerInput = playerControls.ReadValue<Vector2>();
        if (playerInput.y > 0.2f)
        {
            if ((int)birdState < (int)WingStates.Flap)
                birdState = WingStates.Flap;
        }
        else
            birdState = WingStates.Resting;

    }

    private void FixedUpdate()
    {
        BirdMovement();
        BirdRotation();
    }

    private void BirdMovement()
    {
        Vector2 _birdMove = Vector2.zero;

        switch (birdState)
        {
            case WingStates.Flap:
                {
                    if (energy.EnergyConsumption(10))
                    {
                        Debug.Log("Flap!");
                        // Increase the height of the bird
                        // Animator will play the bird flap
                        _birdMove.y += heightGain;
                        virtualSpeed -= 20;
                    }

                    birdState = WingStates.Flapped;
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
        float angle = Mathf.LerpAngle(transform.rotation.eulerAngles.x, Mathf.Clamp(-rb.velocity.y, -50, 50), Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Euler(angle,0f,0f);

    }


    private float GlideBehaviour()
    {
        float lift = 0.0f;
        if (rb.velocity.y < -4.8f)
        {
            virtualSpeed += 120;
            lift += 50f;
        }
        if (virtualSpeed > 500 && rb.velocity.y < 4.8f)
        {
            lift += 20;
            virtualSpeed -= 20;
        }
        else
        {
            if (virtualSpeed > 500)
            {
                lift += 5f;
                virtualSpeed -= 1;
            }
            else
            {
                lift += 4f;
            }

        }
        return lift;
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
