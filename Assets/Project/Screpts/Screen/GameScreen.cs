using System.Collections.Generic;
using Project.Screpts.Game;
using Project.Screpts.GameItem;
using Project.Screpts.Interfaces;
using Project.Screpts.Services;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Screpts.Screen
{
    public class GameScreen : BaseScreen
    {
        [SerializeField] private BlockItem _gamePrefab;
        [SerializeField] private BlockItem _gameMiniBlockPrefab;
        [SerializeField] private GameStats _gameStats;
        [SerializeField] private TextMeshProUGUI _colectedScoreText;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private ContinueScreen _continueScreen;

        private List<BlockItem> _instanceBlocks = new List<BlockItem>();

        private int _collectedItems = 0;
        private bool _paused = false;

        private IPositionScreenService _positionScreenService;

        private readonly Vector2 _instanceUpScreenPosition = new Vector2(0.5f, 0.95f);
        private readonly Vector2 _instanceDownScreenPosition = new Vector2(0.5f, 0.05f);

        private MoveCharacter _moveCharacter;
        private CharacterInstance _characterInstance;
        private InputHandler _inputHandler;
        private CreateColectionItems _createColectionItems;
        private PauseService _pauseService;
        private EdgeObjectSpawner _edgeObjectSpawner;

        public void OnEnable() => _pauseButton.onClick.AddListener(PauseGame);

        public override void Init()
        {
            base.Init();
            _positionScreenService = new PositionScreenService();
            GetServices();
            _pauseService.ClearPauseItems();
            _moveCharacter = new MoveCharacter(_characterInstance.BirdControllerInstance, Camera.main);
            _inputHandler = new InputHandler(_characterInstance.BirdControllerInstance);
            _gameStats.AddGameStartCount(1);
            _characterInstance.BirdControllerInstance.Continue();
            SpawnObjectAtScreenEdge(_instanceUpScreenPosition);
            SpawnObjectAtScreenEdge(_instanceDownScreenPosition);
            _createColectionItems.Init();
            _edgeObjectSpawner =
                new EdgeObjectSpawner(_gameMiniBlockPrefab, _characterInstance.BirdControllerInstance, this);
            _createColectionItems.ItemColected += AddRecordValue;
            _pauseService.AddPauseItem(_characterInstance.BirdControllerInstance);
        }

        private void GetServices()
        {
            _characterInstance = ServiceLocator.Instance.GetService<CharacterInstance>();
            _characterInstance.InstanceCharacter();
            _createColectionItems = ServiceLocator.Instance.GetService<CreateColectionItems>();
            _pauseService = ServiceLocator.Instance.GetService<PauseService>();
        }

        private void Update()
        {
            _inputHandler.HandleInput();
            _edgeObjectSpawner.CheckAndSpawn();
        }

        public void FixedUpdate()
        {
            if (_moveCharacter != null)
            {
                _moveCharacter.MoveHorizontally();
                _moveCharacter.CheckHorizontalBounds();
            }
        }

        public void AddRecordValue()
        {
            _collectedItems++;
            _colectedScoreText.text = $"{_collectedItems}";
            _gameStats.AddValueScore(_collectedItems);
        }

        public void PauseGame()
        {
            var value = !_paused;
            _paused = value;
            if (_paused)
            {
                _pauseService.PauseAllItems();
            }
            else
            {
                _pauseService.ContinueAllItems();
            }
        }

        private void SpawnObjectAtScreenEdge(Vector2 screenAnchor)
        {
            Vector3 spawnPosition = _positionScreenService.GetScreenPosition(screenAnchor);
            Quaternion rotationToCenter = _positionScreenService.GetRotationTowardsCenter(spawnPosition);
            var instanceBlock = Instantiate(_gamePrefab, spawnPosition, rotationToCenter);
            _instanceBlocks.Add(instanceBlock);
            instanceBlock.PlayerEnter += OpenContinueScreen;
        }

        public void OpenContinueScreen()
        {
            var instanceContinueScreen = Instantiate(_continueScreen, transform);
            _characterInstance.BirdControllerInstance.gameObject.SetActive(false);
            _createColectionItems.CurrentObject.gameObject.SetActive(false);
            instanceContinueScreen.Init(this);
            _pauseService.PauseAllItems();
            _edgeObjectSpawner.ClearObjects();
        }

        public void ContinueGame()
        {
            _characterInstance.BirdControllerInstance.gameObject.SetActive(true);
            _createColectionItems.CurrentObject.gameObject.SetActive(true);
        }

        public override void Сlose()
        {
            _instanceBlocks.ForEach((e) => Destroy(e.gameObject));
            _pauseService.ClearPauseItems();
            base.Сlose();
        }

        private void OnDisable() => _pauseButton.onClick.RemoveListener(PauseGame);
    }
}