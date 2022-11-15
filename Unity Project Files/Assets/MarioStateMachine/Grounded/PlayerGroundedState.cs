using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
    : base (currentContext, playerStateFactory, currentStateName){ }

    public override void EnterState() {
        GravityFunction();
        ctx.courotine = ctx.StartCoroutine(ctx.jumpTimerReset());
        ctx.anim.Play("Grounded", 0);
        ctx.Side_Air_Velocity = Vector3.zero;
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void CheckSwitchStates() {
        SwitchState(factory.Idle());
    }

    public override void APressFunction() { }

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

    public override void GravityFunction() {
        ctx.currentGravity = ctx.groundedGravity;
    }
}
