using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerBaseState
{
    float initialForwardVelocity = 400;
    float jumpForce = 800;
    float runSpeed = 500;
    float accelerationSpeed = 30;

    public PlayerWallJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
   : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState()
    {
        Vector3 back = -ctx.transform.forward;
        ctx.transform.forward = back;
        ctx.Jump_Velocity = jumpForce * ctx.groundNormal;
        ctx.Ground_Velocity = initialForwardVelocity * ctx.transform.forward;
        ctx.anim.SetBool("Wall Jump", true);
        ctx.jumpCounter = 1;
        ctx.StopCoroutine(ctx.courotine);
    }

    public override void UpdateState()
    {
        GravityFunction();
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        ctx.anim.SetBool("Wall Jump", false);
    }

    public override void CheckSwitchStates()
    {
        if (ctx.grounded && Vector3.Angle(ctx.Jump_Velocity, ctx.groundNormal) > 90)
        {
            SwitchState(factory.Grounded());
        }
        if (ctx.wallHit && ctx.groundSpeed > 6)
        {
            SwitchState(factory.Bonk());
        }
        if (Vector3.Angle(ctx.Jump_Velocity, ctx.groundNormal) > 90 && ctx.lowLedgeHit && !ctx.highLedgeHit)
        {
            RaycastHit hit;
            if (Physics.Raycast(ctx.transform.position + (ctx.groundNormal * 0.5f) + (ctx.highLedgeForward * 1.5f), -ctx.groundNormal, out hit, 0.5f, ctx.ledgeMask, QueryTriggerInteraction.Ignore))
            {
                ctx.pullUpPoint = hit.point;
                SwitchState(factory.LedgeHang());
            }
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
        SwitchState(factory.GroundPoundStart());
    }

    public override void GravityFunction()
    {

        if (ctx.aPressed && Vector3.Angle(ctx.Jump_Velocity, ctx.groundNormal) < 90)
        {
            ctx.Jump_Velocity = ctx.Jump_Velocity - ctx.currentGravity * ctx.groundNormal;
        }
        else
        {
            bool test = ctx.Jump_Velocity.magnitude > (ctx.terminalVelocity * ctx.groundNormal).magnitude && Vector3.Angle(ctx.Jump_Velocity, -ctx.terminalVelocity * ctx.Jump_Velocity) < 90;
            ctx.Jump_Velocity = test ? -ctx.terminalVelocity * ctx.groundNormal : ctx.Jump_Velocity - 2 * ctx.currentGravity * ctx.groundNormal;
        }
        float clamp = Mathf.Clamp(ctx.Input_Right, -0.5f, 0.5f);
        Vector3 forward = Vector3.MoveTowards(ctx.Ground_Velocity, ctx.jumpMovement(-0.5f, 1, runSpeed), accelerationSpeed);
        ctx.Ground_Velocity = forward;
        ctx.Side_Air_Velocity = clamp * ctx.sideAcceleration * ctx.transform.right;

    }
}
