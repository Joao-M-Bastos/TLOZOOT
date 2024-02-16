using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkCombat : MonoBehaviour
{
    private bool hasSword, hasSling;

    public void GetSword()
    {
        if(hasSword == false)
            hasSword = true;
    }

    public void GetSling()
    {
        if(hasSling == false)
            hasSling = true;
    }
}
