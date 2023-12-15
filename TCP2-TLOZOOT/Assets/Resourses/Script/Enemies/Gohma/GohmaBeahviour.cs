using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GohmaBeahviour : MonoBehaviour
{
    public Player_Scp instaciaPlayer;
    public Animator animator;
    public Animator doorAnimator;
    private Combat playerCombat;
    public Combat combat;

    public bool isInContactWithPlayer;

    public float speed;

    public bool isFighting;

    public bool canAttack;
    public float atkCooldown;

    private void Awake()
    {
        isFighting = false;
        this.instaciaPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Scp>();
        this.playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>();
    }

    // Update is called once per frame
    void Update()
    {
        Behaviour(this.instaciaPlayer.DistanceFromPlayer(this.transform.position));


        if (isFighting)
        {
            if (combat.life == 0)
            {
                isFighting = false;
                this.animator.SetBool("Dies", true);
                doorAnimator.SetBool("isUnlocked", true);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Puzzle") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Armature_stay stuned"))
        {
            this.animator.SetTrigger("Stun");
        }
    }

    public void OnTriggerStay(Collider collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            isInContactWithPlayer = true;            
        }
    }
    public void OnTriggerExit(Collider collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            isInContactWithPlayer = false;
        }
    }


    void LookAtPlayer()
    {
        this.transform.LookAt(this.instaciaPlayer.transform);
        this.transform.rotation = new Quaternion(0, this.transform.rotation.y, 0, this.transform.rotation.w);
    }

    void Behaviour(float distance)
    {
        

        if (distance < 75)
        {
            isFighting = true;
            this.animator.SetTrigger("GetGoing");
            doorAnimator.SetBool("isUnlocked", false);
        }

        if(isFighting && canAttack && animator.GetCurrentAnimatorStateInfo(0).IsName("Armature_walking"))
        {
            LookAtPlayer();
            Walk();
        }

        if (distance < 12f && canAttack && animator.GetCurrentAnimatorStateInfo(0).IsName("Armature_walking"))
        {
            Attack();
        }
    }

    void Walk()
    {
        Vector3 velocityWalk;

        //Mantem a velocidade em y e altera a velocidade para frente
        velocityWalk = this.transform.forward * this.speed * 0.01f;
        this.transform.position += velocityWalk;
    }

    void Attack()
    {
        int atkType = Random.Range(1, 3);
        animator.SetTrigger("Attack" + atkType);        
        canAttack = false;
        StartCoroutine(ResetAttackCooldown());
        StartCoroutine(WaitFotTriggerCombat());
    }

    private void TriggerCombat()
    {
        playerCombat.TakeKnockback(combat.knockbackforce, instaciaPlayer.transform.position - this.transform.position);
        playerCombat.TakeDamage(combat.GiveDamage(), combat.dmgModifier);
    }


    IEnumerator WaitFotTriggerCombat()
    {
        yield return new WaitForSeconds(atkCooldown / 1.8f);
        if (isInContactWithPlayer) TriggerCombat();
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(atkCooldown);
        canAttack = true;
    }
}
