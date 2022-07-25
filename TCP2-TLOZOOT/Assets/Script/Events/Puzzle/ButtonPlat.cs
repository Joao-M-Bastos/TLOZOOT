using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPlat : MonoBehaviour
{
    private Animator anim;

    public float CooldownTime;
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
            this.anim.SetBool("IsActive", true);
            this.StartCoroutine(FallCooldown());
        }
    }

    IEnumerator FallCooldown()
    {
        yield return new WaitForSeconds(CooldownTime);
        this.anim.SetBool("IsActive", false);
    }
}
