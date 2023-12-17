using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class varHolder : MonoBehaviour
{
    public static varHolder inst;
    public static varHolder Instance
    {
        get
        {
            return inst;
        }
    }

    public bool flagNPCDialogue1 = false;
    public bool flagRedAreaDone = false;
    public bool flagNPCDialogue2 = false;
    public bool flagBlueAreaDone = false;
    public bool flagNPCDialogue3 = false;
    public bool flagNPCDialogue4 = false;
    public int rupees = 0;

    void Awake()
    {
        if (inst != null && inst != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            inst = this;
        }
    }
}
