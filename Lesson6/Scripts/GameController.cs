using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Geekbrains
{
    public sealed class GameController : MonoBehaviour, IDisposable
    {
        public PlayerType PlayerType = PlayerType.Ball;
        private ListExecuteObject _interactiveObject;
        private DisplayEndGame _displayEndGame;
        private DisplayBonuses _displayBonuses;
        private CameraController _cameraController;
        private MiniMapMVC _miniMapController;
        private RadarMVC _radarController;
        private InputController _inputController;
        private int _countBonuses;
        private Reference _reference;

        private void Awake()
        {
            _interactiveObject = new ListExecuteObject();

            _reference = new Reference();

            Player player = null;
            if (PlayerType == PlayerType.Ball)
            {
                player = _reference.PlayerBall;
            }
            

            _cameraController = new CameraController(player.transform, _reference.MainCamera.transform);
            _interactiveObject.AddExecuteObject(_cameraController);

            _miniMapController = new MiniMapMVC(player.transform, _reference.MiniMapCamera);
            _interactiveObject.AddExecuteObject(_miniMapController);

            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                _inputController = new InputController(player);
                _interactiveObject.AddExecuteObject(_inputController);
            }

            _displayEndGame = new DisplayEndGame(_reference.EndGame);
            _displayBonuses = new DisplayBonuses(_reference.Bonuse);
            
            _radarController = new RadarMVC(player.transform, _reference.RadarTransform);

            _radarController.RegisterRadarObject(player.gameObject, _radarController.playerIco);

            foreach (var o in _interactiveObject)
            {
                //Debug.Log($"{o.GetType()} тип О {o}");
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

            _interactiveObject.AddExecuteObject(_radarController);

            _reference.RestartButton.onClick.AddListener(RestartGame);
            _reference.RestartButton.gameObject.SetActive(false);
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
                    // когда подбираем бонус - он удаляется с карты.
                    // эта строчка выдает, что interactiveObject = null 
                    // Но почему тогда он проходит проверку предыдущим условием? и не срабатывает continue? 
                    Debug.Log($"{interactiveObject} выдал багу {exc.Message}");
                    
                }
                finally
                {
                }

                
                
            }
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
    }
}