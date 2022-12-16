using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Store
{
    public class StoreButton : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private Button _button;
        public event Action OnClick; 
        private void Awake()
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
            _button = GetComponentInChildren<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(OnClickButton);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClickButton);
        }

        private void OnClickButton()
        {
            OnClick?.Invoke();
        }

        public void Setup(string text, bool isActive)
        {
            _text.text = text;
            _button.interactable = isActive;
        }
    }
}