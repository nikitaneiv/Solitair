using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController : MonoBehaviour
    {
        private const string LAYER_KEY = "PlayingCard";

        private CardDeck _cardDeck;
        private PlayingCard _holdCard;
        private Camera _mainCamera;
        private LayerMask _layerMask;

        private Vector3 _offset;

        public event System.Action<CardType, int> OnAddToMain;
        public event System.Action<CardType, int> OnRemoveFromMain;

        private void Awake()
        {
            _cardDeck = FindObjectOfType<CardDeck>();
            _mainCamera = Camera.main;
            _layerMask = LayerMask.GetMask(LAYER_KEY);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryHoldCard();
            }

            if (Input.GetMouseButton(0) && _holdCard != null)
            {
                MoveCardWithMouse();
            }

            if (Input.GetMouseButtonUp(0) && _holdCard != null)
            {
                ReleaseCard();
            }
        }

        private void TryHoldCard()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _layerMask))
            {
                if (hit.collider.gameObject.TryGetComponent(out PlayingCard card))
                {
                    _holdCard = card;
                    _holdCard.transform.Translate(Vector3.back * 0.5f, Space.World);
                    Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    mousePosition.z = _holdCard.transform.position.z;
                    _offset = _holdCard.transform.position - mousePosition;
                }
            }
        }

        private void ReleaseCard()
        {
            // Vector3 checkPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            // checkPosition.z = _holdCard.transform.position.z;

            Ray ray = new Ray(_holdCard.transform.position, Vector3.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, _layerMask))
            {
                if (hit.collider.gameObject.TryGetComponent(out CardPlace place))
                {
                    if (place.IsCanConnect(_holdCard))
                    {
                        if (place.IsMain)
                        {
                            OnAddToMain?.Invoke(_holdCard.Type, _holdCard.Value);
                        }

                        if (_holdCard.IsMain && !place.IsMain)
                        {
                            OnRemoveFromMain?.Invoke(_holdCard.Type, _holdCard.Value);
                        }

                        _holdCard.SetParent(place);
                        if (_holdCard.IsInDeck)
                        {
                            _cardDeck.ExcludeCurrentCard();
                        }
                    }
                    else
                    {
                        _holdCard.SetParent();
                    }
                }
            }
            else
            {
                _holdCard.SetParent();
            }

            _holdCard = null;
        }

        private void MoveCardWithMouse()
        {
            Vector3 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = _holdCard.transform.position.z;
            _holdCard.transform.position = mousePosition + _offset;
        }
    }
}