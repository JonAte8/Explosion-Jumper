[System.Serializable]
public abstract class PlayerBaseState
{
    protected PlayerStateMachine ctx;
    protected PlayerStateFactory factory;
    protected string stateName;

    public string StateName
    {
        get { return stateName; }
        set { stateName = value; }
    }
    public PlayerBaseState(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory, string currentStateName)
    {
        ctx = currentContext;
        factory = playerStateFactory;
        stateName = currentStateName;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void APressFunction();

    public abstract void BPressFunction();

    public abstract void XPressFunction();

    public abstract void YPressFunction();

    public abstract void ZPressFunction();

    public abstract void GravityFunction();

    public void SwitchState(PlayerBaseState newState) {
        ExitState();

        newState.EnterState();

        ctx.CurrentState = newState;
    }
}
