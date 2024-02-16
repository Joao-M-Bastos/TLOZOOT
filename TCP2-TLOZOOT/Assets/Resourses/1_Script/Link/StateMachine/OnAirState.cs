using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAirState : BaseState
{
    public override void EndState(LinkScpt link, StateMachineController machineController)
    {
        
    }

    public override void EnterState(LinkScpt link, StateMachineController machineController)
    {
        link.LookAtMoveDirection();
        link.RB.useGravity = true;
    }

    public override void FixedUpdateState(LinkScpt link, StateMachineController machineController)
    {
        UpGravity(link);
    }

    private void UpGravity(LinkScpt link)
    {
        if(link.RB.velocity.y < 0.5)
        {
            link.RB.velocity += -link.transform.up / 5;
        }
    }

    public override void UpdateState(LinkScpt link, StateMachineController machineController)
    {
        TryChangeState(link, machineController);
    }

    public void TryChangeState(LinkScpt link, StateMachineController machineController)
    {
        if (Physics.Raycast(link.transform.position + new Vector3(0, 0.4f, 0), link.transform.forward, out RaycastHit wallHit, 0.75f, link.ClimbMask))
        {
            link.transform.rotation = Quaternion.LookRotation(-wallHit.normal);
            machineController.ChangeState(machineController.climbimgState);
        }
        else if (link.IsOnGround())
        {
            machineController.ChangeState(machineController.groundedState);
        }else if (link.CanClimbCorner())
        {
            machineController.ChangeState(machineController.cornerState);
        }
    }
}
