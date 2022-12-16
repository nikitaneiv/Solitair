using System;
using System.Linq;
using UnityEngine;

namespace Store
{
    public class StoreController : MonoBehaviour
    {
        [SerializeField] private StoreConfig storeConfig;
        [SerializeField] private Transform grid;
        [SerializeField] private StoreUiItem prefab;
        [SerializeField] private StoreButton storeButton;
        private StoreUiItem[] _items;
        private int _selectedId;
       

        private void OnEnable()
        {
            if (_items != null)
            {
                for (int i = 0; i < _items.Length; i++)
                {
                    _items[i].OnClick -= OnItemClick;
                    Destroy(grid.GetChild(i));
                }
            }


            var itemsCount = storeConfig.Items.Length;
            _items = new StoreUiItem[itemsCount];
            for (int i = 0; i < itemsCount; i++)
            {
                var item = Instantiate(prefab, grid);
                item.Init(storeConfig.Items[i]);
                _items[i] = item;
                item.OnClick += OnItemClick;
                item.SetSelected(false);
                if (!storeConfig.IsEnableForBuy(item.ID))
                    item.SetUnActive();
            }

            storeButton.Setup(string.Empty, false);
        }

        private void Start()
        {
            storeButton.OnClick += OnStoreButtonClick;
        }

        private void OnDestroy()
        {
            storeButton.OnClick -= OnStoreButtonClick;
        }

        private void OnStoreButtonClick()
        {
            storeConfig.Buy(_selectedId);
            storeButton.Setup(string.Empty, false);
            _items.FirstOrDefault(item => item.ID == _selectedId)?.SetUnActive();
        }

        private void OnItemClick(int id)
        {
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i].SetSelected(id == _items[i].ID);
            }

            _selectedId = id;
            var item = storeConfig.GetItemByID(_selectedId);
            storeButton.Setup($"Buy for {item.Price}", item.Price <= 100);
        }
    }
}