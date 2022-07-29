using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    public Combat playerCombat;
    public Player_Scp instaciaPlayer;
    public Animator anim;

    public int i;

    public bool isOpened;

    private void Awake() {
        this.instaciaPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Scp>();
        this.playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>();
    }

    private void OnTriggerStay(Collider collision) {
        if(Input.GetKey(KeyCode.E) && !isOpened){
            isOpened = true;
            playerCombat.dmgModifier = 1;
            this.anim.SetBool("Open", true);
            if (i == 0)
            {                
                this.instaciaPlayer.HasSword = true;
            }else if(i == 1)
            {
                this.instaciaPlayer.HasSlingShoot = true;
            }
            

        }
    }
}
