using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Scp : MonoBehaviour
{
    //Instacias
    public Animator prefebAnim;
    private Animator parentAnim;
    private CameraManegement cameraManegement;
    private Rigidbody rb;
    private Quaternion playerRotation;
    private Combat playerCombat;
    public SkinnedMeshRenderer meshRenderer;

    //Shoot
    public bool isAiming;

    //Camera
    private bool isLocked;

    //Walk
    private float speed;
    private Vector3 playerForword;
    private Vector3 playerRight;

    //Swim
    private bool isSwiming;
    public LayerMask waterLayerMask;
    private bool isinWater;
    private RaycastHit swimHit;

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
        cameraManegement = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManegement>();
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
        if(this.Rb.useGravity == true && !IsGrounded()){
            //Almenta força da gravidade
            this.Rb.AddForce(0, -9, 0);
        }
        else if(IsSwiming)
        {
            this.Rb.AddForce(0, 4, 0);
        }
    }

    private void LateUpdate()
    {
        this.cameraManegement.CameraUpdate();
        //Rotaciona o jogador caso algo no codigo altere a rotação
        transform.rotation = PlayerRotation;
    }

    void OnCollisionEnter(Collision collision)
    {
        //Verifica se o jogador pode escalar
        if(collision.gameObject.CompareTag("Climb"))
        {
            isInWall = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        //Verifica se o jogador esta encostando na parede
        if(collision.gameObject.CompareTag("Climb"))
        {
            //Se ele esta encostando na parede ele volta um pouco para evitar bugs de collider
            transform.Translate(0,0, -0.1f);            
        }

        //Verifica se esta no chao
        if(collision.gameObject.CompareTag("Ground"))
        {
            isInGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        //Se sair da parede ou chão ele anota
        if(collision.gameObject.CompareTag("Climb"))
        {
            isInWall = false;
        }
        if(collision.gameObject.CompareTag("Ground"))
        {
            isInGround = false;
        }
    }
    
    public bool CanClimb(){
        //Verifica se há uma parede escalavel a frente

        Vector3 rayStartPos = this.transform.position + new Vector3(0,0.4f,0);

        if(Physics.Raycast(rayStartPos, PlayerForword, out wallHit, 1.2f, climbLayerMask) || isInWall){
            return true;
        }
        
        return false;
    }    

    public bool CanWalk(){
        if(!PlayerCombat.isVulnerable || IsInCornerAnimation){
            this.IsLocked = false;
            return false;
        }else if(this.HasAttacked){
            return false;
        }

        return true;
    }

    public bool CanTurn(){
        if(IsLocked){
            this.IsLocked = false;
            return false;
        }

        return true;
    }

    public bool IsGrounded(){

        Vector3 rayStartPos = this.transform.position + new Vector3(0, 1f, 0);
        if (Physics.Raycast(rayStartPos, -transform.up, out _, 1.4f, groundLayerMask) && isInGround){
            return true;
        }
        return false;
    }

    public bool CanSwim(){

        Vector3 rayStartPos = this.transform.position + new Vector3(0, 12f, 0);

        Debug.DrawRay(rayStartPos, -this.transform.up * 10f, Color.red);
        if (Physics.Raycast(rayStartPos, -this.transform.up, out swimHit, 10f, this.waterLayerMask)){
            return true;
        }
        return false;
    }

    public void StopClimbCorner(){
        this.transform.Translate(new Vector3(0, 4, 1.5f));
        this.IsInCorner = false;
        this.Rb.isKinematic = false;
    }

    public void ResetSpeed(){
        Vector3 velocityWalk;

        velocityWalk = PlayerForword * Time.deltaTime;
        velocityWalk.y = Rb.velocity.y;

        if(Rb.velocity.x < 0.01f || IsClimb){
            velocityWalk = new Vector3(0,velocityWalk.y,0);
        }

        Rb.velocity = velocityWalk;
    }

    public float DistanceFromPlayer(Vector3 objTransform){
        return Vector3.Distance(objTransform, this.transform.position);
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

    public bool IsAiming
    {
        set { this.isAiming = value; }
        get { return this.isAiming; }
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

    public bool IsinWater{
        set{this.isinWater = value;}
        get{return this.isinWater;}
    }
    
    public bool IsSwiming{
        set{this.isSwiming = value;}
        get{return this.isSwiming;}
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

    public RaycastHit SwimHit
    {
        set { this.swimHit = value; }
        get { return this.swimHit; }
    }

    public LayerMask ClimbLayerMask{
        set{this.climbLayerMask = value;}
        get{return this.climbLayerMask;}
    }

    public LayerMask GroundLayerMask{
        set{this.groundLayerMask = value;}
        get{return this.groundLayerMask;}
    }
    
    public LayerMask WaterLayerMask{
        set{this.waterLayerMask = value;}
        get{return this.waterLayerMask;}
    }
    
    public Combat PlayerCombat{
        set{this.playerCombat = value;}
        get{return this.playerCombat;}
    }
}
