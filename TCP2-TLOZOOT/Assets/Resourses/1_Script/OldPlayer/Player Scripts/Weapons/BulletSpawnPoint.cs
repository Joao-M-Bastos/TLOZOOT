using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnPoint : MonoBehaviour
{
    private LockOn lockOn;
    public Player_Scp instanciaPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        this.lockOn = GetComponent<LockOn>();
        this.lockOn.ObjTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(lockOn.LockOnTarget(true) != this.transform)
        {
            this.instanciaPlayer.CanShoot = true;
            this.transform.LookAt(lockOn.LockOnTarget(true));
        }
        else
        {
            this.instanciaPlayer.CanShoot = false;
            
        }
        
    }
}
