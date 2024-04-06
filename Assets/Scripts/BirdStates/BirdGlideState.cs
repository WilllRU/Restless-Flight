using UnityEngine;

public class BirdGlideState : BirdBaseState
{
    private float _liftSpeed = 0f;
    private float _height = 0f;

    public override void EnterState(BirdStateMachine bird)
    {
        _liftSpeed = bird.LiftSpeed;
        _height = bird.HeightGain;
        bird.UpdateAnimationState(BirdStateMachine.WingStates.Glide);
    }

    public override void UpdateState(BirdStateMachine bird)
    {
        if(bird.GetInput.y < 0.1f)
        {
            bird.SwitchState(bird.FallState);
        }
    }

    public override void PhysicsUpdateState(BirdStateMachine bird)
    {
        float lift = 0f;
        float inc = 0.01f;

        bird.CurrentSpeed -= bird.SpeedFallOff() + 0.01f;

        bird.CurrentSpeed -= bird.GetRigidbody.velocity.y * 0.2f;

        float liftScale = (bird.CurrentSpeed - _liftSpeed) * inc;

        if (liftScale > 0f)
        {
            lift = _height * liftScale;
        }

        Debug.Log(lift);

        bird.GetRigidbody.AddForce(0f,lift,0f);

    }

    public override void OnTrigger(BirdStateMachine bird, Collider other)
    {

    }
}
