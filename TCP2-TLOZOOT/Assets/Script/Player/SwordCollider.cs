using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    public Combat playerCombat;
    private Combat enemyCombat;
    
    public void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Enemy"){
            enemyCombat = collision.gameObject.GetComponent<Combat>();
            enemyCombat.TakeKnockback(playerCombat.knockbackforce, playerCombat.transform.TransformDirection(Vector3.forward));
            enemyCombat.TakeDamage(playerCombat.GiveDamage());
        }
    }
}
