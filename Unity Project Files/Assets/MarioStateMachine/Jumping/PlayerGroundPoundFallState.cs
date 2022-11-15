using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundPoundFallState : PlayerBaseState
{
    float jumpForce = 1500;
    float timer = 0;

    public PlayerGroundPoundFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
   : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState()
    {
        ctx.anim.Play("Ground Pound Fall", 0);
        ctx.anim.SetBool("Ground Pound Fall", true);
    }

    public override void UpdateState()
    {
        ctx.Jump_Velocity = -jumpForce * ctx.groundNormal;
        if (ctx.grounded)
        {
            timer += Time.deltaTime;
        }
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        ctx.anim.SetBool("Ground Pound Fall", false);
    }

    public override void CheckSwitchStates()
    {
        if (ctx.grounded && Vector3.Angle(ctx.Jump_Velocity, ctx.groundNormal) > 90 && timer > 0.5f)
        {
            SwitchState(factory.Grounded());
        }
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
