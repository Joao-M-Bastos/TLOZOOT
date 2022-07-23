using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    private GameObject[] targets;
    private GameObject[] EnemyTargets;
    private GameObject closest;
    private Transform pTransform;
    private float closestDistance;
    float distanceFromPlayer;
    Vector3 pPosition;

    public Transform PlayerTransform{
        set{this.pTransform = value;}
        get{return this.pTransform;}
    }

    public Transform LockOnTarget(){
        GameObject obj = FindClosestEnemy();
        if(closestDistance < 20){
            return obj.transform;
        }else return pTransform;
    }

    public GameObject FindClosestEnemy(){
        closestDistance = Mathf.Infinity;
        closest = null;
        pPosition = this.pTransform.position;

        targets = GameObject.FindGameObjectsWithTag("LockOnTarget");

        EnemyTargets = GameObject.FindGameObjectsWithTag("Enemy");


        foreach (GameObject target in targets)
        {
            distanceFromPlayer = Vector3.Distance(target.transform.position, pPosition); 
            
            if ((distanceFromPlayer + 3) < closestDistance)
            {
                closest = target;
                closestDistance = distanceFromPlayer;
            }
        }

        foreach (GameObject target in EnemyTargets)
        {
            distanceFromPlayer = Vector3.Distance(target.transform.position, pPosition);

            if ((distanceFromPlayer + 3) < closestDistance)
            {
                closest = target;
                closestDistance = distanceFromPlayer;
            }
        }
        return closest;
    }
}
