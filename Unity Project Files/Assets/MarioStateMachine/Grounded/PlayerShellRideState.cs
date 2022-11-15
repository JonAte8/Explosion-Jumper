using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShellRideState : PlayerBaseState
{
    float runSpeed = 1000;
    float accelerationSpeed = 10;
    float turnAngleSpeed = 160;
    float multiplier = 3;
    Vector3 test;
    Vector3 vertical;
    float groundedGravity;
    float speedMultiplier = 0;
    float minShellMultiplier = 0.5f;

    public PlayerShellRideState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
   : base(currentContext, playerStateFactory, currentStateName) { }

    public override void EnterState()
    {
        ctx.anim.SetBool("Shell Riding", true);
        ctx.anim.Play("Shell Riding", 0);
        ctx.currentGravity = 0;
        ctx.Ground_Velocity = Vector3.zero;
        ctx.Jump_Velocity = Vector3.zero;
        if(ctx.rb != null)
        {
            ctx.rb.velocity = Vector3.zero;
            Rigidbody.Destroy(ctx.rb);
        }
        groundedGravity = ctx.groundedGravity;
        ctx.transform.SetParent(ctx.shellPosition);
        ctx.transform.position = ctx.shellPosition.position + (ctx.groundNormal * 1.5f);
        ctx.transform.forward = ctx.shellPosition.right;
    }

    public override void UpdateState()
    {
        if (speedMultiplier < 1)
        {
            speedMultiplier += ctx.inputVector3.magnitude * Time.deltaTime;
        }
        if(ctx.inputVector3.magnitude < 1 && speedMultiplier > minShellMultiplier)
        {
            speedMultiplier -= (1 - ctx.inputVector3.magnitude) * Time.deltaTime;
        }
        speedMultiplier = speedMultiplier > 1 ? 1 : speedMultiplier;
        speedMultiplier = speedMultiplier < minShellMultiplier ? minShellMultiplier : speedMultiplier;
        GravityFunction();
        ShellStuff();
        CheckSwitchStates();
    }

    public override void ExitState()
    {
        ctx.anim.SetBool("Shell Riding", false);
    }

    public override void CheckSwitchStates()
    {
        if (!ctx.shellGrounded)
        {
            SwitchState(factory.ShellFall());
        }
    }

    public override void APressFunction() {
        if (ctx.shellGrounded)
        {
            SwitchState(factory.ShellJump());
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
        Rigidbody rb = ctx.gameObject.AddComponent<Rigidbody>();
        RigidbodyConstraints constraints = RigidbodyConstraints.FreezeRotation;
        rb.constraints = constraints;
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        ctx.rb = rb;
        ctx.useRB = true;
        ctx.transform.SetParent(null);
        GameObject.Destroy(ctx.shellPosition.gameObject);
        SwitchState(factory.Grounded());

        
    }

    public override void GravityFunction()
    {
        vertical = -ctx.currentGravity * ctx.groundNormal;
    }

    void ShellStuff()
    {
        
        Rigidbody shellRB = ctx.shellPosition.gameObject.GetComponent<Rigidbody>();
        test = runSpeed * shellRB.transform.right * speedMultiplier;
        vertical = -groundedGravity * ctx.shellPosition.forward;
        Vector3 total = test + vertical;
        shellRB.velocity = total * Time.deltaTime;
        Vector3 dot = Vector3.Cross(ctx.shellPosition.transform.forward, ctx.groundNormal);
        float angle = Vector3.Angle(ctx.shellPosition.transform.forward, ctx.groundNormal) < ctx.gravityTurnSpeed ? Vector3.Angle(ctx.shellPosition.transform.forward, ctx.groundNormal) * Time.deltaTime : ctx.gravityTurnSpeed * Time.deltaTime;
        angle = Vector3.SignedAngle(ctx.shellPosition.transform.forward, ctx.groundNormal, dot) < 0 ? -angle : angle;
        ctx.shellPosition.transform.RotateAround(ctx.shellPosition.transform.position, dot, angle * ctx.gravityTurnMultiplier);
        Vector3 turn = ctx.relativeToCameraInputVector3;
        turn = Vector3.ProjectOnPlane(turn, ctx.GroundNormal);
        float turnSpeed = Vector3.Angle(ctx.shellPosition.transform.right, turn) < turnAngleSpeed ? Vector3.Angle(ctx.shellPosition.transform.right, turn) * Time.deltaTime : turnAngleSpeed * Time.deltaTime;
        turnSpeed = Vector3.SignedAngle(ctx.shellPosition.transform.right, turn, ctx.groundNormal) < 0 ? -turnSpeed : turnSpeed;
        ctx.shellPosition.transform.RotateAround(ctx.shellPosition.transform.position, ctx.groundNormal, turnSpeed * multiplier);
        //ctx.transform.position = ctx.shellPosition.position + (ctx.groundNormal * 1.5f);
    }
}
