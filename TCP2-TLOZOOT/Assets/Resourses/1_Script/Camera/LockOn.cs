using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    private GameObject[] targets;
    private GameObject closest;
    private Transform objTransform;
    private float closestDistance;
    float distanceFromPlayer;
    Vector3 pPosition;

    public Transform ObjTransform{
        set{this.objTransform = value;}
        get{return this.objTransform; }
    }

    public Transform LockOnTarget(bool targetType){

        GameObject obj = FindClosestEnemy(true);
        if(closestDistance < 20){
            return obj.transform;
        }else return ObjTransform;
    }

    public GameObject FindClosestEnemy(bool isEnemyTarget)
    {
        closestDistance = Mathf.Infinity;
        closest = null;

        if (isEnemyTarget)
        {
            targets = GameObject.FindGameObjectsWithTag("Enemy");
        }else targets = GameObject.FindGameObjectsWithTag("LockOnTarget");

        


        foreach (GameObject target in targets)
        {
            distanceFromPlayer = Vector3.Distance(target.transform.position, this.ObjTransform.position); 
            
            if (distanceFromPlayer < closestDistance)
            {
                closest = target;
                closestDistance = distanceFromPlayer;
            }
         }

        return closest;
    }
}
