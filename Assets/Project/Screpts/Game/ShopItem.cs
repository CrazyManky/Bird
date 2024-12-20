using System;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Screpts.Game
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private Image _imageShopItem;
        [SerializeField] private TextMeshProUGUI _textPriceShopItem;
        [SerializeField] private Button _buttonBuyItem;

        private int _shopItemPrice;
        private int _itemID;
        private const string _shopItemUnlockPrice = "Free";
        private PlayerWallet _playerWallet => ServiceLocator.Instance.GetService<PlayerWallet>();

        public event Action<int> OnBuyShopItem;

        public void OnEnable() => _buttonBuyItem.onClick.AddListener(ClickButtonBuyShopItem);


        public void SetDataShopItem(Sprite spriteItem, int price, bool IsUnlocked, int itemID)
        {
            _imageShopItem.sprite = spriteItem;
            _itemID = itemID;
            SetPriceData(price, IsUnlocked);
        }

        private void SetPriceData(int price, bool IsUnlocked)
        {
            if (IsUnlocked)
            {
                _textPriceShopItem.text = _shopItemUnlockPrice;
            }
            else
            {
                _shopItemPrice = price;
                _textPriceShopItem.text = price.ToString();
            }
        }

        public void ClickButtonBuyShopItem()
        {
            _playerWallet.RemoveVolute(_shopItemPrice);
            Debug.Log("Покупка");
            OnBuyShopItem?.Invoke(_itemID);
        }

        private void OnDisable() => _buttonBuyItem.onClick.RemoveListener(ClickButtonBuyShopItem);
    }
}