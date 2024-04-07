using UnityEngine;

public class BirdDeathState : BirdBaseState
{
    public override void EnterState(BirdStateMachine bird)
    {
        bird.CurrentSpeed = 0f;
    }

    public override void UpdateState(BirdStateMachine bird)
    {

    }

    public override void PhysicsUpdateState(BirdStateMachine bird)
    {

    }

    public override void OnTrigger(BirdStateMachine bird, Collider other)
    {

    }
}
