using System;
using System.Linq;
using UnityEngine;

namespace Store
{
    [CreateAssetMenu(fileName = "StoreConfig", menuName = "Configs/Store/StoreConfig", order = 0)]
    public class StoreConfig : ScriptableObject
    {
        private const string SAVE_KEY = "StoreKey";

        public event Action<int> OnBuy;
        [SerializeField] private StoreItem[] items;

        public StoreItem[] Items => items;

        public StoreItem GetItemByID(int id)
        {
            return items.FirstOrDefault(item => item.ID == id);
        }

        public void Buy(int id)
        {
            PlayerPrefs.SetInt($"{SAVE_KEY}/{id}", 1);
            OnBuy?.Invoke(id);
        }

        public bool IsEnableForBuy(int id)
        {
            return PlayerPrefs.GetInt($"{SAVE_KEY}/{id}", 0) == 0;
        }
    }
}