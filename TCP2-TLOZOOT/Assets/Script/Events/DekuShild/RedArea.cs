using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedArea : MonoBehaviour
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
        if (other.gameObject == player && varHolder.inst.flagRedAreaDone == false && varHolder.inst.flagNPCDialogue1 == true)
        {
            Debug.Log("Successfully reached the red area!");
            varHolder.inst.flagRedAreaDone = true;
        }
    }
}
