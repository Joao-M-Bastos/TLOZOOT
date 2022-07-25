using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public int life, damage;

    public float knockbackforce, dmgModifier;

    public bool isKnockbackResistant, isVulnerable;

    private Rigidbody rb;

    public void Awake(){
        if(this.gameObject.GetComponent<Rigidbody>() != null){
            rb = this.gameObject.GetComponent<Rigidbody>();
        }
    }

    public void TakeDamage(float damage, float modifier){
        if(isVulnerable){
            damage *= modifier;
            this.life -= (int)damage;
            isVulnerable = false;
            StartCoroutine(ResetVulnerableCooldown());
        }
    }

    public int GiveDamage(){
        return this.damage;
    }

    public void TakeKnockback(float knockbackforce, Vector3 diretion){
        if(!isKnockbackResistant && isVulnerable){
            this.rb.velocity = new Vector3(0,0,0);
            rb.AddForce(transform.up * knockbackforce / 2, ForceMode.VelocityChange);
            rb.AddForce(diretion * knockbackforce / 4, ForceMode.VelocityChange);
        }
    }

    public float GiveKnockback(){
        return this.knockbackforce;
    }

    public IEnumerator ResetVulnerableCooldown()
    {        
        yield return new WaitForSeconds(0.8f);
        isVulnerable = true;
    }
}
