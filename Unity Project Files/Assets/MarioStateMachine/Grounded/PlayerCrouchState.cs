using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    float deccelerationSpeed = 30;

    public PlayerCrouchState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
    : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState()
    {
        ctx.anim.SetBool("Crouch", true);
    }

    public override void UpdateState()
    {
        SkidStuff();
        GravityFunction();
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        ctx.anim.SetBool("Crouch", false);
    }

    public override void CheckSwitchStates()
    {
        if (!ctx.grounded)
        {
            SwitchState(factory.Falling());
        }
        if (!ctx.zPressed)
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
        SwitchState(factory.Backflip());
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
