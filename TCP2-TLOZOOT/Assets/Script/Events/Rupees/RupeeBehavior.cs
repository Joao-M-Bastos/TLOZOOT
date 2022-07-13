using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RupeeBehavior : MonoBehaviour
{
    public GameObject player;

    private float distanceFromPlayer;
    [SerializeField]
    private int increase;

    private void Awake() {
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        if(distanceFromPlayer < 17.5f){
            this.transform.gameObject.tag = "Rotation";
        }else this.transform.gameObject.tag = "Untagged";
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == player)
        {
            varHolder.inst.rupees += increase;
            Destroy(this.gameObject);
        }
    }
}
