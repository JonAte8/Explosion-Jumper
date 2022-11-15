using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{

    float walkSpeed = 100;
    float accelerationSpeed = 50;
    float turnAngleSpeed = 160;
    float multiplier = 10;


    public PlayerWalkState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
    : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState() {
        ctx.anim.SetBool("Walk", true);
    }

    public override void UpdateState() {
        WalkStuff();
        GravityFunction();
        CheckSwitchStates();
    }

    public override void ExitState() {
        ctx.anim.SetBool("Walk", false);
    }

    public override void CheckSwitchStates() {
        if (!ctx.grounded)
        {
            SwitchState(factory.Falling());
        }
        if (ctx.Tilt_Mag > 0.8)
        {
            SwitchState(factory.Run());
        }
        else if (ctx.Tilt_Mag < 0.01)
        {
            SwitchState(factory.Idle());
        }
    }

    void WalkStuff()
    {
        Vector3 turn = ctx.relativeToCameraInputVector3;
        turn = Vector3.ProjectOnPlane(turn, ctx.GroundNormal);
        float turnSpeed = Vector3.Angle(ctx.transform.forward, turn) < turnAngleSpeed ? Vector3.Angle(ctx.transform.forward, turn) * Time.deltaTime : turnAngleSpeed * Time.deltaTime;
        turnSpeed = Vector3.SignedAngle(ctx.transform.forward, turn, ctx.groundNormal) < 0 ? -turnSpeed : turnSpeed;
        ctx.transform.RotateAround(ctx.transform.position, ctx.groundNormal, turnSpeed * multiplier);
        ctx.Ground_Velocity = Vector3.MoveTowards(ctx.Ground_Velocity, ctx.transform.forward * walkSpeed, accelerationSpeed);
    }

    public override void APressFunction()
    {
        SwitchState(factory.Jump());
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
        SwitchState(factory.Crouch());
    }

    public override void GravityFunction()
    {
        ctx.Jump_Velocity = -ctx.currentGravity * ctx.groundNormal;
    }
}
