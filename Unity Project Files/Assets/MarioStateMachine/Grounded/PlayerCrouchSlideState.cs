using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchSlideState : PlayerBaseState
{
    float deccelerationSpeed = 10;

    public PlayerCrouchSlideState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
    : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState()
    {
        ctx.anim.SetBool("Crouch Slide", true);
    }

    public override void UpdateState()
    {
        SkidStuff();
        GravityFunction();
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        ctx.anim.SetBool("Crouch Slide", false);
    }

    public override void CheckSwitchStates()
    {
        if (!ctx.grounded)
        {
            SwitchState(factory.Falling());
        }
        if(ctx.Ground_Velocity == Vector3.zero)
        {
            SwitchState(factory.Idle());
        }
    }

    void SkidStuff()
    {
        ctx.Ground_Velocity = Vector3.MoveTowards(ctx.Ground_Velocity, Vector3.zero, deccelerationSpeed);
    }

    public override void APressFunction()
    {
        if(ctx.groundSpeed > 6)
        {
            SwitchState(factory.LongJump());
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

    public override void GravityFunction()
    {
        ctx.Jump_Velocity = -ctx.currentGravity * ctx.groundNormal;
    }
}
