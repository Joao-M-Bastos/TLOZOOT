using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public int life, damage;

    public float knockbackforce;

    public bool isKnockbackResistant, isVulnerable;

    private Rigidbody rb;

    public void Awake(){
        if(this.gameObject.GetComponent<Rigidbody>() != null){
            rb = this.gameObject.GetComponent<Rigidbody>();
        }
    }

    public void TakeDamage(int damage){
        if(isVulnerable){
            this.life -= damage;
            isVulnerable = false;
            StartCoroutine(ResetVulnerableCooldown());
        }
    }

    public int GiveDamage(){
        return this.damage;
    }

    public void TakeKnockback(float knockbackforce, Vector3 diretion){
        if(!isKnockbackResistant && isVulnerable){
            rb.isKinematic = false;
            rb.AddForce(transform.up * knockbackforce, ForceMode.VelocityChange);
            rb.AddForce(diretion * knockbackforce, ForceMode.VelocityChange);
        }
    }

    public float GiveKnockback(){
        return this.knockbackforce;
    }

    IEnumerator ResetVulnerableCooldown()
    {        
        yield return new WaitForSeconds(0.8f);
        isVulnerable = true;
    }
}
