using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueArea : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player && varHolder.inst.flagBlueAreaDone == false && varHolder.inst.flagNPCDialogue2 == true)
        {
            Debug.Log("Successfully reached the blue area!");
            varHolder.inst.flagBlueAreaDone = true;
        }
    }
}
