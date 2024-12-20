using Project.Screpts.Game;
using Project.Screpts.Services;
using Project.Screpts.ShopItems;
using Services;
using UnityEngine;

namespace Project.Screpts.Screen
{
    public class DialogLauncher : MonoBehaviour, IService
    {
        [SerializeField] private MenuScreen _menuScreen;
        [SerializeField] private GameScreen _gameScreen;
        [SerializeField] private ShopScreen _shopScreen;
        [SerializeField] private GameAndScreen _gameAndScreen;
        [SerializeField] private PlayerWallet _playerWallet;
        [SerializeField] private GameStats _gameStats;
        [SerializeField] private CharacterInstance _characterInstance;
        [SerializeField] private ColectetedItem _colectetedItemPrefab;
        [SerializeField] private AudioManager _audioManager;


        private BaseScreen _activeScreen;

        private void Awake()
        {
            ServiceLocator.Init();
            ServiceLocator.Instance.AddService(this);
            ServiceLocator.Instance.AddService(_playerWallet);
            ServiceLocator.Instance.AddService(_gameStats);
            ServiceLocator.Instance.AddService(_characterInstance);
            ServiceLocator.Instance.AddService(new CreateColectionItems(_colectetedItemPrefab));
            ServiceLocator.Instance.AddService(new PauseService());
            ServiceLocator.Instance.AddService(_audioManager);
        }

        private void Start() => ShowMenuScreen();

        public void ShowMenuScreen()
        {
            _audioManager.PlayMenuMusick();
            ShowScreen(_menuScreen);
        }

        public void ShowShopScreen() => ShowScreen(_shopScreen);

        public void ShowGameScreen()
        {
            _audioManager.PlayGame();
            ShowScreen(_gameScreen);
        }

        public void ShowGameAndScreen() => ShowScreen(_gameAndScreen);


        private void ShowScreen(BaseScreen screen)
        {
            _activeScreen?.Сlose();
            var instanceScreen = Instantiate(screen, transform);
            instanceScreen.Init();
            _activeScreen = instanceScreen;
        }
    }
}