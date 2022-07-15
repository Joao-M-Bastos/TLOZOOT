using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManegement : MonoBehaviour
{
    public Player_Scp instaciaPlayer;

    public float velocidadecamera;
    public float velocidaderotacaocamera;
    public Vector3 cameraOffset;


    // Update is called once per frame
    public void CameraUpdate()
    {
        var pos = this.instaciaPlayer.transform.position - this.transform.forward * cameraOffset.z + this.transform.up * cameraOffset.y + this.transform.right * cameraOffset.x;
        this.transform.position = Vector3.Lerp(this.transform.position, pos, velocidadecamera * Time.deltaTime);

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.instaciaPlayer.PlayerRotation, velocidaderotacaocamera * Time.deltaTime);       
    }

}
