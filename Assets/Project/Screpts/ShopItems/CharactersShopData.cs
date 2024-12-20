using System;
using System.Collections.Generic;
using UnityEngine;


namespace Project.Screpts.ShopItems
{
    [CreateAssetMenu(fileName = "ShopItemsData", menuName = "Shop Items")]
    public class CharactersShopData : ScriptableObject
    {
        [SerializeField] private List<ShopItemData> _shopItems;

        public int ShopItemsCount => _shopItems.Count;

        public Sprite SelectedSprite;

        public Sprite GetActiveSprite()
        {
            return SelectedSprite;
        }

        public ShopItemData GetShopItem(int itemID)
        {
            return _shopItems[itemID];
        }

        public void UnlockShopItem(int itemID)
        {
            for (int i = 0; i < _shopItems.Count; i++)
            {
                if (_shopItems[i].ItemID == itemID)
                {
                    _shopItems[i].UnlockItem();
                    SelectedSprite = _shopItems[i].GetSprite();
                }
            }
        }
    }

    [Serializable]
    public class ShopItemData
    {
        [SerializeField] private bool _activeItem;

        public int ItemID;
        public bool Active => _activeItem;
        public int Price;
        public Sprite SpriteItem;

        public Sprite GetSprite() => SpriteItem;
        public void UnlockItem() => _activeItem = true;
    }
}