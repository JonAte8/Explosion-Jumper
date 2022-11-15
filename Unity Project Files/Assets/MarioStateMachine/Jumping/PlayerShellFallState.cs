using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShellFallState : PlayerBaseState
{
    float runSpeed = 1000;
    float turnAngleSpeed = 160;
    float multiplier = 3;
    float jumpForce = 800;
    Vector3 test;
    Vector3 vertical;
    float jumpGravity;

    public PlayerShellFallState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
   : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState()
    {
        ctx.anim.SetBool("Shell Fall", true);
        ctx.anim.Play("Shell Fall", 0);
        jumpGravity = ctx.jumpGravity;
        ctx.transform.position = ctx.shellPosition.position + (ctx.groundNormal * 1.5f);
    }

    public override void UpdateState()
    {
        GravityFunction();
        ShellStuff();
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        ctx.anim.SetBool("Shell Fall", false);
    }

    public override void CheckSwitchStates()
    {
        if (ctx.shellGrounded && Vector3.Angle(vertical, ctx.groundNormal) > 90)
        {
            SwitchState(factory.ShellRiding());
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

    public override void GravityFunction()
    {
            bool test = vertical.magnitude > (ctx.terminalVelocity * ctx.groundNormal).magnitude && Vector3.Angle(vertical, -ctx.terminalVelocity * ctx.groundNormal) < 90;
            vertical = test ? -ctx.terminalVelocity * ctx.groundNormal : vertical - 2 * jumpGravity * ctx.groundNormal;
    }

    void ShellStuff()
    {
        Vector3 dot = Vector3.Cross(ctx.shellPosition.transform.forward, ctx.groundNormal);
        float angle = Vector3.Angle(ctx.shellPosition.transform.forward, ctx.groundNormal) < ctx.gravityTurnSpeed ? Vector3.Angle(ctx.shellPosition.transform.forward, ctx.groundNormal) * Time.deltaTime : ctx.gravityTurnSpeed * Time.deltaTime;
        angle = Vector3.SignedAngle(ctx.shellPosition.transform.forward, ctx.groundNormal, dot) < 0 ? -angle : angle;
        ctx.shellPosition.transform.RotateAround(ctx.shellPosition.transform.position, dot, angle * ctx.gravityTurnMultiplier);
        float clampedThingForward = Mathf.Clamp(ctx.Input_Forward, -0.5f, 1);
        float clampedThingRight = Mathf.Clamp(ctx.Input_Right, -0.5f, 0.5f);
        Rigidbody shellRB = ctx.shellPosition.gameObject.GetComponent<Rigidbody>();
        float clamp = Mathf.Clamp(ctx.Input_Forward, 0.1f, 1);
        test = Vector3.MoveTowards(test, (clampedThingForward * runSpeed * ctx.shellPosition.right) + (clampedThingRight * runSpeed * ctx.shellPosition.up), runSpeed);
        Vector3 total = test + vertical;
        shellRB.velocity = total * Time.deltaTime;
        ctx.transform.position = ctx.shellPosition.position + (ctx.groundNormal * 1.5f);
    }
}
