using System;
using Cysharp.Threading.Tasks;
using Project.Screpts.Services;
using Services;
using UnityEngine;

namespace Project.Screpts.GameItem
{
    public class BlockItem : MonoBehaviour
    {
        private PauseService _pauseService;
        private float _activationDelay = 10f;
        private bool _isActivated = false;
        
        public event Action PlayerEnter;

        private void OnEnable()
        {
            _pauseService = ServiceLocator.Instance.GetService<PauseService>();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<BirdController>(out BirdController birdController) && !_isActivated)
            {
                _isActivated = true;
                PlayerEnter?.Invoke();
                ActivateWithDelay().Forget();
            }
        }

        private async UniTaskVoid ActivateWithDelay()
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_activationDelay));
                _isActivated = false;

            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка в таймере активации: {ex.Message}");
            }
        }

        private void OnDisable()
        {
            _pauseService = ServiceLocator.Instance.GetService<PauseService>();
            _isActivated = false;
        }
    }
}