using Solitaire;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayingCard : CardPlace
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Transform openCardContainer;
        [SerializeField] private Transform closeCardContainer;

        private int _value;
        private CardColor _color;
        private CardType _type;
        private CardPlace _parent;

        public int Value => _value;

        public CardColor Color => _color;

        public CardType Type => _type;

        public CardPlace Parent => _parent;

        public bool IsInDeck { get; set; } = true;

        public void Initialize(int value, CardColor color, CardType type, Material material)
        {
            _value = value;
            _color = color;
            _type = type;
            meshRenderer.material = material;

            nextCardValue = _value - 1;
            nextCardColor = _color == CardColor.Red ? CardColor.Black : CardColor.Red;
            nextCardType = CardType.Any;
        }

        public void Open()
        {
            if (_isOpen) return;

            _isOpen = true;
            cardContainer = openCardContainer;
            transform.Rotate(Vector3.forward * 180f, Space.Self);
        }

        public void Close()
        {
            if (!_isOpen) return;

            _isOpen = false;
            cardContainer = closeCardContainer;
            transform.Rotate(Vector3.forward * -180f, Space.Self);
        }

        public void SetParent(CardPlace parent = null)
        {
            if (parent == null)
            {
                transform.localPosition = Vector3.zero;
            }
            else
            {
                transform.SetParent(parent.CardContainer);
                transform.localPosition = Vector3.zero;
                SetAtMain(parent.IsMain);
                if (_parent is PlayingCard playingCard)
                {
                    playingCard.Open();
                }

                _parent = parent;
                
            }
        }

        private void SetAtMain(bool state)
        {
            if (state)
            {
                nextCardColor = _color;
                nextCardType = _type;
                nextCardValue = _value + 1;
            }
            else
            {
                nextCardValue = _value - 1;
                nextCardColor = _color == CardColor.Red ? CardColor.Black : CardColor.Red;
                nextCardType = CardType.Any;
            }

            isMain = state;
        }

        public void Reset()
        {
            SetParent();
            Close();
            SetAtMain(false);
            _parent = null;
        }
    }
}