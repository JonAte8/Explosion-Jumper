using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeHangState : PlayerBaseState
{
    float jumpForce = 800;
    float runSpeed = 500;
    float accelerationSpeed = 30;

    public PlayerLedgeHangState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
   : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState()
    {
        ctx.Jump_Velocity = Vector3.zero;
        ctx.Ground_Velocity = Vector3.zero;
        ctx.anim.Play("Ledge Hang");
        ctx.anim.SetBool("Ledge Hang", true);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        ctx.anim.SetBool("Ledge Hang", false);
    }

    public override void CheckSwitchStates()
    {
        ctx.transform.forward = -ctx.lowLedgeNormal;
    }

    public override void APressFunction() {
        SwitchState(factory.LedgePullUp());
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

    }
}
