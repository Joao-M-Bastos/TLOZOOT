using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerState : BaseState
{
    public override void EndState(LinkScpt link, StateMachineController machineController)
    {
        
    }

    public override void EnterState(LinkScpt link, StateMachineController machineController)
    {
        link.RB.useGravity = false;
        link.RB.velocity = Vector3.zero;
    }

    public override void FixedUpdateState(LinkScpt link, StateMachineController machineController)
    {

    }

    public override void UpdateState(LinkScpt link, StateMachineController machineController)
    {

    }
}
