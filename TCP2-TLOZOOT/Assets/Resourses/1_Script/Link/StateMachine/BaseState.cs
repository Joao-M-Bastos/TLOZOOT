using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    // Start is called before the first frame update
    public abstract void EnterState(LinkScpt link, StateMachineController machineController);

    // Update is called once per frame
    public abstract void UpdateState(LinkScpt link, StateMachineController machineController);
    
    public abstract void EndState(LinkScpt link, StateMachineController machineController);
}
