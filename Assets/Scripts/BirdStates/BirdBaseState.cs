using UnityEngine;

public abstract class BirdBaseState
{
    public abstract void EnterState(BirdStateMachine bird);

    public abstract void UpdateState(BirdStateMachine bird);

    public abstract void PhysicsUpdateState(BirdStateMachine bird);

    public abstract void OnTrigger(BirdStateMachine bird, Collider other);
}
