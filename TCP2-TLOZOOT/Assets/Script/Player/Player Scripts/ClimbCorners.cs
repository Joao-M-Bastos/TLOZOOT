using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbCorners : MonoBehaviour
{
    public Player_Scp instaciaPlayer;

    private RaycastHit rayFrontal;
    private RaycastHit rayAcima;
    private RaycastHit rayAcimaFrontal;

    private void Update() {
        //if(instaciaPlayer.IsInCorner){
            CanClimbCorner();
        //}
    }

    public void CanClimbCorner(){
        if(!RayAcimaFrontal()){
            if(RayFrontal() && RayAcima()){
                GetInCorner();
            }
        }
    }

    public bool RayFrontal(){
        //Verifica se há uma parede escalavel a frente
        Vector3 rayStartPos = this.transform.position + new Vector3(0, 2f, 0);

        //Debug.DrawRay(rayStartPos, instaciaPlayer.PlayerForword * 1.4f, Color.green);

        if(Physics.Raycast(rayStartPos, instaciaPlayer.PlayerForword, out rayFrontal, 1.2f, instaciaPlayer.ClimbLayerMask)
         || Physics.Raycast(rayStartPos, instaciaPlayer.PlayerForword, out rayFrontal, 1.2f, instaciaPlayer.GroundLayerMask)){
            return true;
        }
        return false;
    }

    public bool RayAcima(){
        
        //Verifica se há uma parede escalavel a frente
        Vector3 rayStartPos = this.transform.position + instaciaPlayer.PlayerForword * 1.5f + new Vector3(0f, 5f, 0f);

        //Debug.DrawRay(rayStartPos, -this.transform.up * 2.4f, Color.green);

        if(Physics.Raycast(rayStartPos, -this.transform.up, out rayAcima, 2.4f, instaciaPlayer.ClimbLayerMask)
        || Physics.Raycast(rayStartPos, -this.transform.up, out rayAcima, 2.4f, instaciaPlayer.GroundLayerMask)){
            return true;
        }
        return false;
    }

    public bool RayAcimaFrontal(){
        //Verifica se há uma parede escalavel a frente
        Vector3 rayStartPos = this.transform.position  + new Vector3(0f, 5.25f, 0f);

        //Debug.DrawRay(rayStartPos, instaciaPlayer.PlayerForword * 2.4f, Color.green);

        if(Physics.Raycast(rayStartPos, instaciaPlayer.PlayerForword, out rayAcimaFrontal, 2.4f, instaciaPlayer.ClimbLayerMask)
        || Physics.Raycast(rayStartPos, instaciaPlayer.PlayerForword, out rayAcimaFrontal, 2.4f, instaciaPlayer.GroundLayerMask)){
            return true;
        }
        return false;
    }

    public void GetInCorner(){
        if(this.instaciaPlayer.IsInCorner == false){
            this.instaciaPlayer.IsClimb = false;
            this.instaciaPlayer.IsLocked = false;
            this.instaciaPlayer.Rb.isKinematic = true;
            this.instaciaPlayer.PlayerRotation = Quaternion.LookRotation(-this.rayFrontal.normal);
            this.instaciaPlayer.IsInCorner = true;

            Vector3 playerPos;
            playerPos = this.instaciaPlayer.Rb.transform.position;
                        
            playerPos.y = this.rayAcima.point.y - 3;

            this.instaciaPlayer.Rb.transform.position = playerPos;
        }
        
    }
}
