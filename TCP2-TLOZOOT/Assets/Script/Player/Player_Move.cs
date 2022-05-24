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

    RaycastHit wallHit;

    //Camera
    public float velocidadecamera;
    public float velocidaderotacaocamera;
    public Vector3 CameraOffset;

    //Walk
    public float speed;

    private static float baseSpeed;

    float InputX, InputZ;

    //Climb
    public float speedClimb;
    
    public bool isClimb, isInWall;

    //Jump
    public bool isGround;
    public float jumpforce;
    private Vector3 jump, jumpC;





    // Start is called before the first frame update
    void Start()
    {
       

    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        baseSpeed = speed;
        
    }

    // Update is called once per frame
    void Update()
    {
        playerRotation = new Quaternion(0,transform.rotation.y,0,transform.rotation.w);

        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        if(InputX != 0 || InputZ != 0)
        {
            if(canClimbing()){
                if(!isClimb) StartClimb();
            }else{
                if(isClimb) StopClimb();
            }

            if(isClimb) Climb();
            else Walk();

        }else{
            if(isClimb) rb.velocity = new Vector3(0,0,0);
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

    void OnCollisionStay(Collision other)
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground"){
            isGround = true;
            //anim.SetBool("Jump", false);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground"){
            isGround = false;
            //anim.SetBool("Jump", false);
        }
    }

    void StartClimb(){
        isClimb = true;
        rb.useGravity = false;
        playerRotation = Quaternion.LookRotation(-wallHit.normal);
    }

    void Climb(){
        Vector3 direcaoClimb;

        direcaoClimb = new Vector2(InputX, InputZ);
        rb.velocity = transform.TransformDirection(direcaoClimb) * speedClimb;
    }

    void StopClimb(){
        rb.velocity = new Vector3(0,0,0);
        isClimb = false;
        rb.useGravity = true;
    }

    bool canClimbing(){

        float wallLookAngle;        

        if(Physics.Raycast(transform.position + new Vector3(0,-0.1f,0), transform.forward, out wallHit, 0.45f)){
            wallLookAngle = Vector3.Angle(transform.forward, -wallHit.normal);
            if(wallLookAngle < 65){
                return true;
            }
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
            if (isGround)
            {
                rb.AddForce(transform.up * jumpforce, ForceMode.Impulse);
                //anim.SetBool("Jump", true);
            }
        }else{
            if(playerRotation.y > -80 && playerRotation.y < -100 || playerRotation.y > 80 && playerRotation.y < 100){
                transform.Translate(transform.TransformDirection(wallHit.normal) * jumpforce * -0.1f);
            }else transform.Translate(transform.TransformDirection(wallHit.normal) * jumpforce * 0.1f);
            StopClimb();
        }   
    }

    void Run(){        
        if(Input.GetKey(KeyCode.LeftShift)) {speed = baseSpeed * 2; anim.SetBool("Run", true);}
        else {speed = baseSpeed; anim.SetBool("Run", false);}
    }
}
