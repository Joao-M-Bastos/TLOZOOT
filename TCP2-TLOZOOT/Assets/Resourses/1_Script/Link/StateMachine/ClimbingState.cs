using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingState : BaseState
{
    public override void EnterState(LinkScpt link, StateMachineController machineController)
    {
        Vector3 velocityClimb;

        //direcaoClimb = new Vector3(-InputX, InputZ, 0) * this.instaciaPlayer.PlayerRight.x;

        velocityClimb = link.transform.right * Input.GetAxis("Horizontal");
        velocityClimb.y = Input.GetAxis("Vertical");

        //Movimenta o jogador nos eixos X e Y

        link.linkRigidbody.velocity = velocityClimb * link.speedClimb;
    }

    public override void EndState(LinkScpt link, StateMachineController machineController)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(LinkScpt link, StateMachineController machineController)
    {
        throw new System.NotImplementedException();
    }
}
