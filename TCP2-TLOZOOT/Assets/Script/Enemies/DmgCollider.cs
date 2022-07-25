using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgCollider : MonoBehaviour
{
    public Player_Scp instaciaPlayer;
    private Combat playerCombat;
    public Combat enemyCombat;

    private void Awake() {
        this.instaciaPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Scp>();
        this.playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>();
    }
    
    public void OnTriggerStay(Collider collision){
        if(collision.gameObject.CompareTag("Player"))
        {
            playerCombat = collision.gameObject.GetComponent<Combat>();            
            playerCombat.TakeKnockback(enemyCombat.knockbackforce, collision.transform.position - this.transform.position);
            playerCombat.TakeDamage(enemyCombat.GiveDamage(), enemyCombat.dmgModifier);
        }
    }
}
