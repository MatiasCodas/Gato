using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teto : MonoBehaviour

    
{
    public GameObject Mascara;

    
    public void Start()
    { Mascara.SetActive(false);}

        // Este evento é chamado quando outro Collider2D entra no trigger (Collider2D deve estar marcado como Trigger)
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("O jogador entrou na área!");
            // Faça o que for necessário quando o jogador entrar na área
            
            }
        }

        // Este evento é chamado uma vez por frame para cada Collider2D que está tocando o trigger.
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("O jogador está na área!");
            // Faça o que for necessário enquanto o jogador estiver na área
            Mascara.SetActive(true);
        }
        }

        // Este evento é chamado quando o Collider2D sai do trigger
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("O jogador saiu da área!");
            // Faça o que for necessário quando o jogador sair da área
            Mascara.SetActive(false);
        }
        }
    }

