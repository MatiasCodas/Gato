using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixadeTexto : MonoBehaviour
{

    public GameObject caixaDeTexto;

    // Start is called before the first frame update
    void Start()
    {
        caixaDeTexto.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Time.timeScale = 0;
            caixaDeTexto.SetActive (true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        caixaDeTexto.SetActive (!false);
        Destroy(caixaDeTexto);
    }
    // Update is called once per frame
    void Update()
    {
      //s if (Input.GetButtonDown("Dash"))
//{
        //    Time.timeScale = 1;
         //   caixa/DeTexto.SetActive(false);
         //   Destroy(this.gameObject);

        }
    }

