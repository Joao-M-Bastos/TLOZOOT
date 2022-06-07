using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : MonoBehaviour
{
    public Player_Attack playerAttack;
    private MeshRenderer meshRenderer;

    private void Awake() {
        meshRenderer = this.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerAttack.hasShild) meshRenderer.enabled = false;
        else meshRenderer.enabled = true;
    }
}
