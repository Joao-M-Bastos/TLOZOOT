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
        if (CanClimbCorner())
        {

        }
    }

    public void CanClimbCorner()
    {
        if (!RayAcimaFrontal())
        {
            if (RayFrontal() && RayAcima())
            {
                GetInCorner();
            }
        }
    }

    public bool RayFrontal()
    {
        //Verifica se há uma parede escalavel a frente
        Vector3 rayStartPos = this.transform.position + new Vector3(0, 2f, 0);

        //Debug.DrawRay(rayStartPos, instaciaPlayer.PlayerForword * 1.4f, Color.green);

        if (Physics.Raycast(rayStartPos, instaciaPlayer.PlayerForword, out rayFrontal, 1.2f, instaciaPlayer.ClimbLayerMask)
         || Physics.Raycast(rayStartPos, instaciaPlayer.PlayerForword, out rayFrontal, 1.2f, instaciaPlayer.GroundLayerMask))
        {
            return true;
        }
        return false;
    }

    public bool RayAcima()
    {

        //Verifica se há uma parede escalavel a frente
        Vector3 rayStartPos = this.transform.position + instaciaPlayer.PlayerForword * 1.5f + new Vector3(0f, 5f, 0f);

        //Debug.DrawRay(rayStartPos, -this.transform.up * 2.4f, Color.green);

        if (Physics.Raycast(rayStartPos, -this.transform.up, out rayAcima, 2.4f, instaciaPlayer.ClimbLayerMask)
        || Physics.Raycast(rayStartPos, -this.transform.up, out rayAcima, 2.4f, instaciaPlayer.GroundLayerMask))
        {
            return true;
        }
        return false;
    }

    public bool RayAcimaFrontal()
    {
        //Verifica se há uma parede escalavel a frente
        Vector3 rayStartPos = this.transform.position + new Vector3(0f, 4, 0f);

        Debug.DrawRay(rayStartPos, instaciaPlayer.PlayerForword * 2.4f, Color.green);

        if (Physics.Raycast(rayStartPos, instaciaPlayer.PlayerForword, out rayAcimaFrontal, 2.4f, instaciaPlayer.ClimbLayerMask)
        || Physics.Raycast(rayStartPos, instaciaPlayer.PlayerForword, out rayAcimaFrontal, 2.4f, instaciaPlayer.GroundLayerMask))
        {
            return true;
        }
        return false;
    }
}
