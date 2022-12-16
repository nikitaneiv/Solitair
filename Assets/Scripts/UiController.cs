using System;
using Ads;
using UnityEngine;

namespace DefaultNamespace
{
    public class UiController : MonoBehaviour
    {
        private GameController _gameController;
        [SerializeField] private GameObject game;
        [SerializeField] private GameObject win;

        private void Awake()
        {
            _gameController = FindObjectOfType<GameController>();
            _gameController.OnWin += OnWin;
            OnGame();
        }

        private void OnDestroy()
        {
            _gameController.OnWin -= OnWin;
        }

        private void OnWin()
        {
            game.SetActive(false);
            win.SetActive(true);
        }

        public void OnGame()
        {
            game.SetActive(true);
            win.SetActive(false);
            FindObjectOfType<AdsInitializer>().ShowAdReward();
        }
    }
}