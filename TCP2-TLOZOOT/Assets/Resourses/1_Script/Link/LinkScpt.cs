using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkScpt : MonoBehaviour
{
    #region Movement

    //Walk
    [SerializeField] public float baseSpeed;
    [SerializeField] public float speedClimb;
    private float InputX, InputZ;

    //Jump
    public float jumpforce;

    //Camera
    [SerializeField] Transform cameraTransform;
    [SerializeField] float turnSmoothVelocity, turnSmoothTime;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
