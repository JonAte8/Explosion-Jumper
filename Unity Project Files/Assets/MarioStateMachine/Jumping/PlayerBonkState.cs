using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBonkState : PlayerBaseState
{
    float runSpeed = 150;
    float accelerationSpeed = 150;
    float wallJumpTimer = 0;

    public PlayerBonkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
   : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState()
    {
        ctx.anim.Play("Bonk", 0);
        ctx.anim.SetBool("Bonk", true);
        ctx.jumpCounter = 1;
        ctx.StopCoroutine(ctx.courotine);
        WallReflect();
    }

    public override void UpdateState()
    {
        GravityFunction();
        CheckSwitchStates();
        wallJumpTimer += Time.deltaTime;
    }

    public override void ExitState()
    {
        ctx.anim.SetBool("Bonk", false);
    }

    public override void CheckSwitchStates()
    {
        if (ctx.grounded && Vector3.Angle(ctx.Jump_Velocity, ctx.groundNormal) > 90)
        {
            SwitchState(factory.Grounded());
        }
    }

    public override void APressFunction() {
        if (wallJumpTimer < 0.25f)
        {
            SwitchState(factory.WallJump());
        }
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

    public void WallReflect()
    {
        Vector3 test = Vector3.Reflect(ctx.transform.forward, ctx.wallNormal);
        ctx.transform.forward = -test;
        ctx.Jump_Velocity = Vector3.zero;
    }

    public override void GravityFunction()
    {
        bool test = ctx.Jump_Velocity.magnitude > (ctx.terminalVelocity * ctx.groundNormal).magnitude && Vector3.Angle(ctx.Jump_Velocity, -ctx.terminalVelocity * ctx.Jump_Velocity) < 90;
        ctx.Jump_Velocity = test ? -ctx.terminalVelocity * ctx.groundNormal : ctx.Jump_Velocity - (ctx.currentGravity * ctx.groundNormal);
        ctx.Ground_Velocity = Vector3.MoveTowards(ctx.Ground_Velocity, ctx.transform.forward * -runSpeed, accelerationSpeed);
    }
}
