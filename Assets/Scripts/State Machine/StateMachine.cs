using UnityEngine;


public class StateMachine : MonoBehaviour
{
    [SerializeField] public AIController aiController;

    public  BaseState _currentState;

    public IdleState _idleState = new();
    public ChaseState _chaseState = new();
    public PatrolState _patrolState = new();


    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState();
        }
    }
    public void ChangeState(BaseState newState)
    {
        if (_currentState != null)
        {
            _currentState.ExitState();
        }

        _currentState = newState;

        if (_currentState != null)
        {
            _currentState.owner = this;
            _currentState.EnterState();
        }
    }
}