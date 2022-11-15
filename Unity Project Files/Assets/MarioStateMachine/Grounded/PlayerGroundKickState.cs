using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundKickState : PlayerBaseState
{
    float accelerationSpeed = 10;

    public PlayerGroundKickState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
   : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState()
    {
        ctx.anim.SetBool("Ground Kick", true);
    }

    public override void UpdateState()
    {
        ctx.Ground_Velocity = Vector3.MoveTowards(ctx.Ground_Velocity, Vector3.zero, accelerationSpeed);
        GravityFunction();
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        ctx.anim.SetBool("Ground Kick", false);
    }

    public override void CheckSwitchStates()
    {
        if (ctx.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f && ctx.anim.GetCurrentAnimatorStateInfo(0).IsName("Ground Kick"))
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
        ctx.Jump_Velocity = -ctx.currentGravity * ctx.groundNormal;
    }
}
