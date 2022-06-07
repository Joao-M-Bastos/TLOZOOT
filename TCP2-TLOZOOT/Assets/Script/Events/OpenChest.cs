using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public Combat playerCombat;
    public Player_Attack playerAttack;
    public Animator anim;

    public bool isOpened;

    private void OnTriggerStay(Collider collision) {
        if(Input.GetKey(KeyCode.E) && !isOpened){
            isOpened = true;
            playerCombat.dmgModifier++;
            playerAttack.hasSword = true;
            this.anim.SetBool("Open", true);
        }
    }
}
