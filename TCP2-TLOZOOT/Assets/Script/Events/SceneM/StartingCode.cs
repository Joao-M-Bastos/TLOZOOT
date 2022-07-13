using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingCode : MonoBehaviour
{
    public int startingCode;

    public int StartingCodeGS{
        set{this.startingCode = value;}
        get{return this.startingCode;}
    }
}
