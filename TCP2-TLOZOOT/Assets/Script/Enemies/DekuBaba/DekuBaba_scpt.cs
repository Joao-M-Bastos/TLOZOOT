using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DekuBaba_scpt : MonoBehaviour
{
    public Player_Combat player_Combat;
    public Animator animator;
    public Combat combat;

    public bool canAttack;
    public float atkCooldown;

    private float distanceFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = PlayerDistance();
        Behaviour(distanceFromPlayer);

        if(!isAttacking()){
            LookAtPlayer();
        }

        if(combat.life == 0){
            Destroy(this.gameObject);
        }

    }    

    void LookAtPlayer(){
        this.transform.LookAt(player_Combat.transform);
        this.transform.rotation = new Quaternion(0,this.transform.rotation.y,0,this.transform.rotation.w);
    }

    float PlayerDistance(){
        return Vector3.Distance(this.transform.position, player_Combat.transform.position);
    }

    void Behaviour(float distance){
        if(distance < 10){
            this.animator.SetBool("isShown", true);
        }else this.animator.SetBool("isShown", false);

        if(distance < 3){
            if(canAttack) Attack();
        }
    }

    void Attack(){
        animator.SetBool("Attack", true);
        canAttack = false;
        StartCoroutine(ResetAttackCooldown());
    }

    bool isAttacking(){
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("DekuBaba_Attack"))
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
