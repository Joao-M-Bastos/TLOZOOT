using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManegement : MonoBehaviour
{
    public Player_Scp instaciaPlayer;

    private Vector3 lockOnAnimTrasnf;
    public Transform lockOnAnimTrasnfBase;

    public MeshRenderer slingshotMesh;
    public MeshRenderer slingshotGomaMesh;
    public MeshRenderer lockOnMesh;
    public SkinnedMeshRenderer LinkMesh;

    public float velocidadecamera;
    public float velocidaderotacaocamera;
    private Vector3 cPosition;
    
    private Vector3 pPosition;
    private Vector3 cameraOffset;
    public Vector3 baseCameraOffset;
    private LockOn lockOn;

    RaycastHit hit;

    private void Awake() {
        this.lockOn = GetComponent<LockOn>();
        this.lockOn.ObjTransform = this.instaciaPlayer.transform;
        ObjRender(false);
        cameraOffset = baseCameraOffset;
        lockOnAnimTrasnf = lockOnAnimTrasnfBase.position;
    }

    private void CheckWalls(Vector3 camPos){
        if (Physics.Linecast(this.pPosition, camPos, out hit, instaciaPlayer.ClimbLayerMask)
        ||  Physics.Linecast(this.pPosition, camPos, out hit, instaciaPlayer.GroundLayerMask))
        {
            this.CPosition = hit.point + transform.forward * 0.3f + transform.up * -0.1f;
        }
    }

    //Chamado por outras classes para objeter o Timing certinho para atualizar
    public void CameraUpdate()
    {        
        //this.CPosition = this.transform.position;
        this.pPosition = this.instaciaPlayer.transform.position + new Vector3(0,2,0);

        CPosition = this.pPosition - this.instaciaPlayer.transform.forward * cameraOffset.z + this.instaciaPlayer.transform.up * cameraOffset.y + this.instaciaPlayer.transform.right * cameraOffset.x;

        CheckWalls(CPosition);

        if (CheckFP())
        {
            this.CPosition = pPosition + transform.forward * 0.85f + transform.up * 1.2f;            
        }

        ObjRender(CheckFP());

        this.transform.position = Vector3.Lerp(this.transform.position, CPosition, velocidadecamera * Time.deltaTime);


        CreateLook();
    }

    public bool CheckFP()
    {
        if (this.instaciaPlayer.IsAiming || this.instaciaPlayer.DistanceFromPlayer(hit.point) < 5f)
        {
            return true;
        }
        return false;
    }

    public void CreateLook(){
        //Liga desliga LockOn
        if(Input.GetKeyDown(KeyCode.LeftControl) && this.instaciaPlayer.IsGrounded()){
            this.instaciaPlayer.IsLocked = !this.instaciaPlayer.IsLocked;
            this.instaciaPlayer.ResetSpeed();
        }

        //Trava a visão no alvo
        if (this.instaciaPlayer.IsLocked || this.instaciaPlayer.IsAiming) {
            this.instaciaPlayer.transform.LookAt(lockOn.LockOnTarget(false));
            this.instaciaPlayer.PlayerRotation = new Quaternion(0, this.instaciaPlayer.transform.rotation.y, 0, this.instaciaPlayer.transform.rotation.w);
            LockOnAnimMesh(true);
        } else LockOnAnimMesh(false);


        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.instaciaPlayer.PlayerRotation, velocidaderotacaocamera * Time.deltaTime);

        if (this.instaciaPlayer.IsAiming && lockOn.LockOnTarget(true) != this.instaciaPlayer.transform)
        {
            this.transform.LookAt(lockOn.LockOnTarget(true));
        }

        
    }

    public Vector3 CPosition{
        set{this.cPosition = value;}
        get{return this.cPosition;}
    }

    public void LockOnAnimMesh(bool canAppear)
    {
        if(lockOn.LockOnTarget(true) != this.instaciaPlayer.transform && canAppear)
        {
            lockOnMesh.enabled = true;
        }else lockOnMesh.enabled = false;
    }

    public void ObjRender(bool valor)
    {
        if (this.instaciaPlayer.IsAiming)
        {
            slingshotMesh.enabled = true;
            slingshotGomaMesh.enabled = true;
        }
        else
        {
            slingshotMesh.enabled = false;
            slingshotGomaMesh.enabled = false;
        }

        LinkMesh.enabled = !valor;
    }

    public void FixLockOnAnimTranf(int i)
    {
        lockOnAnimTrasnf = lockOnAnimTrasnfBase.position + new Vector3(0, 0.5f * i, 0);
    }

}
