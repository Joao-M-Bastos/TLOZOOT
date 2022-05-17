using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RupeeCounter : MonoBehaviour
{
    private int rupeeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (rupeeCount != varHolder.inst.rupees)
        {
            rupeeCount = varHolder.inst.rupees;
            GetComponent<Text>().text = rupeeCount.ToString();
        }
    }
}
