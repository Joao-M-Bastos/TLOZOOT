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
        if (CanClimbCorner(link, machineController))
        {
            machineController.ChangeState(machineController.cornerState);
        }
    }

    public bool CanClimbCorner(LinkScpt link, StateMachineController machineController)
    {
        if (!RayAcimaFrontal(link, machineController))
        {
            if (RayFrontal(link, machineController) && RayAcima(link, machineController))
            {
                return true;
            }
        }
        return false;
    }

    public bool RayFrontal(LinkScpt link, StateMachineController machineController)
    {
        //Verifica se há uma parede escalavel a frente
        Vector3 rayStartPos = link.transform.position + new Vector3(0, 2f, 0);

        //Debug.DrawRay(rayStartPos, instaciaPlayer.PlayerForword * 1.4f, Color.green);

        if (Physics.Raycast(rayStartPos, link.transform.forward, out RaycastHit rayFrontal, 1.2f, link.ClimbMask)
         || Physics.Raycast(rayStartPos, link.transform.forward, out rayFrontal, 1.2f, link.GroundMask))
        {
            return true;
        }
        return false;
    }

    public bool RayAcima(LinkScpt link, StateMachineController machineController)
    {

        //Verifica se há uma parede escalavel a frente
        Vector3 rayStartPos = link.transform.position + link.transform.forward * 1.5f + new Vector3(0f, 5f, 0f);

        //Debug.DrawRay(rayStartPos, -this.transform.up * 2.4f, Color.green);

        if (Physics.Raycast(rayStartPos, -link.transform.up, out RaycastHit rayAcima, 2.4f, link.ClimbMask)
        || Physics.Raycast(rayStartPos, -link.transform.up, out rayAcima, 2.4f, link.GroundMask))
        {
            return true;
        }
        return false;
    }

    public bool RayAcimaFrontal(LinkScpt link, StateMachineController machineController)
    {
        //Verifica se há uma parede escalavel a frente
        Vector3 rayStartPos = link.transform.position + new Vector3(0f, 4, 0f);

        Debug.DrawRay(rayStartPos, link.transform.forward * 2.4f, Color.green);

        if (Physics.Raycast(rayStartPos, link.transform.forward, out RaycastHit rayAcimaFrontal, 2.4f, link.ClimbMask)
        || Physics.Raycast(rayStartPos, link.transform.forward, out rayAcimaFrontal, 2.4f, link.GroundMask))
        {
            return true;
        }
        return false;
    }
}
