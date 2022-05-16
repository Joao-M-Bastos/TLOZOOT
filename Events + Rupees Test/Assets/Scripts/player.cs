using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]
    private Transform position;
    [SerializeField]
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material.SetColor("_Color", Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            transform.RotateAround(transform.position, Vector3.up, 60f * Time.deltaTime * Input.GetAxis("Horizontal"));
        }
    }
}
