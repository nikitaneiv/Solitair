using Solitaire;
using UnityEngine;

namespace DefaultNamespace
{
    public class CardPlace : MonoBehaviour
    {
        [SerializeField] protected int nextCardValue = -1;
        [SerializeField] protected float onGameZOffset = -2f;
        [SerializeField] protected CardColor nextCardColor = CardColor.Any;
        [SerializeField] protected CardType nextCardType = CardType.Any;
        [SerializeField] protected Transform cardContainer;
        [SerializeField] protected bool isMain;

        protected bool _isOpen = true;

        public int NextCardValue => nextCardValue;

        public CardColor NextCardColor => nextCardColor;

        public CardType NextCardType => nextCardType;

        public Transform CardContainer => cardContainer;

        public bool IsMain => isMain;

        public bool IsOpen => _isOpen;

        public bool IsCanConnect(PlayingCard playingCard)
        {
            if (!_isOpen)
                return false;

            if (playingCard.Value != nextCardValue && nextCardValue != -1)
                return false;

            if (nextCardColor != CardColor.Any && playingCard.Color != nextCardColor)
                return false;

            if (nextCardType != CardType.Any && playingCard.Type != nextCardType)
                return false;

            Vector3 position = playingCard.CardContainer.transform.localPosition;
            position.z = isMain ? 0f : onGameZOffset;
            playingCard.CardContainer.localPosition = position;
            return true;
        }
    }
}