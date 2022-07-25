using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swim : MonoBehaviour
{
    public Player_Scp instaciaPlayer;

    private void Update() {
        CanSwimCorner();
    }

    public void CanSwimCorner(){

        if(instaciaPlayer.CanSwim() && instaciaPlayer.IsinWater){
            if(instaciaPlayer.IsSwiming == false){
                StartSwim();
            }
        }else if(instaciaPlayer.IsSwiming == true) StopSwim();
    }

    private void OnTriggerStay(Collider collision) {
        if(collision.gameObject.CompareTag("Water"))
        {
            instaciaPlayer.IsinWater = true;
            if(!instaciaPlayer.PrefebAnimScp.IsSwiming && !instaciaPlayer.PrefebAnimScp.IsInCorner
                && !instaciaPlayer.PrefebAnimScp.IsClimbing)
            {
                this.instaciaPlayer.PrefebAnimScp.ResetAllAnimations();
                this.instaciaPlayer.PrefebAnimScp.IsSwiming = true;
            }
        }
    }

    private void OnTriggerExit(Collider collision) {
        if(collision.gameObject.CompareTag("Water"))
        {
            instaciaPlayer.IsinWater = false;
            this.instaciaPlayer.PrefebAnimScp.IsSwiming = false;
        }
    }

    public void StartSwim(){
        instaciaPlayer.IsSwiming = true;
        instaciaPlayer.Rb.useGravity = false;
        instaciaPlayer.Rb.velocity = new Vector3(0, instaciaPlayer.Rb.velocity.y /3 , 0);
    }

    public void StopSwim(){
        instaciaPlayer.IsSwiming = false;
        instaciaPlayer.Rb.velocity = new Vector3(0, instaciaPlayer.Rb.velocity.y / 3, 0);
        instaciaPlayer.Rb.useGravity = true;
    }
}
