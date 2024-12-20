using UnityEngine;

namespace Project.Screpts.Game
{
    [CreateAssetMenu(fileName = "GameStats", menuName = "Game/Game Stats")]
    public class GameStats : ScriptableObject, IService
    {
        [SerializeField] private int _gameCount;
        [SerializeField] private int _bestScore;

        public int GameCount => _gameCount;
        public int BestScore => _bestScore;


        public void AddValueScore(int value)
        {
            if (value > 0 && value > _bestScore)
            {
                _bestScore = value;
            }
        }

        public void AddGameStartCount(int value)
        {
            if (value > 0)
                _gameCount += value;
        }
    }
}