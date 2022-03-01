using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBase : MonoBehaviour
{
    private GameObject goalPortal;
    Animator animator;
    private Collectible collect;
    private int timer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        collect = GameObject.Find("Player").GetComponent<Collectible>();
        goalPortal = GameObject.Find("GoalPortal");
    }

    private void Update()
    {
        if (timer > 0)
            timer--;
        else
        {
            goalPortal.GetComponent<Animator>().SetBool("open", false);
            collect._locked = true;
            animator.SetBool("hasWeight", false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (animator.GetBool("hasWeight") == false)
        {
            animator.SetBool("hasWeight", true);
            if (collect.pressed)
            {
                goalPortal.GetComponent<Animator>().SetBool("open", true);
                collect._locked = false;
            }
        }
        
        timer = 10;
    }
}
