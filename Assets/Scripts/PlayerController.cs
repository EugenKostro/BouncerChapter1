using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Preuzmi Animator komponentu
        animator = GetComponent<Animator>();
    }

   public void TurnLeft()
{
    animator.SetBool("isTurningLeft", true);
    animator.SetBool("isTurningRight", false);
}

public void TurnRight()
{
    animator.SetBool("isTurningRight", true);
    animator.SetBool("isTurningLeft", false);
}



}
