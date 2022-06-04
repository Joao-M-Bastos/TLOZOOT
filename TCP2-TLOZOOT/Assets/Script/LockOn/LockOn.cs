using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
    private GameObject[] targets;
    private GameObject closest;
    private float distance;
    float distanceFromPlayer;
    Vector3 position;

    public Transform LockOnTarget(){
        GameObject obj = FindClosestEnemy();
        if(distance < 10){
            return obj.transform;
        }else return this.transform;
        
    }

    public GameObject FindClosestEnemy(){
        distance = Mathf.Infinity;
        closest = null;
        position = transform.position;

        targets = GameObject.FindGameObjectsWithTag("LockOnTarget");

        foreach (GameObject target in targets)
        {
            distanceFromPlayer = Vector3.Distance(target.transform.position, this.transform.position);
            if (distanceFromPlayer < distance)
            {
                closest = target;
                distance = distanceFromPlayer;
            }
        }
        return closest;
    }
}
