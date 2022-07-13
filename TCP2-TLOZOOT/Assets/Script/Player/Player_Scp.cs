using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Scp : MonoBehaviour
{
    

    //Instacias
    public static Player_Scp instaciaPlayer;
    public Animator prefebAnim;
    private Animator parentAnim;
    public Camera maincamera;
    private Rigidbody rb;
    private Quaternion playerRotation;
    public Combat playerCombat;
    private LockOn lockOn;

    //Camera
    public float velocidadecamera;
    public float velocidaderotacaocamera;
    public Vector3 CameraOffset;
    private bool isLocked;

    //Walk
    private float speed;
    private Vector3 playerForword;
    private Vector3 playerRight;


    //Climb
    private bool isClimb, isInWall, isInCorner, isInCornerAnimation;
    private RaycastHit wallHit;
    public LayerMask climbLayerMask;

    //Jump
    private bool isInGround;
    public LayerMask groundLayerMask;

    //Attack
    private bool hasAttacked;
    public bool hasSword, hasShild;

    //SceneLoader
    GameObject[] StartingPositionObj;
    private int startingPosCode;

    private void Awake()
    {
        this.ParentAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerCombat = this.gameObject.GetComponent<Combat>();
        lockOn = this.gameObject.GetComponent<LockOn>();
        this.IsInCorner = false;
    }

    private void OnLevelWasLoaded(int i){
        StartingPositionObj = GameObject.FindGameObjectsWithTag("StartingPosition");

        foreach (GameObject obj in StartingPositionObj)
        {
            if(obj.GetComponent<StartingCode>() != null && obj.GetComponent<StartingCode>().startingCode == startingPosCode){
                this.transform.position = obj.transform.position;
            }
        }

    }

    // Update is called once per frame
    private void Update()
    {
        if(this.Rb.useGravity == true){
            //Almenta força da gravidade
            this.Rb.AddForce(0, -10, 0);

        }

        //Liga desliga LockOn
        if(Input.GetKeyDown(KeyCode.LeftControl) && isGrounded()){
            IsLocked = !IsLocked;
        }

        //Trava a visão no alvo
        if(isLocked){
            this.transform.LookAt(lockOn.LockOnTarget());
            PlayerRotation = new Quaternion(0,transform.rotation.y,0,transform.rotation.w);
        }
        //Rotaciona a Camera
        maincamera.transform.rotation = Quaternion.Lerp(maincamera.transform.rotation, PlayerRotation, velocidaderotacaocamera * Time.deltaTime);
    }

    private void LateUpdate()
    {
        //Move a camera
        var pos = transform.position - maincamera.transform.forward * CameraOffset.z + maincamera.transform.up * CameraOffset.y + maincamera.transform.right * CameraOffset.x;
        maincamera.transform.position = Vector3.Lerp(maincamera.transform.position, pos, velocidadecamera * Time.deltaTime);
        
        
        //Rotaciona o jogador caso algo no codigo altere a rotação
        transform.rotation = PlayerRotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        //Verifica se o jogador pode escalar
        if(collision.gameObject.tag == "Climb"){
            isInWall = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        //Verifica se o jogador esta encostando na parede
        if(collision.gameObject.tag == "Climb"){
            //Se ele esta encostando na parede ele volta um pouco para evitar bugs de collider
            transform.Translate(0,0, -0.1f);            
        }

        //Verifica se esta no chao
        if(collision.gameObject.tag == "Ground"){
            isInGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        //Se sair da parede ou chão ele anota
        if(collision.gameObject.tag == "Climb"){
            isInWall = false;
        }
        if(collision.gameObject.tag == "Ground"){
            isInGround = false;
        }
    }
    
    public bool canClimb(){
        //Verifica se há uma parede escalavel a frente

        RaycastHit climbRayValue;

        Vector3 rayStartPos = this.transform.position + new Vector3(0,0.4f,0);

        if(Physics.Raycast(rayStartPos, PlayerForword, out wallHit, 1.2f, climbLayerMask) || isInWall){
            return true;
        }
        
        return false;
    }    

    public bool canWalk(){
        if(!playerCombat.isVulnerable || IsInCornerAnimation || this.HasAttacked){
            return false;
        }
        
        return true;
    }

    public bool isGrounded(){
        RaycastHit groundHit;

        Vector3 rayStartPos = this.transform.position + new Vector3(0,1f,0);

        if(Physics.Raycast(rayStartPos, -transform.up, out groundHit, 1.4f, groundLayerMask) && isInGround){
            return true;            
        }        
        return false;
    }

    public void StopClimbCorner(){
        this.IsInCorner = false;
        this.Rb.isKinematic = false;
    }

    


    //GETS AND SETS -----------------------------//
    public Quaternion PlayerRotation{
        set{this.playerRotation = value;}
        get{return this.playerRotation;}
    }

    public Vector3 PlayerForword{
        set{this.playerForword = value;}
        get{return this.playerForword;}
    }    
    
    public Vector3 PlayerRight{
        set{this.playerRight = value;}
        get{return this.playerRight;}
    }

    public int StartingPosCode{
        set{this.startingPosCode = value;}
        get{return this.startingPosCode;}
    }

    public bool IsClimb{
        set{this.isClimb = value;}
        get{return this.isClimb;}
    }

    public bool HasAttacked{
        set{this.hasAttacked = value;}
        get{return this.hasAttacked;}
    }

    public bool HasShild{
        set{this.hasShild = value;}
        get{return this.hasShild;}
    }

    public bool HasSword{
        set{this.hasSword = value;}
        get{return this.hasSword;}
    }

    public bool IsLocked{
        set{this.isLocked = value;}
        get{return this.isLocked;}
    }

    public bool IsInCorner{
        set{this.isInCorner = value;}
        get{return this.isInCorner;}
    }

        public bool IsInCornerAnimation{
        set{this.isInCornerAnimation = value;}
        get{return this.isInCornerAnimation;}
    }

    public float Speed{
        set{this.speed = value;}
        get{return this.speed;}
    }

    public Animator PrefebAnim{
        set{this.prefebAnim = value;}
        get{return this.prefebAnim;}
    }
    
    public Animator ParentAnim{
        set{this.parentAnim = value;}
        get{return this.parentAnim;}
    }

    public Rigidbody Rb{
        set{this.rb = value;}
        get{return this.rb;}
    }

    public RaycastHit WallHit{
        set{this.wallHit = value;}
        get{return this.wallHit;}
    }

    public LayerMask ClimbLayerMask{
        set{this.climbLayerMask = value;}
        get{return this.climbLayerMask;}
    }

    public LayerMask GroundLayerMask{
        set{this.groundLayerMask = value;}
        get{return this.groundLayerMask;}
    }
}
