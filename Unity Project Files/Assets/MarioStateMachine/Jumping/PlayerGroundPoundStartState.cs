using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundPoundStartState : PlayerBaseState
{
    float timer = 0;

    public PlayerGroundPoundStartState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
   : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState()
    {
        ctx.Jump_Velocity = Vector3.zero;
        ctx.Ground_Velocity = Vector3.zero;
        ctx.anim.Play("Ground Pound Start", 0);
        ctx.anim.SetBool("Ground Pound Start", true);
        ctx.jumpCounter = 0;
        ctx.StopCoroutine(ctx.courotine);

    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;
        if (timer > 0.5f)
        {
            SwitchState(factory.GroundPoundFall());
        }
    }

    public override void ExitState()
    {
        ctx.anim.SetBool("Ground Pound Start", false);
    }

    public override void CheckSwitchStates()
    {

    }

    public override void APressFunction() { }

    public override void BPressFunction()
    {

    }

    public override void XPressFunction()
    {

    }

    public override void YPressFunction()
    {

    }

    public override void ZPressFunction()
    {

    }

    public override void GravityFunction()
    {

    }
}
