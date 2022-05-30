using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RupeeCounter : MonoBehaviour
{
    private int rupeeCount = 0;

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (rupeeCount != varHolder.inst.rupees)
        {
            rupeeCount = varHolder.inst.rupees;
            text.text = rupeeCount.ToString();
        }
    }
}
