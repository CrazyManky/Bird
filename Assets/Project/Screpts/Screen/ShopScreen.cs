using System.Collections.Generic;
using Project.Screpts.Game;
using Project.Screpts.ShopItems;
using Services;
using TMPro;
using UnityEngine;

namespace Project.Screpts.Screen
{
    public class ShopScreen : BaseScreen
    {
        [SerializeField] private CharactersShopData _shopData;
        [SerializeField] private List<ShopItem> _shopItems;
        [SerializeField] private TextMeshProUGUI _playerBalanceValue;

        private PlayerWallet _playerWallet => ServiceLocator.Instance.GetService<PlayerWallet>();

        public override void Init()
        {
            base.Init();
            LoadData();
            SetDataPlayerWallet();
        }

        private void LoadData()
        {
            for (int i = 0; i < _shopItems.Count; i++)
            {
                var shopItem = _shopData.GetShopItem(i);
                _shopItems[i].SetDataShopItem(shopItem.SpriteItem, shopItem.Price, shopItem.Active, shopItem.ItemID);
                _shopItems[i].OnBuyShopItem += UnlockItem;
            }
        }

        public void UnlockItem(int id)
        {
            _shopData.UnlockShopItem(id);
            Dialog.ShowMenuScreen();
            AudioManager.PlayButtonClick();
        }

        private void SetDataPlayerWallet() => _playerBalanceValue.text = $"{_playerWallet.Value}";


        public override void Сlose()
        {
            for (int i = 0; i < _shopItems.Count; i++)
            {
                var shopItem = _shopData.GetShopItem(i);
                _shopItems[i].OnBuyShopItem -= UnlockItem;
            }

            base.Сlose();
        }
    }
}