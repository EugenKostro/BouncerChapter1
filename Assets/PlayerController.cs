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

    public void TurnRight()
    {
        // Okretanje igrača na desno
        transform.localScale = new Vector3(14, 11, 1); // Podesi skalu da bude normalna za desnu stranu
        animator.SetTrigger("Player-right"); // Pokreni animaciju okretanja desno
    }
}
