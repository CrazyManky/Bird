using System.Collections.Generic;
using Project.Screpts.GameItem;
using Project.Screpts.Screen;
using UnityEngine;

namespace Project.Screpts.Services
{
    public class EdgeObjectSpawner
    {
        private readonly BlockItem _prefab;
        private readonly BirdController _player;
        private GameScreen _gameScreen;
        private float _spawnPadding = 0.5f;
        private int _minObjects = 3;
        private int _maxObjects = 5;
        private float _minDistanceFromPlayer = 2f;

        private readonly Vector2 _screenBounds;
        private List<BlockItem> _spawnedObjects = new();

        private string _currentSide = "None";

        public EdgeObjectSpawner(BlockItem prefab, BirdController player, GameScreen gameScreen)
        {
            _prefab = prefab;
            _player = player;
            _gameScreen = gameScreen;
            _screenBounds =
                Camera.main.ScreenToWorldPoint(new Vector3(UnityEngine.Screen.width, UnityEngine.Screen.height, 0));
        }

        public void CheckAndSpawn()
        {
            if (_player.transform.position.x >= _screenBounds.x - _spawnPadding && _currentSide != "Right")
            {
                SpawnObjects("Right");
            }
            else if (_player.transform.position.x <= -_screenBounds.x + _spawnPadding && _currentSide != "Left")
            {
                SpawnObjects("Left");
            }
        }

        private void SpawnObjects(string side)
        {
            _currentSide = side;
            ClearObjects();
            float xPosition = side == "Right" ? _screenBounds.x - _spawnPadding : -_screenBounds.x + _spawnPadding;

            int objectCount = Random.Range(_minObjects, _maxObjects + 1);

            float availableHeight = _screenBounds.y * 2;

            float step = availableHeight / (objectCount + 1);

            for (int i = 0; i < objectCount; i++)
            {
                float yPosition = -_screenBounds.y + (i + 1) * step;

                Vector2 spawnPosition = new Vector2(xPosition, yPosition);
                if (Vector2.Distance(spawnPosition, _player.transform.position) >= _minDistanceFromPlayer)
                {
                    var obj = Object.Instantiate(_prefab, spawnPosition, Quaternion.identity);
                    obj.PlayerEnter += _gameScreen.OpenContinueScreen;
                    SetRotation(obj.transform, side);

                    _spawnedObjects.Add(obj);
                }
            }
        }

        private void SetRotation(Transform objTransform, string side)
        {
            float rotationAngle = side == "Right" ? 90f : 270f;
            objTransform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }

        public void ClearObjects()
        {
            foreach (var obj in _spawnedObjects)
            {
                if (obj != null)
                {
                    obj.PlayerEnter -= _gameScreen.OpenContinueScreen;
                    Object.Destroy(obj.gameObject);
                }
            }

            _spawnedObjects.Clear();
        }
    }
}