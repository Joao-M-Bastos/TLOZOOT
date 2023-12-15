using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour
{

    public Player_Scp instaciaPlayer;


    public Combat playerCombat;


    private Combat enemyCombat;
    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;

    public void Awake() {
        boxCollider = this.GetComponent<BoxCollider>();
        meshRenderer = this.GetComponent<MeshRenderer>();
    }
    
    public void OnTriggerEnter(Collider collision){
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("b");
            enemyCombat = collision.gameObject.GetComponent<Combat>();
            enemyCombat.TakeKnockback(playerCombat.knockbackforce, playerCombat.transform.TransformDirection(Vector3.forward));
            enemyCombat.TakeDamage(playerCombat.GiveDamage(), playerCombat.dmgModifier);
        }
    }

    public void Update() {
        
        if(!this.instaciaPlayer.HasSword) meshRenderer.enabled = false;
        else meshRenderer.enabled = true;
        
        if(this.instaciaPlayer.HasAttacked) boxCollider.enabled = true;
        else boxCollider.enabled = false;
    }
}
