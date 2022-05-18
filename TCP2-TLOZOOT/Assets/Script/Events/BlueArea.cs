using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueArea : MonoBehaviour
{
    [SerializeField]
    GameObject player;

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
        if (other.gameObject == player && varHolder.inst.flagBlueAreaDone == false && varHolder.inst.flagNPCDialogue2 == true)
        {
            Debug.Log("Successfully reached the blue area!");
            varHolder.inst.flagBlueAreaDone = true;
        }
    }
}
