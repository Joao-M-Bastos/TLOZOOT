using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBox : MonoBehaviour
{
    private Animator anim;
    public Animator doorAnim;

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
        if (collision.gameObject.CompareTag("Puzzle"))
        {
            this.anim.SetBool("IsPush", true);
            this.doorAnim.SetBool("isOpen", true);
        }
    }
}
