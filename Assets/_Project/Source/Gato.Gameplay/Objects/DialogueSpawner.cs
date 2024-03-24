using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gato.Gameplay
{
    public class DialogueSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _dialogueUI;
        public bool StartWithDialogue;
        private void Awake()
        {
            _dialogueUI.SetActive(StartWithDialogue);
        }

        private void Update()
        {
        
        }

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (collider2D.gameObject.CompareTag("Player") == false) return;
            _dialogueUI.SetActive(true);
        }
    }
}
