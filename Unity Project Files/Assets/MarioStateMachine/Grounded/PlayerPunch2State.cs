using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch2State : PlayerBaseState
{
    float accelerationSpeed = 10;

    public PlayerPunch2State(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
   : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState()
    {
        ctx.anim.SetBool("Punch2", true);
    }

    public override void UpdateState()
    {
        ctx.Ground_Velocity = Vector3.MoveTowards(ctx.Ground_Velocity, Vector3.zero, accelerationSpeed);
        GravityFunction();
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        ctx.anim.SetBool("Punch2", false);
    }

    public override void CheckSwitchStates()
    {
        if (ctx.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f && ctx.anim.GetCurrentAnimatorStateInfo(0).IsName("Punch2"))
        {
            SwitchState(factory.Grounded());
        }
    }

    public override void APressFunction()
    {

    }

    public override void BPressFunction()
    {
        if (ctx.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4)
        {
            SwitchState(factory.GroundKick());
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

    public override void GravityFunction()
    {
        ctx.Jump_Velocity = -ctx.currentGravity * ctx.groundNormal;
    }
}
