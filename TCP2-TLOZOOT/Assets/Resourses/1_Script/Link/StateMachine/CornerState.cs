using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerState : BaseState
{
    bool exit;
    float distanceUp = 0.2f;
    float distanceFront = 0.1f;
    public override void EndState(LinkScpt link, StateMachineController machineController)
    {
        exit = false;
    }

    public override void EnterState(LinkScpt link, StateMachineController machineController)
    {
        exit = false;
        link.RB.useGravity = false;
        link.RB.velocity = Vector3.zero;
    }

    public override void FixedUpdateState(LinkScpt link, StateMachineController machineController)
    {
        if (exit)
        {
            if (distanceUp > 0)
            {
                link.transform.position += link.transform.up * 2.5f * Time.deltaTime;
                distanceUp -= Time.deltaTime;
            }
            else if (distanceFront > 0)
            {
                link.transform.position += link.transform.forward * 5 * Time.deltaTime;
                distanceFront -= Time.deltaTime;
            }
            else
                exit = false;
            
        }
    }

    public override void UpdateState(LinkScpt link, StateMachineController machineController)
    {
        if (exit)
              return;
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            link.Jump(-link.transform.forward, link.JumpForce/5);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            exit = true;
            //link.CapsuleC.isTrigger = true;
            distanceUp = 1;
            distanceFront = 0.3f;
        }

        TryChangeState(link, machineController);
    }

    public void TryChangeState(LinkScpt link, StateMachineController machineController)
    {
        if (!link.CanClimbCorner())
        {
            machineController.ChangeState(machineController.onAirState);
        }
    }
}
