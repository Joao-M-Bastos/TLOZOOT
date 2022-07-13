using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public Combat playerCombat;
    public Player_Scp instaciaPlayer;
    public Animator anim;

    public bool isOpened;

    private void Awake() {
        this.instaciaPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Scp>();
        this.playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>();
    }

    private void OnTriggerStay(Collider collision) {
        if(Input.GetKey(KeyCode.E) && !isOpened){
            isOpened = true;
            playerCombat.dmgModifier++;
            this.instaciaPlayer.HasSword = true;
            this.anim.SetBool("Open", true);
        }
    }
}
