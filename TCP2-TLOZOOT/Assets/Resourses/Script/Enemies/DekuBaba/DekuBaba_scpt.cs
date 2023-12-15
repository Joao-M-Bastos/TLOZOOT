using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DekuBaba_scpt : MonoBehaviour
{
    public Player_Scp instaciaPlayer;
    public Animator animator;
    public Combat combat;
    public Transform parentTranform;

    public bool canAttack;
    public float atkCooldown;

    private float distanceFromPlayer;

    private void Start() {
        this.instaciaPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Scp>();

    }

    // Update is called once per frame
    void Update()
    {
        Behaviour(this.instaciaPlayer.DistanceFromPlayer(this.transform.position));

        Debug.Log(instaciaPlayer.gameObject.name);

        if(!isAttacking()){
            LookAtPlayer();
        }

        if(combat.life == 0){
            Destroy(this.gameObject);
        }

    }

    void LookAtPlayer(){
        //parentTranform.Rotate(0, 1, 0);
        this.parentTranform.LookAt(instaciaPlayer.transform.position);
        this.parentTranform.rotation = new Quaternion(0, this.transform.rotation.y ,0,this.transform.rotation.w);
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
        animator.SetTrigger("Attack");
        canAttack = false;
        StartCoroutine(ResetAttackCooldown());
    }

    bool isAttacking(){
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Armature_attack");
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(atkCooldown);
        canAttack = true;
    }
}
