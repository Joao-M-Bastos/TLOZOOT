using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundedState : BaseState
{
    float turnSmoothVelocity;
    public override void EnterState(LinkScpt link, StateMachineController machineController)
    {
        link.RB.useGravity = true;
    }

    public override void EndState(LinkScpt link, StateMachineController machineController)
    {
        
    }

    public override void FixedUpdateState(LinkScpt link, StateMachineController machineController)
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

            float speed = link.BaseSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                link.ChangeFov(70);
                speed = speed * 1.5f;
            }
            else
            {
                link.ChangeFov(60);
            }

            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(link.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, link.TurnSmoothTime);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * speed;

            moveDir.y = link.RB.velocity.y;

            link.RB.velocity = moveDir;
                
            link.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        }
        else
        {
            Vector3 stop = new Vector3(0, link.RB.velocity.y, 0);
            link.RB.velocity = stop;
        }
    }

    public override void UpdateState(LinkScpt link, StateMachineController machineController)
    {
        if (Input.GetKeyDown(KeyCode.Space) && link.RB.velocity.y < 5) link.Jump(link.transform.up, link.JumpForce);

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
