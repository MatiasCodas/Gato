using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Gato.UI
{
    public class DialogueNator : MonoBehaviour
    {
        [Header("Mudar aqui quantos e quais textos terão")]
        [SerializeField] private string[] _dialogueBubbles;
        [Header("Mudar aqui quais artes aparecem em cada fala")]
        [SerializeField] private Sprite[] _enunciatorArt;


        [Header("Referências para o código")]
        [SerializeField] private Image _enunciatorUIImage;
        [SerializeField] private TMP_Text _uiText;
        [SerializeField] private Button _nextButton;
        private int _talkIndex = 0;

        private void Start()
        {
            _nextButton.onClick.AddListener(HandleNextDialogue);
            _talkIndex = 0; 
            SetCurrentDialogue();
            EventSystem.current.SetSelectedGameObject(_nextButton.gameObject);
            Time.timeScale = 0;
        }

        
        private void Update()
        {
        
        }


        private void HandleNextDialogue() 
        {
            _talkIndex++;
            SetCurrentDialogue();
            
        }

        private void SetCurrentDialogue()
        {
            if(_talkIndex >= _dialogueBubbles.Length)
            {
                Destroy(transform.parent.gameObject);
                return;
            }
            int imageIndex = _talkIndex < _enunciatorArt.Length? _talkIndex : _dialogueBubbles.Length - 1;
            _enunciatorUIImage.sprite = _enunciatorArt[imageIndex];
            _uiText.text = _dialogueBubbles[_talkIndex];
        }

        private void OnDestroy()
        {
            Time.timeScale = 1;
        }
    }
}
