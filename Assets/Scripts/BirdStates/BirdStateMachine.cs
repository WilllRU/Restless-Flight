using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdStateMachine : MonoBehaviour
{
    #region States
    private BirdBaseState currentState;

    public BirdGlideState GlideState = new BirdGlideState();
    public BirdFlapState FlapState = new BirdFlapState();
    public BirdFallState FallState = new BirdFallState();
    public BirdDeathState DeathState = new BirdDeathState();
    #endregion

    private bool _isDead = false;
    public bool DeadBird => _isDead;

    [SerializeField] public BarSystem energy;

    #region Variables
    [SerializeField] private InputAction _controller;
    private Rigidbody _rb;
    private BoxCollider _collider;
    public Animator anim;

    public Vector2 GetInput => _controller.ReadValue<Vector2>();
    public Rigidbody GetRigidbody => _rb;
    #endregion

    #region Bird Variables
    // All the variables in this region can be replaced with a 'Scriptable object'

    [SerializeField] private float _curSpeed = 0f;
    private float _rotationSpeed = 5.0f;
    [SerializeField] private float _heightGain = 200f;
    private float _flapForce = 400f;

    [SerializeField] private AnimationCurve _dropOff;
    private float _liftSpeed = 30f;
    private float _minSpeed = 20f;
    private float _maxSpeed = 100f;

    private float _turnAmount = 40f;
    private float _rollAmount = 45f;
    //private float _turnForce = 0f;

    public float LiftSpeed => _liftSpeed;
    public float HeightGain => _heightGain;
    public float FlapForce => _flapForce;

    #endregion
    public float CurrentSpeed
    {
        get
        {
            return _curSpeed;
        }
        set
        {
            if (!_isDead)
            {
                value = Mathf.Clamp(value, _minSpeed, _maxSpeed);
            }
            _curSpeed = value;
        }

    }

    public Vector2 BirdVector()
    {
        Vector2 velocity = new Vector2(GetInput.x * _turnAmount / (_turnAmount * 2f), (_curSpeed / (_maxSpeed * 2f)));
        //Debug.Log(velocity);
        return velocity;
    }

    public float SpeedMagnitude()
    {
        return (_curSpeed / _maxSpeed) * 0.1f;
    }

    public float SpeedFallOff()
    {

        float artificialDrag = _dropOff.Evaluate((_curSpeed - _minSpeed) / _maxSpeed) * 0.2f;
        //Debug.Log(artificialDrag);
        return artificialDrag;
    }

    public enum WingStates
    {
        None = 0,
        Resting = 1,
        Flap,
        Glide,
    }

    public WingStates birdState = WingStates.None;

    private void OnEnable()
    {
        _controller.Enable();
    }

    private void OnDisable()
    {
        _controller.Disable();
    }


    // Start is called before the first frame update
    private void Awake()
    {
        _curSpeed = _minSpeed;
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        currentState = GlideState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    private void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTrigger(this, other);
    }

    private void FixedUpdate()
    {
        currentState.PhysicsUpdateState(this);
        BirdRotation();
    }

    public void SwitchState(BirdBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void UpdateAnimationState(WingStates state)
    {
        birdState = state;
        anim.SetInteger("CurState", (int)state);
    }

    private void BirdRotation()
    {
        float pitchAngle = Mathf.LerpAngle(transform.rotation.eulerAngles.x, Mathf.Clamp(-_rb.velocity.y, -70, 80), Time.deltaTime * _rotationSpeed);
        //float yawAngle = Mathf.LerpAngle(transform.rotation.eulerAngles.y, Mathf.Clamp(playerInput.x * turnAmount, -turnAmount, turnAmount), Time.deltaTime * rotationSpeed);
        float rollAngle = Mathf.LerpAngle(-transform.rotation.eulerAngles.z, Mathf.Clamp(GetInput.x * _rollAmount, -_rollAmount, _rollAmount), Time.deltaTime * _rotationSpeed * 0.5f);
        transform.rotation = Quaternion.Euler(pitchAngle, 0f, -rollAngle);
    }


    public void BirdDead()
    {
        _isDead = true;
        SwitchState(DeathState);
        FindObjectOfType<GameManager>().EndGame();
    }

}
