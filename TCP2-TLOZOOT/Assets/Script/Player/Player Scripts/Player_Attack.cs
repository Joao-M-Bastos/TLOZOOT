using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Player_Scp instaciaPlayer;
    public LayerMask enemyLayerMask;
    public Animator slingshotAnimator;



    //Attack
    public bool canAttack;
    public float atkCooldown;
    public int atkCombo,atkTipo,lastAtkType;
    private float chargeRenged;


    //Charge
    float charge;
    bool isCharged;

    //SlingShot
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed;

    void Awake()
    {

        canAttack = true;
        charge = 0;
        chargeRenged = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && instaciaPlayer.HasSlingShoot)
        {            
            this.instaciaPlayer.IsAiming = !this.instaciaPlayer.IsAiming;
        }

        if (this.instaciaPlayer.IsAiming)
        {
            if (Input.GetButton("Fire1") && this.instaciaPlayer.CanShoot)
            {
                ShootCharge();
            }
            if (Input.GetButtonUp("Fire1"))
            {
                Shoot();
            }
        }else if (Input.GetMouseButtonDown(1) && this.instaciaPlayer.HasShild){
            Block();
        }else if (Input.GetMouseButtonUp(0)){
            if(isCharged) Attack(3);
            charge = 0;
            isCharged = false;
        }else if(Input.GetMouseButtonDown(0) && canAttack && this.instaciaPlayer.HasSword){
            Attack(GetAtkType());
        }

        if (Input.GetMouseButtonUp(1) && this.instaciaPlayer.PrefebAnimScp.IsBlocking)
        {
            UnBlock();
        }
    }

    public int GetAtkType(){
        if(!this.instaciaPlayer.IsClimb){
            if(this.instaciaPlayer.IsGrounded()){
                
                charge += 1;

                if(charge >= 3){
                    isCharged = true;
                }
                if(charge >= 2){
                    return 4;
                }
                if(this.instaciaPlayer.PrefebAnimScp.IsWalking) return 1;
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

    public void Block()
    {
        this.instaciaPlayer.ResetSpeed();
        this.instaciaPlayer.PrefebAnimScp.ResetAllAnimations();
        this.instaciaPlayer.PrefebAnimScp.IsBlocking = true;
        this.instaciaPlayer.PlayerCombat.isVulnerable = false;
    }

    void UnBlock()
    {
        this.instaciaPlayer.PrefebAnimScp.IsBlocking = false;
        this.instaciaPlayer.PlayerCombat.isVulnerable = true;
    }

    public void Attack(int tipo)
    {
        if(tipo == lastAtkType) atkCombo++;
        else atkCombo = 0;

        this.instaciaPlayer.PrefebAnimScp.TrigAttack = true;


        switch (tipo)
        {
            case 5:
                Debug.Log("Air Attack");
                break;
            case 4:
                Debug.Log("Preparing charged");
                break;
            case 3:
                Debug.Log("Charged Attack");
                break;
            case 2:
                Debug.Log("Stopped Attack");
                break;
            case 1:
                Debug.Log("Walking Attack");
                break;
            default:
                print("Incorrect intelligence level.");
                break;
        }

        lastAtkType = tipo;

        canAttack = false;
        this.instaciaPlayer.HasAttacked = true;
        StartCoroutine(ResetAttackCooldown());
    }

    public void ShootCharge()
    {
        this.chargeRenged += 1 * Time.deltaTime;
        if (!this.slingshotAnimator.GetBool("Charge"))
        {
            this.slingshotAnimator.SetBool("Charge", true);
        }
        
    }

    public void Shoot()
    {

        if(this.chargeRenged > 1)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
        this.slingshotAnimator.SetBool("Charge", false);
        this.chargeRenged = 0;
    }


}

