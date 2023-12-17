using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_Cai : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        this.anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            this.StartCoroutine(FallCooldown());
            
        }
    }

    IEnumerator FallCooldown()
    {
        yield return new WaitForSeconds(0.25f);
        this.anim.SetBool("isFall", true);
    }
}
