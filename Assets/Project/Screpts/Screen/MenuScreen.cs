using Project.Screpts.Game;
using Project.Screpts.ShopItems;
using Project.Screpts.SO;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Screpts.Screen
{
    public class MenuScreen : BaseScreen
    {
        [SerializeField] private TextMeshProUGUI _balancePlayerWallet;
        [SerializeField] private TextMeshProUGUI _bestScore;
        [SerializeField] private TextMeshProUGUI _scoreGameStatrt;
        [SerializeField] private PolicyScreen _policyScreenPrefab;
        [SerializeField] private StartPanelInGame _panelPrefab;
        [SerializeField] private CharactersShopData _charactersShopData;
        [SerializeField] private Image _characterActive;
        [SerializeField] private SounConfig _sounConfig;
        [SerializeField] private Image _sounImage;
        [SerializeField] private Sprite _soundActive;
        [SerializeField] private Sprite _soundDisable;

        private PlayerWallet _playerWallet;
        private GameStats _gameStats;
        private StartPanelInGame _startPanel;
        private CharacterInstance _characterInstance;

        public override void Init()
        {
            base.Init();
            _playerWallet = ServiceLocator.Instance.GetService<PlayerWallet>();
            _gameStats = ServiceLocator.Instance.GetService<GameStats>();
            _characterInstance = ServiceLocator.Instance.GetService<CharacterInstance>();
            LoadData();
            CreateClickPanel();
            SetData(_sounConfig.AudioActive);
        }

        private void LoadData()
        {
            _balancePlayerWallet.text = $"{_playerWallet.Value}";
            _bestScore.text = $"{_gameStats.BestScore}";
            _scoreGameStatrt.text = $"{_gameStats.GameCount}";
            _characterActive.sprite = _charactersShopData.GetActiveSprite();
        }

        private void SetData(bool value)
        {
            if (value)
            {
                _sounImage.sprite = _soundActive;
            }
            else
            {
                _sounImage.sprite = _soundDisable;
            }
        }

        private void CreateClickPanel()
        {
            _startPanel = Instantiate(_panelPrefab, Dialog.transform);
            _startPanel.transform.SetSiblingIndex(1);
            _startPanel.OnStartPanelClicked += StartGame;
        }

        public void StartGame()
        {
            Destroy(_startPanel.gameObject);
            Dialog.ShowGameScreen();
        }

        public void SwitchSoundValue()
        {
            AudioManager.PlayButtonClick();
            _sounConfig.SwitchAudio();
            SetData(_sounConfig.AudioActive);
        }

        public void ShowPolicyScreen()
        {
            AudioManager.PlayButtonClick();
            Instantiate(_policyScreenPrefab, Dialog.transform);
        }

        public void ShowShopScreen()
        {
            AudioManager.PlayButtonClick();
            Destroy(_startPanel.gameObject);
            Dialog.ShowShopScreen();
        }
    }
}