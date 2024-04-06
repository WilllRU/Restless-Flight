using System.Collections;
using UnityEngine;

public class BirdFlapState : BirdBaseState
{
    private bool flapping = false;
    private Coroutine flapCoroutine;


    public override void EnterState(BirdStateMachine bird)
    {
        if (!flapping)
        {
            flapCoroutine = bird.StartCoroutine(FlapWings(bird));
            return;
        }
        bird.SwitchState(bird.GlideState);
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


    private IEnumerator FlapWings(BirdStateMachine bird)
    {
        if (bird.energy.EnergyConsumption(15))
        {
            bird.anim.StopPlayback();
            flapping = true;
            bird.UpdateAnimationState(BirdStateMachine.WingStates.Flap);

            yield return new WaitForSeconds(0.35f);

            bird.GetRigidbody.AddForce(0f, bird.FlapForce, 0f);
            bird.CurrentSpeed -= 2f;
            //anim.ResetTrigger("Flap");
            flapping = false;
        }
        bird.SwitchState(bird.GlideState);
        yield return null;
    }

}