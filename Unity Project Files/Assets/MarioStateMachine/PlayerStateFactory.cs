public class PlayerStateFactory
{
    PlayerStateMachine context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        context = currentContext;
    }

    public PlayerBaseState Idle() {
        return new PlayerIdleState(context, this, "Idle");
    }
    public PlayerBaseState Walk() {
        return new PlayerWalkState(context, this, "Walk");
    }
    public PlayerBaseState Run() {
        return new PlayerRunState(context, this, "Run");
    }
    public PlayerBaseState Grounded() {
        return new PlayerGroundedState(context, this, "Grounded");
    }
    public PlayerBaseState Jump() {
        return new PlayerJumpState(context, this, "Jump");
    }
    public PlayerBaseState Skid() {
        return new PlayerSkidState(context, this, "Skid");
    }
    public PlayerBaseState FirstJump()
    {
        return new PlayerFirstJumpState(context, this, "Jump1");
    }
    public PlayerBaseState SecondJump()
    {
        return new PlayerSecondJumpState(context, this, "Jump2");
    }
    public PlayerBaseState ThirdJump()
    {
        return new PlayerThirdJumpState(context, this, "Jump3");
    }
    public PlayerBaseState Pivot()
    {
        return new PlayerPivotState(context, this, "Pivot");
    }
    public PlayerBaseState Sideflip()
    {
        return new PlayerSideflipState(context, this, "Sideflip");
    }
    public PlayerBaseState Crouch()
    {
        return new PlayerCrouchState(context, this, "Crouch");
    }
    public PlayerBaseState CrouchSlide()
    {
        return new PlayerCrouchSlideState(context, this, "Crouch Slide");
    }
    public PlayerBaseState LongJump()
    {
        return new PlayerLongJumpState(context, this, "Long Jump");
    }
    public PlayerBaseState Falling()
    {
        return new PlayerFallingState(context, this, "Falling");
    }
    public PlayerBaseState Bonk()
    {
        return new PlayerBonkState(context, this, "Bonk");
    }
    public PlayerBaseState WallJump()
    {
        return new PlayerWallJumpState(context, this, "Wall Jump");
    }
    public PlayerBaseState Backflip()
    {
        return new PlayerBackFlipState(context, this, "Backflip");
    }
    public PlayerBaseState GroundPoundStart()
    {
        return new PlayerGroundPoundStartState(context, this, "Ground Pound Start");
    }
    public PlayerBaseState GroundPoundFall()
    {
        return new PlayerGroundPoundFallState(context, this, "Ground Pound Fall");
    }
    public PlayerBaseState LedgeHang()
    {
        return new PlayerLedgeHangState(context, this, "Ledge Hang");
    }
    public PlayerBaseState LedgePullUp()
    {
        return new PlayerLedgePullUpState(context, this, "Ledge Pull Up");
    }
    public PlayerBaseState ShellRiding()
    {
        return new PlayerShellRideState(context, this, "Shell Riding");
    }
    public PlayerBaseState ShellJump()
    {
        return new PlayerShellJumpState(context, this, "Shell Jump");
    }
    public PlayerBaseState ShellFall()
    {
        return new PlayerShellFallState(context, this, "Shell Fall");
    }
    public PlayerBaseState Punch1()
    {
        return new PlayerPunch1State(context, this, "Punch1");
    }
    public PlayerBaseState Punch2()
    {
        return new PlayerPunch2State(context, this, "Punch2");
    }
    public PlayerBaseState GroundKick()
    {
        return new PlayerGroundKickState(context, this, "Ground Kick");
    }
    public PlayerBaseState AirKick()
    {
        return new PlayerAirKickState(context, this, "Air Kick");
    }
}
