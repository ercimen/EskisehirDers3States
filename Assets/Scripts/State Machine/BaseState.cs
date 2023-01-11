
public abstract class BaseState
{
    public StateMachine owner;

    public virtual void EnterState() { }

    public virtual void UpdateState() { }

    public virtual void ExitState() { }
}