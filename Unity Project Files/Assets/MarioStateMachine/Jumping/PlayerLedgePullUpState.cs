using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgePullUpState : PlayerBaseState
{
    float jumpForce = 800;
    float runSpeed = 500;
    float accelerationSpeed = 30;

    public PlayerLedgePullUpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
   : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState()
    {
        ctx.Jump_Velocity = Vector3.zero;
        ctx.Ground_Velocity = Vector3.zero;
        ctx.anim.Play("Ledge Pull Up");
        ctx.anim.SetBool("Ledge Pull Up", true);
    }

    public override void UpdateState()
    {
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        ctx.transform.position = ctx.pullUpPoint - (ctx.transform.forward * 0.5f) + ctx.groundNormal;
        ctx.anim.SetBool("Ledge Pull Up", false);
        ctx.anim.Play("Grounded", 0);
    }

    public override void CheckSwitchStates()
    {
        if (ctx.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f)
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

    }
}
