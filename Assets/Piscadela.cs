using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piscadela : MonoBehaviour
{
    public float interval = 100f; // Intervalo de tempo em segundos
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        InvokeRepeating("PlayAnimation", 0f, interval);
    }

    private void PlayAnimation()
    {
        // Troque "NomeDaAnimacao" pelo nome da sua animação no Animator Controller
        animator.SetTrigger("PiscarSP");
    }
}
