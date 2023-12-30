using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    public Player_Scp instaciaPlayer;

    //Walk
    public float baseSpeed;
    public float speedClimb;    
    private float InputX, InputZ;

    //Jump
    public float jumpforce;

    //Camera
    [SerializeField] Transform cameraTransform;
    [SerializeField] float turnSmoothVelocity, turnSmoothTime;

    private void Awake()
    {
        this.instaciaPlayer.Speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInstances();

        if(this.instaciaPlayer.CanWalk()){
            //Se jogador pode escalar e 
                if(this.instaciaPlayer.CanClimb()){
                    //se ele nn estiver escalando ele escala
                    if(!this.instaciaPlayer.IsClimb) StartClimb();
                //se ele nn puder e estiver escalando ele para de escalar
                }else if((this.instaciaPlayer.IsClimb)){
                    StopClimb();
                }

            if(this.IsGrounded() || this.instaciaPlayer.IsClimb || this.instaciaPlayer.IsInCorner
               || this.instaciaPlayer.IsinWater)
            {
                //Verifica se o jogador que mover
                if(InputX != 0 || InputZ != 0)
                {
                    //Se ele estiver escalando movimentação de escalar
                    if(this.instaciaPlayer.IsInCorner){
                        Corner();
                    }else if(this.instaciaPlayer.IsClimb){
                        Climb();
                    }else{
                        Walk();
                    }
                }else{
                    if(this.instaciaPlayer.IsClimb || this.instaciaPlayer.IsGrounded()){
                        this.instaciaPlayer.ResetSpeed();
                    }
                    this.instaciaPlayer.PrefebAnimScp.IsWalking = false;
                }
            }
        }else this.instaciaPlayer.PrefebAnimScp.IsWalking = false;

        if (this.instaciaPlayer.HasAttacked){
            this.instaciaPlayer.ResetSpeed();
        }


        //Pulo
        if(Input.GetKeyDown(KeyCode.Space)) Jump();
    }


    //Instancias de movimentação necessárias
    public void UpdateInstances(){
        //Rotação e direção frontal atual do jogador
        this.instaciaPlayer.PlayerRotation = new Quaternion(0,transform.rotation.y,0,transform.rotation.w);        
        this.instaciaPlayer.PlayerForword = this.transform.TransformDirection(Vector3.forward);
        this.instaciaPlayer.PlayerRight = this.transform.TransformDirection(Vector3.right);

        //Dircão que o jogador vai querer mover
        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

    }

    void Corner(){
        this.instaciaPlayer.PrefebAnimScp.ResetAllAnimations();

        this.instaciaPlayer.PrefebAnimScp.IsInCorner = true;

        if (Input.GetKeyDown(KeyCode.W) && this.instaciaPlayer.IsInCornerAnimation == false)
        {
            this.instaciaPlayer.ParentAnim.SetBool("ClimbCorner", true);

            this.instaciaPlayer.IsInCornerAnimation = true;

            //this.instaciaPlayer.StopClimbCorner();

            this.StartCoroutine(ResetCornerAnimation());

            this.transform.Translate(new Vector3(0, 3.1f, 1.5f));
        }
    }

    IEnumerator ResetCornerAnimation()
    {
        yield return new WaitForSeconds(0.8f);
        this.instaciaPlayer.StopClimbCorner();
    }

    void StartClimb(){
        //Prepara variaveis para escalada
        this.instaciaPlayer.IsClimb = true;
        this.instaciaPlayer.IsLocked = false;
        this.instaciaPlayer.Rb.useGravity = false;

        this.instaciaPlayer.PrefebAnimScp.ResetAllAnimations();

        this.instaciaPlayer.PrefebAnimScp.IsClimbing = true;
    }

    void Climb(){
        Vector3 velocityClimb;

        //O jogador fica sempre com a cara na parede
        this.instaciaPlayer.PlayerRotation = Quaternion.LookRotation(-this.instaciaPlayer.WallHit.normal);

        //direcaoClimb = new Vector3(-InputX, InputZ, 0) * this.instaciaPlayer.PlayerRight.x;

        velocityClimb = this.instaciaPlayer.PlayerRight * InputX;
        velocityClimb.y = InputZ;

        //Movimenta o jogador nos eixos X e Y

        this.instaciaPlayer.Rb.velocity = velocityClimb * speedClimb;
    }

    void StopClimb(){
        //Limpa as variaves quando parar de escalar
        this.instaciaPlayer.IsClimb = false;
        this.instaciaPlayer.Rb.useGravity = true;

        this.instaciaPlayer.PrefebAnimScp.IsClimbing = false;
    }

    void Walk() {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        Run();

        if (moveDirection.magnitude >= 0.1f)
        {

            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * instaciaPlayer.Speed;

            moveDir.y = instaciaPlayer.Rb.velocity.y;

            if (Mathf.Abs(instaciaPlayer.Rb.velocity.x) + Mathf.Abs(instaciaPlayer.Rb.velocity.z) <= 7.5f)
            {
                instaciaPlayer.Rb.velocity = moveDir;
                instaciaPlayer.transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
        }
        
    }
    void Jump(){

        float jumpforceAlterTransform = 0;
        float jumpforceAlterForce = 0;

        Vector3 jumpDirection = Vector3.zero;

        if(this.instaciaPlayer.IsInCorner){
            jumpforceAlterTransform = jumpforce * -0.1f;            
            this.instaciaPlayer.StopClimbCorner();
        }
        
        else if(this.instaciaPlayer.IsClimb && !this.instaciaPlayer.IsGrounded()){
            jumpforceAlterTransform = jumpforce * -0.1f;
            StopClimb();
        }
        
        else if (this.instaciaPlayer.IsGrounded() && !this.instaciaPlayer.CanSwim()){
            jumpforceAlterForce = jumpforce;
            jumpDirection = transform.up;
        }
        
        else if (CanDive())
        {
            jumpforceAlterForce = jumpforce/1.5F;
            jumpDirection = -transform.up;
            
        }

        this.instaciaPlayer.Rb.isKinematic = false;

        this.instaciaPlayer.transform.Translate(0, 0, jumpforceAlterTransform);
        this.instaciaPlayer.Rb.AddForce(jumpDirection * jumpforceAlterForce, ForceMode.Impulse);

        
    }    

    public bool CanDive()
    {
        if (this.instaciaPlayer.CanSwim())
        {
            Vector3 playerPos;
            playerPos = this.instaciaPlayer.Rb.transform.position;

            if(Vector3.Distance(this.transform.position, this.instaciaPlayer.SwimHit.point) < 4)
            {
                return true;
            }            
        }
        return false;

    }

    void Run(){
        if(Input.GetKey(KeyCode.LeftShift)) {
            this.instaciaPlayer.Speed = baseSpeed * 1.3f;
            this.instaciaPlayer.PrefebAnimScp.IsRunning = true;
        }
        else {
            this.instaciaPlayer.Speed = baseSpeed;
            this.instaciaPlayer.PrefebAnimScp.IsRunning =  false;
        }
    }

    public bool IsGrounded(){

        Vector3 rayStartPos = this.transform.position + new Vector3(0, 1f, 0);
        if (Physics.Raycast(rayStartPos, -transform.up, out _, 2f, this.instaciaPlayer.GroundLayerMask)){
            return true;
        }        
        return false;
    }
}
