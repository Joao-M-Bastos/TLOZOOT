using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingState : BaseState
{
    public override void EnterState(LinkScpt link, StateMachineController machineController)
    {
        link.RB.useGravity = false;
    }

    public override void EndState(LinkScpt link, StateMachineController machineController)
    {
        link.RB.useGravity = true;
    }

    public override void FixedUpdateState(LinkScpt link, StateMachineController machineController)
    {
        Vector3 velocityClimb;

        //direcaoClimb = new Vector3(-InputX, InputZ, 0) * this.instaciaPlayer.PlayerRight.x;

        velocityClimb = link.transform.right * Input.GetAxis("Horizontal");
        velocityClimb.y = Input.GetAxis("Vertical");

        //Movimenta o jogador nos eixos X e Y

        link.RB.velocity = velocityClimb * link.BaseSpeed;
    }

    public override void UpdateState(LinkScpt link, StateMachineController machineController)
    {
        TryChangeState(link, machineController);
    }

    public void TryChangeState(LinkScpt link, StateMachineController machineController)
    {
        if (!Physics.Raycast(link.transform.position + new Vector3(0, 0.4f, 0), link.transform.forward, out RaycastHit wallHit, 1.2f, link.ClimbMask))
        {
            machineController.ChangeState(machineController.groundedState);
        }
    }
}
