using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerState : BaseState
{
    public override void EndState(LinkScpt link, StateMachineController machineController)
    {
        link.RB.useGravity = true;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            link.Jump(-link.transform.forward, link.JumpForce/5);
        }

        TryChangeState(link, machineController);
    }

    public void TryChangeState(LinkScpt link, StateMachineController machineController)
    {
        if (!link.IsOnGround())
        {
            machineController.ChangeState(machineController.onAirState);
        }
    }
}
