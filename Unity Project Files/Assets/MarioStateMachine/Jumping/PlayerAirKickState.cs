using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirKickState : PlayerBaseState
{
    float upForce = 500;
    float runSpeed = 500;
    float accelerationSpeed = 30;

    public PlayerAirKickState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
   : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState()
    {
        ctx.Jump_Velocity = upForce * ctx.groundNormal;
        ctx.jumpCounter = 1;
        ctx.anim.Play("Air Kick");
        ctx.anim.SetBool("Air Kick", true);
        ctx.StopCoroutine(ctx.courotine);
    }

    public override void UpdateState()
    {
        GravityFunction();
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        ctx.anim.SetBool("Air Kick", false);
    }

    public override void CheckSwitchStates()
    {
        if (ctx.grounded && Vector3.Angle(ctx.Jump_Velocity, ctx.groundNormal) > 90)
        {
            SwitchState(factory.Grounded());
        }
    }

    public override void APressFunction()
    {

    }

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
        bool test = ctx.Jump_Velocity.magnitude > (ctx.terminalVelocity * ctx.groundNormal).magnitude && Vector3.Angle(ctx.Jump_Velocity, -ctx.terminalVelocity * ctx.Jump_Velocity) < 90;
        ctx.Jump_Velocity = test ? -ctx.terminalVelocity * ctx.groundNormal : ctx.Jump_Velocity - 2 * ctx.currentGravity * ctx.groundNormal;
        float clamp = Mathf.Clamp(ctx.Input_Right, -0.5f, 0.5f);
        Vector3 forward = Vector3.MoveTowards(ctx.Ground_Velocity, ctx.jumpMovement(-0.5f, 1, runSpeed), accelerationSpeed);
        ctx.Ground_Velocity = forward;
        ctx.Side_Air_Velocity = clamp * ctx.sideAcceleration * ctx.transform.right;
    }
}
