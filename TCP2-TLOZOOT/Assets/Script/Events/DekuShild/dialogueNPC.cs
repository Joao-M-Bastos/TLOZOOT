using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogueNPC : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    public Player_Attack playerAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        /*
        if (other.gameObject == player && varHolder.inst.flagNPCDialogue1 == false)
        {
            Debug.Log("\"Thanks for talking to me! Please go get some Rupees!\"");
            Debug.Log("QUEST STARTED: Generic Quest");
            varHolder.inst.flagNPCDialogue1 = true;
        }

        if (other.gameObject == player && varHolder.inst.flagRedAreaDone == true && varHolder.inst.flagNPCDialogue2 == false)
        {
            Debug.Log("\"Good job! Now please head over to the blue area!\"");
            varHolder.inst.flagNPCDialogue2 = true;
        }
        */

        if (other.gameObject == player  && varHolder.inst.flagNPCDialogue3 == false)
        {
            Debug.Log("\"Thanks for talking to me! Now please bring me 25 rupees and I'll give you this Deku Shield!\"");
            varHolder.inst.flagNPCDialogue3 = true;
        }

        if (other.gameObject == player && varHolder.inst.rupees >= 25 && varHolder.inst.flagNPCDialogue3 == true && varHolder.inst.flagNPCDialogue4 == false)
        {
            varHolder.inst.rupees -= 25;
            Debug.Log("\"Good job! Here's your Deku Shield!\"");
            Debug.Log("QUEST COMPLETED: Generic Quest");
            varHolder.inst.flagNPCDialogue4 = true;
            playerAttack.hasShild = true;
        }
    }
}
