using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SwimState : BaseState
{
    float turnSmoothVelocity;
    public override void EndState(LinkScpt link, StateMachineController machineController)
    {
        
    }

    public override void EnterState(LinkScpt link, StateMachineController machineController)
    {
        link.RB.useGravity = false;
    }

    public override void FixedUpdateState(LinkScpt link, StateMachineController machineController)
    {

        link.RB.velocity *= 0.95f;
        if (RayAcimaFrontal(link, machineController))
        {
            link.RB.AddForce(link.transform.up * 3, ForceMode.Acceleration);
        }

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

            float angle = Mathf.SmoothDampAngle(link.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, link.TurnSmoothTime * 3);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * (speed / 2);

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
        
    }

    public bool RayAcimaFrontal(LinkScpt link, StateMachineController machineController)
    {
        //Verifica se há uma parede escalavel a frente
        Vector3 rayStartPos = link.transform.position + new Vector3(0f, 5f, 0f);

        Debug.DrawRay(rayStartPos, -link.transform.up * 4.5f, Color.green);

        if (Physics.Raycast(rayStartPos, -link.transform.up, out RaycastHit rayAcimaFrontal, 4.5f, link.WaterMask))
        {
            return true;
        }
        return false;
    }
}
