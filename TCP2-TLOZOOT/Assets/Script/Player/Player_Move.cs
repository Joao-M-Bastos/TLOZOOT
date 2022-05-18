using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour
{
    //Instacias
    public Animator anim;
    public Camera maincamera;
    private Rigidbody rb;

    //Camera
    public float velocidadecamera;
    public float velocidaderotacaocamera;
    public Vector3 CameraOffset;

    //Move
    public float speed, speedClimb;

    private static float baseSpeed;
    public bool isClimb;

    float InputX, InputZ;
    Vector3 direcao, direcaoClimb;

    //Jump
    public float jumph;
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
        jump = new Vector3(0f, jumph, 0f);
        jumpC = new Vector3(0f, 0f, -jumph);
        baseSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {  
        transform.rotation = new Quaternion(0,transform.rotation.y,0,transform.rotation.w);

        InputX = Input.GetAxis("Horizontal");
        InputZ = Input.GetAxis("Vertical");

        if(InputX != 0 || InputZ != 0)
        {
            if(isClimb) Climb();
            else Walk();

        }else anim.SetBool("Walk", false);


        if(Input.GetKeyDown(KeyCode.Space)) Jump();
        
        maincamera.transform.rotation = Quaternion.Lerp(maincamera.transform.rotation, transform.rotation, velocidaderotacaocamera * Time.deltaTime);
        
        
    }

        private void LateUpdate()
    {
        var pos = transform.position - maincamera.transform.forward * CameraOffset.z + maincamera.transform.up * CameraOffset.y + maincamera.transform.right * CameraOffset.x;
        maincamera.transform.position = Vector3.Lerp(maincamera.transform.position, pos, velocidadecamera    * Time.deltaTime);
    }



    

    private void OnCollisionStay(Collision collision)
    {
            if (collision.gameObject.tag == "Ground"){
                isGround = true;
                //anim.SetBool("Jump", false);
            }   

        if(collision.gameObject.tag == "Climb"){
            isClimb = true;
            rb.useGravity = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Climb"){
            
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground"){
            isGround = false;
            //anim.SetBool("Jump", false);
        }   

        if(collision.gameObject.tag == "Climb"){
            isClimb = false;
            rb.useGravity = true;
        }
    }


    void Climb(){

        direcaoClimb = new Vector2(InputX, InputZ);

        transform.rotation = new Quaternion(0,0,0,transform.rotation.w);

        rb.velocity = transform.TransformDirection(direcaoClimb) * speedClimb;
    }



    void Walk(){
        direcao = new Vector3(InputX, 0, InputZ);
        
        var camerarot = maincamera.transform.rotation;
        camerarot.x = 0;
        camerarot.z = 0;

        anim.SetBool( "Walk", true ); 

        Run();

        transform.Translate(0, 0, speed * Time.deltaTime);            
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direcao)*camerarot, 5 * Time.deltaTime);

        
    }

    void Jump(){
        if(!isClimb){
            if (isGround)
            {
                rb.AddForce(jump * jumpforce, ForceMode.Impulse);
                //anim.SetBool("Jump", true);
            }
        }else{           
            rb.AddForce(jumpC * jumpforce, ForceMode.Impulse);
            GetComponent<Rigidbody>().useGravity = true;
        }   
    }

    void Run(){        
        if(Input.GetKey(KeyCode.LeftShift)) {speed = baseSpeed * 2; anim.SetBool("Run", true);}
        else {speed = baseSpeed; anim.SetBool("Run", false);}
    }
}
