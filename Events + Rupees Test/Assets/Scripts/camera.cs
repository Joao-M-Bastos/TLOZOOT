using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.TransformPoint(0f, 1.5f, -3f);
        transform.LookAt(player.transform);
        transform.Rotate(-15.5f, 0f, 0f);
    }
}
