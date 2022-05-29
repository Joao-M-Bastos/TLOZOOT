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
    private Combat playerEntityCombat;


    //Camera
    public float velocidadecamera;
    public float velocidaderotacaocamera;
    public Vector3 CameraOffset;

    //Walk
    public float speed;

    private bool canWalk;

    private static float baseSpeed;

    float InputX, InputZ;

    //Climb
    public float speedClimb;
    
    public bool isClimb, isInWall;
    RaycastHit wallHit;
    public LayerMask climbLayerMask;

    //Jump
    public float jumpforce;
    private Vector3 jump, jumpC;
    public LayerMask groundLayerMask;




    // Start is called before the first frame update
    void Start()
    {
       

    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        baseSpeed = speed;
        playerEntityCombat = this.gameObject.GetComponent<Combat>();
    }

    // Update is called once per frame
    void Update()
    {
        RandomUpdate();
        playerRotation = new Quaternion(0,transform.rotation.y,0,transform.rotation.w);

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
            else if(playerEntityCombat.isVulnerable) Walk();

        }else{
            anim.SetBool("Walk", false);
        }

        if(Input.GetKeyDown(KeyCode.Space)) Jump();

        maincamera.transform.rotation = Quaternion.Lerp(maincamera.transform.rotation, playerRotation, velocidaderotacaocamera * Time.deltaTime);
    }

    private void RandomUpdate(){
        if(Random.Range(0f, 60.0f) > 59){
            isGround();
        }
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
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Climb"){
            isInWall = false;
        }
    }

    void StartClimb(){        
        isClimb = true;
        rb.useGravity =false;        
    }

    void Climb(){
        Vector3 direcaoClimb;

        rb.isKinematic = true;
        rb.isKinematic = false;

        playerRotation = Quaternion.LookRotation(-wallHit.normal);

        direcaoClimb = new Vector2(InputX, InputZ);
        transform.Translate(direcaoClimb * speedClimb * Time.deltaTime);
    }

    void StopClimb(){
        isClimb = false;    
        rb.useGravity = true;    
    }

    bool canClimb(){

        float wallLookAngle;
        Vector3 rayStartPos = this.transform.position + new Vector3(0,0.3f,0);

        if(Physics.Raycast(rayStartPos, transform.TransformDirection(Vector3.forward), out wallHit, 0.5f, climbLayerMask) || isInWall){
            return true;
        }
        
        return false;
    }



    void Walk(){
        Vector3 direcao;

        direcao = new Vector3(InputX, 0, InputZ);
        
        var camerarot = maincamera.transform.rotation;
        camerarot.x = 0;
        camerarot.z = 0;

        anim.SetBool( "Walk", true );

        Run();

        transform.Translate(0, 0, speed * Time.deltaTime);
        playerRotation = Quaternion.Lerp(playerRotation, Quaternion.LookRotation(direcao)*camerarot, 8 * Time.deltaTime);
    }

    void Jump(){
        if(!isClimb){
            if (isGround())
            {
                rb.isKinematic = false;
                rb.AddForce(transform.up * jumpforce, ForceMode.Impulse);
                //anim.SetBool("Jump", true);
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

        if(Physics.Raycast(transform.position, -transform.up, out groundHit, 0.1f, groundLayerMask)){
            rb.isKinematic = true;
            return true;            
        }
        return false;
    }

    void Run(){        
        if(Input.GetKey(KeyCode.LeftShift)) {speed = baseSpeed * 2; anim.SetBool("Run", true);}
        else {speed = baseSpeed; anim.SetBool("Run", false);}
    }
}
