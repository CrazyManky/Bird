using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Screpts.Game;
using Project.Screpts.Screen;
using Project.Screpts.Services;
using Project.Screpts.ShopItems;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContinueScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _balancePlayerWallet;
    [SerializeField] private TextMeshProUGUI _bestScore;
    [SerializeField] private TextMeshProUGUI _scoreGameStatrt;
    [SerializeField] private TextMeshProUGUI _timerValue;
    [SerializeField] private PlayerWallet _playerWallet;
    [SerializeField] private CharactersShopData _playerCharacter;
    [SerializeField] private Image _characterImage;

    private int _priceContinue = 100;
    private PauseService _pauseService;
    private GameStats _gameStats;
    private CountdownTimer _countdownTimer;
    private GameScreen _gameScreen;
    private DialogLauncher _dialogLauncher;
    private AudioManager _audioManager;

    public void Init(GameScreen gameScreen)
    {
        _gameScreen = gameScreen;
        _pauseService = ServiceLocator.Instance.GetService<PauseService>();
        _gameStats = ServiceLocator.Instance.GetService<GameStats>();
        _audioManager = ServiceLocator.Instance.GetService<AudioManager>();
        LoadData();
        _countdownTimer = new CountdownTimer(_timerValue);
        _characterImage.sprite = _playerCharacter.GetActiveSprite();
        _countdownTimer.StartTimer();
        _countdownTimer.OnTimerEnd += ShowGameAndScreen;
        _dialogLauncher = ServiceLocator.Instance.GetService<DialogLauncher>();
    }


    public void ContinueGame()
    {
        if (_playerWallet.Value >= _priceContinue)
        {
            _audioManager.PlayButtonClick();
            _playerWallet.RemoveVolute(_priceContinue);
            _pauseService.ContinueAllItems();
            _gameScreen.ContinueGame();
            Destroy(gameObject);
        }
    }

    public void ShowGameAndScreen()
    {
        _audioManager.PlayButtonClick();
        _dialogLauncher.ShowGameAndScreen();
    }

    private void LoadData()
    {
        _balancePlayerWallet.text = $"{_playerWallet.Value}";
        _bestScore.text = $"{_gameStats.BestScore}";
        _scoreGameStatrt.text = $"{_gameStats.GameCount}";
    }

    private void OnDestroy()
    {
        _countdownTimer.OnTimerEnd -= ShowGameAndScreen;
        _countdownTimer.OnDestroy();
    }
}

public class CountdownTimer
{
    private TextMeshProUGUI _timerText;
    private int startTime = 10;
    private int currentTime;
    private bool isRunning = false;

    private CancellationTokenSource cancellationTokenSource;

    public event Action OnTimerEnd;

    public CountdownTimer(TextMeshProUGUI textTimer)
    {
        _timerText = textTimer;
    }

    public void OnDestroy()
    {
        cancellationTokenSource?.Cancel();
    }

    public void StartTimer()
    {
        currentTime = startTime;
        if (!isRunning)
        {
            isRunning = true;
            cancellationTokenSource = new CancellationTokenSource();
            RunTimer(cancellationTokenSource.Token).Forget(); // Запускаем асинхронную задачу
        }
    }

    private async UniTaskVoid RunTimer(CancellationToken cancellationToken)
    {
        try
        {
            while (currentTime > 0 && isRunning)
            {
                UpdateTimerDisplay();
                await UniTask.Delay(1000, cancellationToken: cancellationToken);
                currentTime--;
            }

            if (currentTime <= 0)
                TimerEnd();
        }
        catch (OperationCanceledException)
        {
            Debug.Log("Таймер был отменён.");
        }
        finally
        {
            isRunning = false;
        }
    }

    private void UpdateTimerDisplay()
    {
        _timerText.text = $"{currentTime} SECONDS";
    }

    private void TimerEnd()
    {
        OnTimerEnd?.Invoke();
    }
}