using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkidState : PlayerBaseState
{
    float deccelerationSpeed = 30;

    public PlayerSkidState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
    : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState()
    {
        ctx.anim.SetBool("Skid", true);
    }

    public override void UpdateState() 
    {
        SkidStuff();
        GravityFunction();
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        ctx.anim.SetBool("Skid", false);
    }

    public override void CheckSwitchStates()
    {
        if (!ctx.grounded)
        {
            SwitchState(factory.Falling());
        }
        if (ctx.Tilt_Mag > 0.8)
        {
            SwitchState(factory.Run());
        }
        else if (ctx.Tilt_Mag > 0.01)
        {
            SwitchState(factory.Walk());
        }
        else if(ctx.Ground_Velocity == Vector3.zero)
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

    }

    public override void GravityFunction()
    {
        ctx.Jump_Velocity = -ctx.currentGravity * ctx.groundNormal;
    }
}
