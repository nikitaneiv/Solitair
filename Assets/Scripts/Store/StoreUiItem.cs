using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Store
{
    public class StoreUiItem : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private Image selectedBg;

        private Button _button;
        private int _id;
        public event Action<int> OnClick;

        public int ID => _id;

        private void Awake()
        {
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
            OnClick?.Invoke(_id);
        }

        public void Init(StoreItem item)
        {
            _id = item.ID;
            image.sprite = item.Icon;
            titleText.text = item.Title;
            descriptionText.text = item.Description;
        }

        public void SetSelected(bool isOn)
        {
            selectedBg.enabled = isOn;
        }

        public void SetUnActive()
        {
            _button.interactable = false;
            SetSelected(false);
        }
    }
}