using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{
    public Combat playerCombat;
    public Player_Attack playerAttack;


    private Combat enemyCombat;
    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;

    public void Awake() {
        boxCollider = this.GetComponent<BoxCollider>();
        meshRenderer = this.GetComponent<MeshRenderer>();
    }
    
    public void OnTriggerEnter(Collider collision){
        if(collision.gameObject.tag == "Enemy"){
            enemyCombat = collision.gameObject.GetComponent<Combat>();
            enemyCombat.TakeKnockback(playerCombat.knockbackforce, playerCombat.transform.TransformDirection(Vector3.forward));
            enemyCombat.TakeDamage(playerCombat.GiveDamage(), playerCombat.dmgModifier);
        }
    }

    public void Update() {
        
        if(!playerAttack.hasSword) meshRenderer.enabled = false;
        else meshRenderer.enabled = true;
        
        if(playerAttack.hasAttacked) boxCollider.enabled = true;
        else boxCollider.enabled = false;
    }
}
