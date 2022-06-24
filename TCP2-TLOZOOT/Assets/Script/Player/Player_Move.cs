using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    //Instacias
    public Animator anim;
    public Camera maincamera;
    private Rigidbody rb;
    public Quaternion playerRotation;
    private Combat playerCombat;
    private LockOn lockOn;

    //Camera
    public float velocidadecamera;
    public float velocidaderotacaocamera;
    public Vector3 CameraOffset;
    public bool isLocked;

    //Walk
    public float speed;
    public Vector3 playerForword;
    private static float baseSpeed;
    float InputX, InputZ;

    //Climb
    public float speedClimb;
    
    public bool isClimb, isInWall;
    RaycastHit wallHit;
    public LayerMask climbLayerMask;

    //Jump
    public float jumpforce;
    public bool isInGround;
    private Vector3 jump, jumpC;
    public LayerMask groundLayerMask;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        baseSpeed = speed;
        playerCombat = this.gameObject.GetComponent<Combat>();
        lockOn = this.gameObject.GetComponent<LockOn>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetKeyDown(KeyCode.LeftControl) && isGround()){
            isLocked = !isLocked;
        }

        if(isLocked) this.transform.LookAt(lockOn.LockOnTarget());

        playerRotation = new Quaternion(0,transform.rotation.y,0,transform.rotation.w);
        
        playerForword = this.transform.TransformDirection(Vector3.forward);      

        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        if(InputX != 0 || InputZ != 0)
        {
            if(canClimb()){
                if(!isClimb) StartClimb();
            }else{
                if(isClimb) StopClimb();
            }

            if(isClimb) Climb();
            else if(playerCombat.isVulnerable) Walk();

        }else{
            ResetSpeed();
            anim.SetBool("Walk", false);
        }

        if(Input.GetKeyDown(KeyCode.Space)) Jump();

        

        maincamera.transform.rotation = Quaternion.Lerp(maincamera.transform.rotation, playerRotation, velocidaderotacaocamera * Time.deltaTime);
    }

    private void LateUpdate()
    {
        var pos = transform.position - maincamera.transform.forward * CameraOffset.z + maincamera.transform.up * CameraOffset.y + maincamera.transform.right * CameraOffset.x;
        maincamera.transform.position = Vector3.Lerp(maincamera.transform.position, pos, velocidadecamera * Time.deltaTime);
        transform.rotation = playerRotation;
    }


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Climb"){
            isInWall = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Climb"){
            transform.Translate(0,0, -0.1f);            
        }
        if(collision.gameObject.tag == "Ground"){
            isInGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Climb"){
            isInWall = false;
        }
        if(collision.gameObject.tag == "Ground"){
            isInGround = false;
        }
    }

    void StartClimb(){        
        isClimb = true;
        isLocked = false;
        rb.useGravity =false;
        anim.SetBool("StartClimb", true );
    }

    void Climb(){
        Vector3 direcaoClimb;

        playerRotation = Quaternion.LookRotation(-wallHit.normal);

        direcaoClimb = new Vector3(InputX, -InputZ, 0);
        this.rb.velocity = direcaoClimb * -speedClimb;
    }

    void StopClimb(){
        isClimb = false;    
        rb.useGravity = true;
        anim.SetBool("StopClimb", true);
    }

    bool canClimb(){

        float wallLookAngle;
        Vector3 rayStartPos = this.transform.position + new Vector3(0,0.3f,0);

        if(Physics.Raycast(rayStartPos, transform.TransformDirection(Vector3.forward), out wallHit, 1.4f, climbLayerMask) || isInWall){
            return true;
        }
        
        return false;
    }



    void Walk(){
        Vector3 velocityWalk;

        var direcao = new Vector3(InputX, 0, InputZ);

        velocityWalk = playerForword * speed;
        velocityWalk.y = rb.velocity.y;
        
        Run();

        if(!isLocked){
            var camerarot = this.transform.rotation;
            camerarot.x = 0;
            camerarot.z = 0;

            anim.SetBool("Walk", true );
            this.rb.velocity = velocityWalk;
            playerRotation = Quaternion.Lerp(playerRotation, Quaternion.LookRotation(direcao)*camerarot, 3 * Time.deltaTime);
        }else{
            transform.Translate(direcao * speed * Time.deltaTime);
        }
    }

    void Jump(){
        if(!isClimb){
            if (isGround())
            {
                rb.isKinematic = false;
                rb.AddForce(transform.up * jumpforce, ForceMode.Impulse);
            }
        }else{
            if(!isGround()){
                rb.isKinematic = false;               
                transform.Translate(0,0, jumpforce * -0.1f);
                StopClimb();
            }            
        }   
    }

    public bool isGround(){
        RaycastHit groundHit;

        Vector3 rayStartPos = this.transform.position + new Vector3(0,1f,0);

        if(Physics.Raycast(rayStartPos, -transform.up, out groundHit, 1f, groundLayerMask) && isInGround){
            return true;            
        }        
        return false;
    }

    void ResetSpeed(){
        Vector3 velocityWalk;

        velocityWalk = playerForword * Time.deltaTime;
        velocityWalk.y = rb.velocity.y;

        this.rb.velocity = velocityWalk;
    }

    void Run(){        
        if(Input.GetKey(KeyCode.LeftShift)) {speed = baseSpeed * 1.3f; anim.SetBool("Run", true);}
        else {speed = baseSpeed; anim.SetBool("Run", false);}
    }
}
