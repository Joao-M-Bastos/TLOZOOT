using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RupeeBehavior : MonoBehaviour
{
    public GameObject player;
    [SerializeField]
    private int increase;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 36, 0) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            varHolder.inst.rupees += increase;
            Destroy(this.gameObject);
        }
    }
}
