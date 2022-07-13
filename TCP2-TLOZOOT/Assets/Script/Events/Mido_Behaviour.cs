using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mido_Behaviour : MonoBehaviour
{
    public Player_Scp instaciaPlayer;
    public Animator anim;
    private Combat combat;

    private void Awake() {
        this.instaciaPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Scp>();
        combat = this.gameObject.GetComponent<Combat>();
    }

    public void Update(){
        if(combat.life == 0){
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(this.instaciaPlayer.HasShild && this.instaciaPlayer.HasSword){
            this.anim.SetTrigger("GetOut");
        }
    }
    
}
