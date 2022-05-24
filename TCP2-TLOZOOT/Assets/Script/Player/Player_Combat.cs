using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    //Attack
    public bool canAttack;
    public float atkCooldown;
    public int atkCombo,atkTipo,lastAtkType;

    public Animator anim;


    public Player_Move player_Move;

    //Charge
    float charge;
    bool isCharged;

    //Ray
    Vector3 rayDirection;
    float rayHitRange;

    void Awake()
    {
        canAttack = true;
        charge = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(0)){
            if(isCharged) attack(3);
            charge = 0;
            isCharged = false;
        }else if(canAttack){
            if (Input.GetMouseButton(0)){
                attack(getAtkType());
            }                    
        }
        
        
    }

    public int getAtkType(){
        if(!player_Move.isClimb){
            if(player_Move.isGround){
                
                charge += 1;                

                if(charge >= 3){
                    isCharged = true;
                }
                if(charge >= 2){
                    return 4;
                }

                if(anim.GetBool("Walk") || anim.GetBool("Run")) return 1;
            
                else return 2;
            
            }else return 5;
        
        }return 0;
    }

    IEnumerator ResetAttackCooldown()
    {
        
        yield return new WaitForSeconds(atkCooldown);
        anim.SetBool("Attack", false);
        canAttack = true;
    }

    public void attack(int tipo)
    {
        if(tipo == lastAtkType) atkCombo++;
        else atkCombo = 0;

        anim.SetBool("Attack", true);


        if(tipo == 1){
            Debug.Log("Walking Attack");       
            rayDirection = Vector3.forward;
            rayHitRange = 1;
        }else if(tipo == 2){
            Debug.Log("Stopped Attack");
            rayDirection = new Vector3(1,0,2);
        }else if(tipo == 3){
            Debug.Log("Charged Attack");
        }else if(tipo == 4){
            Debug.Log("Preparing charged");            
        }else if(tipo == 5){
            Debug.Log("Air Attack");
        }

        HitEnemy(rayHitRange);

        lastAtkType = tipo;

        canAttack = false;
        //anim.SetTrigger("Hit");
        StartCoroutine(ResetAttackCooldown());
    }

    void HitEnemy(float rayHitRange){
        
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, rayHitRange)){
        
         if(hit.transform.gameObject.tag == "Enemy")
         {
            Debug.Log("Inimigo tomou dano");
         }
        }
    }
    
}
