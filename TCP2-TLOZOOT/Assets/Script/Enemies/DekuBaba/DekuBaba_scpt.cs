using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DekuBaba_scpt : MonoBehaviour
{
    public Player_Scp instaciaPlayer;
    public Animator animator;
    public Combat combat;
    public GameObject gameObject;

    public bool canAttack;
    public float atkCooldown;

    private float distanceFromPlayer;

    private void Awake() {
        this.instaciaPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Scp>();       
    }

    // Update is called once per frame
    void Update()
    {
        Behaviour(this.instaciaPlayer.DistanceFromPlayer(this.transform.position));
        
        if(!isAttacking()){
            LookAtPlayer();
        }

        if(combat.life == 0){
            Destroy(this.gameObject);
        }

    }

    void LookAtPlayer(){
        this.transform.LookAt(instaciaPlayer.transform);
        this.transform.rotation = new Quaternion(0,this.transform.rotation.y,0,this.transform.rotation.w);
    }

    float PlayerDistance(){
        return Vector3.Distance(this.transform.position, instaciaPlayer.transform.position);
    }

    void Behaviour(float distance){
        if(distance < 15){
            this.animator.SetBool("isShown", true);
        }else this.animator.SetBool("isShown", false);

        if(distance < 7.5f && canAttack){
            Attack();
        }
    }

    void Attack(){
        animator.SetBool("Attack", true);
        canAttack = false;
        StartCoroutine(ResetAttackCooldown());
    }

    bool isAttacking(){
        if (this.animator.GetBool("Attack"))
        {
            return true;
        }
        return false;
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(atkCooldown);
        canAttack = true;
    }
}
