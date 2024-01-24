using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundedState : BaseState
{
    float turnSmoothVelocity;
    public override void EnterState(LinkScpt link, StateMachineController machineController)
    {
        throw new System.NotImplementedException();
    }

    public override void EndState(LinkScpt link, StateMachineController machineController)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(LinkScpt link, StateMachineController machineController)
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

            float speed = link.baseSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = speed * 1.3f;
            }

            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(link.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, link.TurnSmoothTime);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * speed;

            moveDir.y = link.linkRigidbody.velocity.y;

            link.linkRigidbody.velocity = moveDir;
                
            link.transform.rotation = Quaternion.Euler(0f, angle, 0f);

        }
        else
        {
            Vector3 stop = new Vector3(0, link.linkRigidbody.velocity.y, 0);
            link.linkRigidbody.velocity = stop;
        }

        //if (Input.GetKeyDown(KeyCode.Space)) Jump();

        TryChangeState(link, machineController);
    }

    public void TryChangeState(LinkScpt link, StateMachineController machineController)
    {

        if (Physics.Raycast(link.transform.position + new Vector3(0, 0.4f, 0), link.transform.forward, out RaycastHit wallHit, 1.2f, link.climbLayerMask))
        {
            link.transform.rotation = Quaternion.LookRotation(-wallHit.normal);
            machineController.ChangeState(machineController.climbimgState);
        }
    }
}
