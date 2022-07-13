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

    private void Awake()
    {
        this.instaciaPlayer.Speed = baseSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInstances();

        

        if(this.instaciaPlayer.canWalk()){
            //Se jogador pode escalar e 
                if(this.instaciaPlayer.canClimb()){
                    //se ele nn estiver escalando ele escala
                    if(!this.instaciaPlayer.IsClimb) StartClimb();
                //se ele nn puder e estiver escalando ele para de escalar
                }else if((this.instaciaPlayer.IsClimb)){
                    StopClimb();
                }


            if(this.isGrounded() || this.instaciaPlayer.IsClimb || this.instaciaPlayer.IsInCorner){
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
                    if(this.instaciaPlayer.IsClimb || this.instaciaPlayer.isGrounded()){
                        ResetSpeed();
                    }
                    this.instaciaPlayer.PrefebAnim.SetBool("Walk", false);
                }
            }
        }if(this.instaciaPlayer.HasAttacked){
            ResetSpeed();
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
        if(Input.GetKeyDown(KeyCode.W)){
            this.instaciaPlayer.transform.Translate(new Vector3(0,0,1) * 1.5f);
            this.instaciaPlayer.transform.Translate(this.transform.up * 4);
            this.instaciaPlayer.StopClimbCorner();
            this.instaciaPlayer.ParentAnim.SetBool("ClimbCorner", true);
            this.instaciaPlayer.IsInCornerAnimation = true;
            this.StartCoroutine(ResetCornerAnimation());
        }
    }

    IEnumerator ResetCornerAnimation()
    {
        yield return new WaitForSeconds(1);
        this.instaciaPlayer.IsInCornerAnimation = false;
    }

    void StartClimb(){
        //Prepara variaveis para escalada
        this.instaciaPlayer.IsClimb = true;
        this.instaciaPlayer.IsLocked = false;
        this.instaciaPlayer.Rb.useGravity = false;
        this.instaciaPlayer.PrefebAnim.SetTrigger("StartClimb");
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
        this.instaciaPlayer.PrefebAnim.SetBool("StopClimb", true);
    }

    void Walk(){
        Vector3 velocityWalk;

        var direcao = new Vector3(InputX, 0, InputZ);

        //Mantem a velocidade em y e altera a velocidade para frente
        velocityWalk = this.instaciaPlayer.PlayerForword * this.instaciaPlayer.Speed * InputZ;
        velocityWalk.y = this.instaciaPlayer.Rb.velocity.y;
        
        Run();

        if(!this.instaciaPlayer.IsLocked){
            //Se não está travado a camera rotaciona
            var camerarot = this.instaciaPlayer.transform.rotation;
            camerarot.x = 0;
            camerarot.z = 0;

            this.instaciaPlayer.PrefebAnim.SetBool("Walk", true );
            this.instaciaPlayer.Rb.velocity = velocityWalk;

            //Rotaciona o jogador
            this.instaciaPlayer.PlayerRotation = Quaternion.Lerp(this.instaciaPlayer.PlayerRotation, Quaternion.LookRotation(direcao)*camerarot, 2f * Time.deltaTime);
        }else{
            //Movimenta o jogador nos eixo X e Z
            this.instaciaPlayer.transform.Translate(direcao * this.instaciaPlayer.Speed * Time.deltaTime / 2);
        }
    }

    void Jump(){
        if(this.instaciaPlayer.IsInCorner){                
            this.instaciaPlayer.transform.Translate(0,0, jumpforce * -0.1f);
            this.instaciaPlayer.StopClimbCorner();

        }else if(this.instaciaPlayer.IsClimb){
            if (!this.instaciaPlayer.isGrounded())
            {
                this.instaciaPlayer.Rb.isKinematic = false;               
                this.instaciaPlayer.transform.Translate(0,0, jumpforce * -0.1f);
                StopClimb();
            }
        }else if(this.instaciaPlayer.isGrounded()){
            this.instaciaPlayer.Rb.isKinematic = false;
            this.instaciaPlayer.Rb.AddForce(transform.up * jumpforce, ForceMode.Impulse);
        }      
    }

    void ResetSpeed(){
        Vector3 velocityWalk;

        velocityWalk = this.instaciaPlayer.PlayerForword * Time.deltaTime;
        velocityWalk.y = this.instaciaPlayer.Rb.velocity.y;

        if(this.instaciaPlayer.Rb.velocity.x < 0.01f && this.instaciaPlayer.IsClimb){
            velocityWalk = new Vector3(0,0,0);
        }

        this.instaciaPlayer.Rb.velocity = velocityWalk;
    }

    void Run(){
        if(Input.GetKey(KeyCode.LeftShift)) {this.instaciaPlayer.Speed = baseSpeed * 1.3f; this.instaciaPlayer.PrefebAnim.SetBool("Run", true);}
        else {this.instaciaPlayer.Speed = baseSpeed; this.instaciaPlayer.PrefebAnim.SetBool("Run", false);}
    }

    public bool isGrounded(){
        RaycastHit groundHit;

        Vector3 rayStartPos = this.transform.position + new Vector3(0,1f,0);

        if(Physics.Raycast(rayStartPos, -transform.up, out groundHit, 1.5f, this.instaciaPlayer.GroundLayerMask)){
            return true;            
        }        
        return false;
    }
}
