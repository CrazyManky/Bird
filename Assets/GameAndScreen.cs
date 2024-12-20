using Project.Screpts.Game;
using Project.Screpts.Screen;
using Services;
using TMPro;
using UnityEngine;

public class GameAndScreen : BaseScreen
{
    [SerializeField] private TextMeshProUGUI _balancePlayerWallet;
    [SerializeField] private TextMeshProUGUI _bestScore;
    [SerializeField] private TextMeshProUGUI _scoreGameStatrt;
    [SerializeField] private TextMeshProUGUI _playerValue;

    private PlayerWallet _playerWallet;
    private GameStats _gameStats;

    public override void Init()
    {
        base.Init();
        _playerWallet = ServiceLocator.Instance.GetService<PlayerWallet>();
        _gameStats = ServiceLocator.Instance.GetService<GameStats>();
        LoadData();
    }

    public void RestartGame()
    {
        AudioManager.PlayButtonClick();
        Dialog.ShowGameScreen();
    }

    public void ShowMenu()
    {
        AudioManager.PlayButtonClick();
        Dialog.ShowMenuScreen();
    }

    private void LoadData()
    {
        _balancePlayerWallet.text = $"{_playerWallet.Value}";
        _bestScore.text = $"{_gameStats.BestScore}";
        _scoreGameStatrt.text = $"{_gameStats.GameCount}";
        _playerValue.text = $"{_playerWallet.Value}";
    }
}