using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreFabAnimato : MonoBehaviour
{
    public Animator prefebAnim;

    private bool isWalking, isRunning, isInCorner, isClimbing, isSwiming, trigAttack, isBlocking;

    public void Awake()
    {
        this.prefebAnim = this.gameObject.GetComponent<Animator>();
    }

    public void Update()
    {
        this.prefebAnim.SetBool("Attack", TrigAttack);
        this.prefebAnim.SetBool("isBlocking", IsBlocking);
        this.prefebAnim.SetBool("isSwiming", IsSwiming);
        this.prefebAnim.SetBool("isClimbing", IsClimbing);
        this.prefebAnim.SetBool("isInCorner", IsInCorner);
        this.prefebAnim.SetBool("isRunning", IsRunning);
        this.prefebAnim.SetBool("isWalking", IsWalking);
        if (TrigAttack) TrigAttack = false;
    }


    public void ResetAllAnimations()
    {
        IsBlocking = false;
        TrigAttack = false;
        IsSwiming = false;
        IsClimbing = false;
        IsInCorner = false;
        IsRunning = false;
        IsWalking = false;
    }

    public bool IsBlocking
    {
        set { this.isBlocking = value; }
        get { return this.isBlocking; }
    }

    public bool TrigAttack
    {
        set { this.trigAttack = value; }
        get { return this.trigAttack; }
    }

    public bool IsWalking
    {
        set { this.isWalking = value; }
        get { return this.isWalking; }
    }

    public bool IsRunning
    {
        set { this.isRunning = value; }
        get { return this.isRunning; }
    }

    public bool IsInCorner
    {
        set { this.isInCorner = value; }
        get { return this.isInCorner; }
    }
    public bool IsClimbing
    {
        set { this.isClimbing = value; }
        get { return this.isClimbing; }
    }
    public bool IsSwiming
    {
        set { this.isSwiming = value; }
        get { return this.isSwiming; }
    }
}
