using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgCollider : MonoBehaviour
{
    private Combat playerCombat;
    public Combat enemyCombat;
    
    public void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Player"){
            playerCombat = collision.gameObject.GetComponent<Combat>();
            playerCombat.TakeKnockback(enemyCombat.knockbackforce, enemyCombat.transform.forward);
            playerCombat.TakeDamage(enemyCombat.GiveDamage());
        }
    }
}
