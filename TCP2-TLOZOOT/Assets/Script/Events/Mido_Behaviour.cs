using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mido_Behaviour : MonoBehaviour
{
    public Player_Attack playerAttack;
    public Animator anim;

    private void OnTriggerEnter(Collider other) {
        if(playerAttack.hasShild && playerAttack.hasSword){
            this.anim.SetBool("GetOut", true);
        }
    }
    
}
