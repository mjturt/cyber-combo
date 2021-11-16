using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBase : MonoBehaviour
{
    public GameObject goalPortal;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("hasWeight", true);
        goalPortal.GetComponent<Animator>().SetBool("open", true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("hasWeight", false);
        goalPortal.GetComponent<Animator>().SetBool("open", false);
    }
}
