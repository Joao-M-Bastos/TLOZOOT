using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RupeeCounter : MonoBehaviour
{
    private int rupeeCount = 0;

    public Text text;

    private void Awake() {
        this.text = GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
        this.text.text = "0";
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
