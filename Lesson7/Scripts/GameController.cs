using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Geekbrains
{
    public sealed class GameController : MonoBehaviour, IDisposable
    {
        #region Fields
        
        public PlayerType PlayerType = PlayerType.Ball;
        private Player player = null;
        private ListExecuteObject _interactiveObject;
        private DisplayEndGame _displayEndGame;
        private DisplayBonuses _displayBonuses;
        private CameraController _cameraController;
        private MiniMapMVC _miniMapController;
        private RadarMVC _radarController;
        private InputController _inputController;
        private Reference _reference;
        private int _countBonuses;

        #endregion

        #region UnityMethods  

        private void Awake()
        {
            _interactiveObject = new ListExecuteObject();

            _reference = new Reference();
                        
            if (PlayerType == PlayerType.Ball)
            {
                player = _reference.PlayerBall;
            }

            InitializeControllers();

            _displayEndGame = new DisplayEndGame(_reference.EndGame);
            _displayBonuses = new DisplayBonuses(_reference.Bonuse);
            
            _radarController.RegisterRadarObject(player.gameObject, _radarController.playerIco);

            foreach (var o in _interactiveObject)
            {
                if (o is BadBonus badBonus)
                {
                    badBonus.OnCaughtPlayerChange += CaughtPlayer;
                    badBonus.OnCaughtPlayerChange += _displayEndGame.GameOver;

                    badBonus.IsDestroyed += _radarController.RemoveRadarObject;
                    badBonus.IsDestroyed += _interactiveObject.DelExecuteObject;

                    _radarController.RegisterRadarObject(badBonus.gameObject, _radarController.badIco);
                }

                if (o is GoodBonus goodBonus)
                {
                    goodBonus.OnPointChange += AddBonuse;

                    goodBonus.IsDestroyed += _radarController.RemoveRadarObject;
                    goodBonus.IsDestroyed += _interactiveObject.DelExecuteObject;

                    _radarController.RegisterRadarObject(goodBonus.gameObject, _radarController.goodIco);
                }
            }

            _reference.RestartButton.onClick.AddListener(RestartGame);
            _reference.RestartButton.gameObject.SetActive(false);
        }

        private void Update()
        {
            for (var i = 0; i < _interactiveObject.Length; i++)
            {
                var interactiveObject = _interactiveObject[i];

                if (interactiveObject == null)
                {
                    continue;
                }
                try
                {
                    interactiveObject.Execute();
                }
                catch (Exception exc)
                {
                    Debug.Log($"{interactiveObject} выдал багу {exc.Message}");
                }
                finally
                {
                }
            }
        }

        #endregion

        #region Methods 
        
        private void InitializeControllers()
        {
            _cameraController = new CameraController(player.transform, _reference.MainCamera.transform);
            _interactiveObject.AddExecuteObject(_cameraController);

            _miniMapController = new MiniMapMVC(player.transform, _reference.MiniMapCamera);
            _interactiveObject.AddExecuteObject(_miniMapController);

            _radarController = new RadarMVC(player.transform, _reference.RadarTransform);
            _interactiveObject.AddExecuteObject(_radarController);

            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                _inputController = new InputController(player);
                _interactiveObject.AddExecuteObject(_inputController);
            }
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
            Time.timeScale = 1.0f;
        }

        private void CaughtPlayer(string value, Color args)
        {
            _reference.RestartButton.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }

        private void AddBonuse(int value)
        {
            _countBonuses += 1; //value;
            _displayBonuses.Display(_countBonuses);
        }
                
        public void Dispose()
        {
            foreach (var o in _interactiveObject)
            {
                if (o is BadBonus badBonus)
                {
                    badBonus.OnCaughtPlayerChange -= CaughtPlayer;
                    badBonus.OnCaughtPlayerChange -= _displayEndGame.GameOver;
                }

                if (o is GoodBonus goodBonus)
                {
                    goodBonus.OnPointChange -= AddBonuse;
                }
            }
        }

        #endregion
    }
}