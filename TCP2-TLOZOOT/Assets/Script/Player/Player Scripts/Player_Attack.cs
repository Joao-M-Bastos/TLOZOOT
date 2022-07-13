using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Player_Scp instaciaPlayer;
    public LayerMask enemyLayerMask;

    //Attack
    public bool canAttack;
    public float atkCooldown;
    public int atkCombo,atkTipo,lastAtkType;    

    //Charge
    float charge;
    bool isCharged;

    void Awake()
    {
        canAttack = true;
        charge = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0)){
            if(isCharged) attack(3);
            charge = 0;
            isCharged = false;
        }else if(Input.GetMouseButton(0) && canAttack && this.instaciaPlayer.HasSword){
            attack(getAtkType());
        }
        //HitEnemy(5);
    }

    public int getAtkType(){
        if(!this.instaciaPlayer.IsClimb){
            if(this.instaciaPlayer.isGrounded()){
                
                charge += 1;

                if(charge >= 3){
                    isCharged = true;
                }
                if(charge >= 2){
                    return 4;
                }
                if(this.instaciaPlayer.PrefebAnim.GetBool("Walk") || this.instaciaPlayer.PrefebAnim.GetBool("Run")) return 1;
                else return 2;
            
            }else return 5;
        
        }return 0;
    }

    IEnumerator ResetAttackCooldown()
    {        
        yield return new WaitForSeconds(atkCooldown);
        canAttack = true;
        this.instaciaPlayer.HasAttacked = false;
    }

    public void attack(int tipo)
    {
        if(tipo == lastAtkType) atkCombo++;
        else atkCombo = 0;

        this.instaciaPlayer.PrefebAnim.SetBool("Attack", true);


        if(tipo == 1){
            Debug.Log("Walking Attack");
        }else if(tipo == 2){
            Debug.Log("Stopped Attack");
        }else if(tipo == 3){
            Debug.Log("Charged Attack");
        }else if(tipo == 4){
            Debug.Log("Preparing charged");            
        }else if(tipo == 5){
            Debug.Log("Air Attack");
        }

        lastAtkType = tipo;

        canAttack = false;
        this.instaciaPlayer.HasAttacked = true;
        StartCoroutine(ResetAttackCooldown());
    }
}
