using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBase : MonoBehaviour
{
    public GameObject goalPortal;
    Animator animator;
    private Collectible collect;

    private void Start()
    {
        animator = GetComponent<Animator>();
        collect = GameObject.Find("Player").GetComponent<Collectible>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("hasWeight", true);
        
        if (collect.pressed)
        {
            goalPortal.GetComponent<Animator>().SetBool("open", true);
            collect._locked = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("hasWeight", false);
        
        if (collect.pressed)
        {
            goalPortal.GetComponent<Animator>().SetBool("open", false);
            collect._locked = true;
        }
    }
}
