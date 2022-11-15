using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPivotState : PlayerBaseState
{
    float runSpeed = 500;
    float accelerationSpeed = 20;
    float turnAngleSpeed = 160;
    float multiplier = 1;

    public PlayerPivotState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
    : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState()
    {
        ctx.anim.SetBool("Pivot", true);
    }

    public override void UpdateState()
    {
        RunStuff();
        GravityFunction();
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        ctx.anim.SetBool("Pivot", false);
        Vector3 back = -ctx.transform.forward;
        ctx.transform.forward = back;
    }

    public override void CheckSwitchStates()
    {
        if (!ctx.grounded)
        {
            SwitchState(factory.Falling());
        }
        if (ctx.Ground_Velocity == Vector3.zero)
        {
            SwitchState(factory.Run());
        }
    }

    void RunStuff()
    {
        Vector3 turn = ctx.relativeToCameraInputVector3;
        turn = Vector3.ProjectOnPlane(turn, ctx.GroundNormal);
        float turnSpeed = Vector3.Angle(ctx.transform.forward, turn) < turnAngleSpeed ? Vector3.Angle(ctx.transform.forward, turn) * Time.deltaTime : turnAngleSpeed * Time.deltaTime;
        turnSpeed = Vector3.SignedAngle(ctx.transform.forward, turn, ctx.groundNormal) < 0 ? -turnSpeed : turnSpeed;
        ctx.transform.RotateAround(ctx.transform.position, ctx.groundNormal, -turnSpeed * multiplier);
        ctx.Ground_Velocity = Vector3.MoveTowards(ctx.Ground_Velocity, Vector3.zero, accelerationSpeed);
    }

    public override void APressFunction()
    {
        SwitchState(factory.Sideflip());
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
        ctx.Jump_Velocity = -ctx.currentGravity * ctx.groundNormal;
    }
}
