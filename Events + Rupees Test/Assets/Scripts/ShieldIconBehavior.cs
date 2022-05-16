using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldIconBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(varHolder.inst.flagNPCDialogue4 == true && GetComponent<Image>().enabled == false)
            GetComponent<Image>().enabled = true;
    }
}
