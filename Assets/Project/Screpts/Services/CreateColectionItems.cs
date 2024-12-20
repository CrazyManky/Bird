using System;
using Project.Screpts.ShopItems;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Project.Screpts.Services
{
    public class CreateColectionItems : IService
    {
        private ColectetedItem _colectetedItemPrefab;
        public float margin = 1.3f;
        private Camera mainCamera; 
        private Vector2 screenBounds;
        private ColectetedItem currentObject;
        public ColectetedItem CurrentObject => currentObject;

        public event Action ItemColected;

        public CreateColectionItems(ColectetedItem colectetedItemPrefab)
        {
            _colectetedItemPrefab = colectetedItemPrefab;
        }

        public void Init()
        {
            mainCamera = Camera.main;
            screenBounds =
                mainCamera.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, UnityEngine.Screen.height, 0));

            SpawnRandomObject();
        }

        public void SpawnRandomObject()
        {
            if (currentObject != null)
            {
                ItemColected?.Invoke();
                Object.Destroy(currentObject);
            }


            float randomX = Random.Range(-screenBounds.x + margin, screenBounds.x - margin);
            float randomY = Random.Range(-screenBounds.y + margin, screenBounds.y - margin);

            Vector2 spawnPosition = new Vector2(randomX, randomY);
            currentObject = Object.Instantiate(_colectetedItemPrefab, spawnPosition, Quaternion.identity);
        }
    }
}