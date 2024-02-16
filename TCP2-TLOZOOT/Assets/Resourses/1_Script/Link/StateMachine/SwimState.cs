using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimState : BaseState
{
    public override void EndState(LinkScpt link, StateMachineController machineController)
    {
        
    }

    public override void EnterState(LinkScpt link, StateMachineController machineController)
    {
        link.RB.useGravity = false;
    }

    public override void FixedUpdateState(LinkScpt link, StateMachineController machineController)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(LinkScpt link, StateMachineController machineController)
    {
        throw new System.NotImplementedException();
    }
}
