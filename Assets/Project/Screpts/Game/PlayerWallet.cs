using UnityEngine;

namespace Project.Screpts.Game
{
    [CreateAssetMenu(fileName = "PlayerWallet", menuName = "PlayerData/PlayerWallet")]
    public class PlayerWallet : ScriptableObject, IService
    {
        [SerializeField] private int _playerVolute;

        public int Value => _playerVolute;

        public void AddVolute(int value)
        {
            if (value > 0)
                _playerVolute += value;
        }

        public void RemoveVolute(int value)
        {
            if (value <= Value)
            {
                _playerVolute -= value;
            }
        }
    }
}