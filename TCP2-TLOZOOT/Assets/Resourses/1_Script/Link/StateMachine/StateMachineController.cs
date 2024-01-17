using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineController : MonoBehaviour
{
    LinkScpt linkScript;

    BaseState currentState;

    GroundedState groundedState = new GroundedState();
    ClimbingState climbimgState = new ClimbingState();

    private void Awake()
    {
        linkScript = GetComponent<LinkScpt>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(groundedState);
    }

    public void ChangeState(BaseState newState)
    {
        currentState.EndState(linkScript, this);
        currentState = newState;
        currentState.EnterState(linkScript, this);
    }

    public void Update()
    {
        currentState.UpdateState(linkScript, this);
    }
}
