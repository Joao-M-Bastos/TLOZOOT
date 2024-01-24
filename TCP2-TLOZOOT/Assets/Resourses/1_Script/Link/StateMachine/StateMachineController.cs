using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineController : MonoBehaviour
{
    LinkScpt linkScript;

    BaseState currentState;

    public GroundedState groundedState = new GroundedState();
    public ClimbingState climbimgState = new ClimbingState();

    private void Awake()
    {
        linkScript = GetComponent<LinkScpt>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = groundedState;
        currentState.EnterState(linkScript, this);
    }

    public void ChangeState(BaseState newState)
    {
        currentState.EndState(linkScript, this);
        currentState = newState;
        currentState.EnterState(linkScript, this);
    }

    public void FixedUpdate()
    {
        currentState.UpdateState(linkScript, this);
    }
}
