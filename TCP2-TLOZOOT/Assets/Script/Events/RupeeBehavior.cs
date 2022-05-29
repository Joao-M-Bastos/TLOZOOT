using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RupeeBehavior : MonoBehaviour
{
    public GameObject player;

    private float distanceFromPlayer;
    [SerializeField]
    private int increase;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);
        if(distanceFromPlayer < 10){
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
