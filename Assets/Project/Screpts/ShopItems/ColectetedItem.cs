using Project.Screpts.Game;
using Project.Screpts.Services;
using Services;
using UnityEngine;

namespace Project.Screpts.ShopItems
{
    public class ColectetedItem : MonoBehaviour
    {
        private CreateColectionItems _createColectionItems;
        private PlayerWallet _playerWallet;

        private void OnEnable()
        {
            _createColectionItems = ServiceLocator.Instance.GetService<CreateColectionItems>();
            _playerWallet = ServiceLocator.Instance.GetService<PlayerWallet>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<BirdController>(out var bird))
            {
                _createColectionItems.SpawnRandomObject();
                _playerWallet.AddVolute(10);
                Destroy(gameObject);
            }
        }
    }
}