using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSalaPrincipal : MonoBehaviour
{

    public GameObject cameraPrincipal;
    public GameObject cameraSala;
//public GameObject cameraCollider;
    // Start is called before the first frame update
    void Start()
    {
        cameraSala.SetActive(false);
        cameraPrincipal.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cameraPrincipal.SetActive(false);
        cameraSala.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cameraPrincipal.SetActive(true);
        cameraSala.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
