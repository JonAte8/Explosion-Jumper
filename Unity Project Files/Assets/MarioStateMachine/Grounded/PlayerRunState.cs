using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    float runSpeed = 500;
    float accelerationSpeed = 30;
    float turnAngleChange = 10 * Time.deltaTime;
    float turnAngleSpeed = 160;
    float multiplier = 3;

    public PlayerRunState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
    : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState() {
        ctx.anim.SetBool("Run", true);
        ctx.dustParticles.Play();
    }

    public override void UpdateState() {
        GravityFunction();
        CheckSwitchStates();
        RunStuff();
    }

    public override void ExitState() {
        ctx.anim.SetBool("Run", false);
        ctx.dustParticles.Stop();
    }

    public override void CheckSwitchStates() {
        if (!ctx.grounded)
        {
            SwitchState(factory.Falling());
        }
        if (ctx.Input_Forward < -0.3)
        {
            SwitchState(factory.Pivot());
        }
        else if (ctx.Tilt_Mag < 0.01)
        {
            SwitchState(factory.Skid());
        }
        else if (ctx.Tilt_Mag < 0.8)
        {
            SwitchState(factory.Walk());
        }
    }

    void RunStuff()
    {
        Vector3 turn = ctx.relativeToCameraInputVector3;
        turn = Vector3.ProjectOnPlane(turn, ctx.GroundNormal);
        float turnSpeed = Vector3.Angle(ctx.transform.forward, turn) < turnAngleSpeed ? Vector3.Angle(ctx.transform.forward, turn) * Time.deltaTime : turnAngleSpeed * Time.deltaTime;
        turnSpeed = Vector3.SignedAngle(ctx.transform.forward, turn, ctx.groundNormal) < 0 ? -turnSpeed : turnSpeed;
        ctx.transform.RotateAround(ctx.transform.position, ctx.groundNormal, turnSpeed * multiplier);
        ctx.Ground_Velocity = Vector3.MoveTowards(ctx.Ground_Velocity, ctx.transform.forward * runSpeed, accelerationSpeed).magnitude > (ctx.transform.forward * runSpeed).magnitude ? ctx.transform.forward * runSpeed : Vector3.MoveTowards(ctx.Ground_Velocity, ctx.transform.forward * runSpeed, accelerationSpeed);
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
        SwitchState(factory.CrouchSlide());
    }

    public override void GravityFunction()
    {
        ctx.Jump_Velocity = -ctx.currentGravity * ctx.groundNormal;
    }
}
