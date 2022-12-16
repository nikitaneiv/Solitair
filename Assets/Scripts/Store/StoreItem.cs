using UnityEngine;

namespace Store
{
    [CreateAssetMenu(fileName = "StoreItem", menuName = "Configs/Store/StoreItem", order = 0)]
    public class StoreItem : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private float price;
        [SerializeField] private string title;
        [SerializeField] private string description;

        [SerializeField] private Sprite icon;

        public int ID => id;

        public float Price => price;

        public string Title => title;

        public string Description => description;

        public Sprite Icon => icon;
    }
}