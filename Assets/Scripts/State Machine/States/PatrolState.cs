using UnityEngine;

public class PatrolState : BaseState
{
    private float exitDelay = 0.25f;
    public override void EnterState()
    {
   
    }

    public override void UpdateState()
    {
        owner.aiController.Patrol();
    }

    public override void ExitState()
    {
       
    }
}