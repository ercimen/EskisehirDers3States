using UnityEngine;

public class ChaseState : BaseState
{

    public override void EnterState()
    {

    }

    public override void UpdateState()
    {
        owner.aiController.Chase();
    }

    public override void ExitState()
    {
       
    }
}