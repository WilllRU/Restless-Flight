using UnityEngine;

public class BirdFallState : BirdBaseState
{
    public override void EnterState(BirdStateMachine bird)
    {
        // Switch the animation to falling
        bird.UpdateAnimationState(BirdStateMachine.WingStates.Resting);
    }

    public override void UpdateState(BirdStateMachine bird)
    {
        if (bird.GetInput.y > 0.1f)
        {
            bird.SwitchState(bird.FlapState);
        }
    }

    public override void PhysicsUpdateState(BirdStateMachine bird)
    {
        bird.CurrentSpeed -= 0.1f;
    }

    public override void OnTrigger(BirdStateMachine bird, Collider other)
    {

    }
}
