using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    float deccelerationSpeed = 100;
    float turnAngleSpeed = 160;

    public PlayerIdleState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
    : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState() {
        ctx.anim.SetBool("Idle", true);
    }

    public override void UpdateState() {
        IdleStuff();
        GravityFunction();
        CheckSwitchStates();
    }

    public override void ExitState() {
        ctx.anim.SetBool("Idle", false);
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
        else if(ctx.Tilt_Mag > 0.01)
        {
            SwitchState(factory.Walk());
        }
        else if (ctx.zPressed)
        {
            SwitchState(factory.Crouch());
        }
    }



    void IdleStuff()
    {
        if (ctx.Ground_Velocity.magnitude < 0.5f)
        {
            Vector3 turn = ctx.relativeToCameraInputVector3;
            turn = Vector3.ProjectOnPlane(turn, ctx.GroundNormal);
            float turnSpeed = Vector3.Angle(ctx.transform.forward, turn) < turnAngleSpeed ? Vector3.Angle(ctx.transform.forward, turn) : turnAngleSpeed;
            turnSpeed = Vector3.SignedAngle(ctx.transform.forward, turn, ctx.groundNormal) < 0 ? -turnSpeed : turnSpeed;
            ctx.transform.RotateAround(ctx.transform.position, ctx.groundNormal, turnSpeed);
        }
        ctx.Ground_Velocity = Vector3.MoveTowards(ctx.Ground_Velocity, Vector3.zero, deccelerationSpeed);
    }

    public override void APressFunction() {
        SwitchState(factory.Jump());
    }

    public override void BPressFunction()
    {
        if(ctx.groundSpeed < 6)
        {
            SwitchState(factory.Punch1());
        }
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

    public override void GravityFunction() {
        ctx.Jump_Velocity = -ctx.currentGravity * ctx.groundNormal;
    }
}
