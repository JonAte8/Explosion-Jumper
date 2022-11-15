using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
    : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState() {
        GravityFunction();
        ctx.anim.Play("Jump", 0);
    }

    public override void UpdateState() {
        CheckSwitchStates();
    }

    public override void ExitState() { }

    public override void CheckSwitchStates() {
        //logic to tell which jump to go to
        if (ctx.jumpCounter == 0)
        {
            SwitchState(factory.FirstJump());
        }
        else if (ctx.jumpCounter == 1)
        {
            SwitchState(factory.SecondJump());
        }
        else if(ctx.jumpCounter == 2)
        {
            if(ctx.groundSpeed >= 8)
            {
                SwitchState(factory.ThirdJump());
            }
            else
            {
                ctx.jumpCounter = 0;
                SwitchState(factory.FirstJump());
            }
        }
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
        ctx.currentGravity = ctx.jumpGravity;
    }
}
