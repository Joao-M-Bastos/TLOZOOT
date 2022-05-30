using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldIconBehavior : MonoBehaviour
{
    public Image img;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {        
        img.enabled = false;
        img.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(varHolder.inst.flagNPCDialogue4 == true && img.enabled == false)
            img.enabled = true;
    }
}
