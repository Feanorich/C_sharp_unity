using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Geekbrains
{
    public sealed class RadarMVC : IExecute
    {
        public Image goodIco;
        public Image badIco;
        public Image playerIco;

        public List<RadarObject> RadObjects = new List<RadarObject>();

        private Transform _playerPos; // Позиция главного героя
        private readonly float _mapScale = 2;
        private Transform _radarTransform;        

        public RadarMVC(Transform player, Transform radarTransform)
        {
            _playerPos = player;
            _radarTransform = radarTransform;

            goodIco = Resources.Load<Image>("MiniMap/RadarGood");
            badIco = Resources.Load<Image>("MiniMap/RadarBad");
            playerIco = Resources.Load<Image>("MiniMap/RadarPlayer");

            
        }
                
        public void RegisterRadarObject(GameObject o, Image i)
        {
            Image image = GameObject.Instantiate(i);
            RadObjects.Add(new RadarObject { Owner = o, Icon = image });
        }

        public void RemoveRadarObject(GameObject o)
        {
            List<RadarObject> newList = new List<RadarObject>();
            foreach (RadarObject t in RadObjects)
            {
                if (t.Owner == o)
                {
                    GameObject.Destroy(t.Icon);
                    continue;
                }
                newList.Add(t);
            }
            RadObjects.RemoveRange(0, RadObjects.Count);
            RadObjects.AddRange(newList);
        }

        private void DrawRadarDots() // Синхронизирует значки на миникарте с реальными объектами
        {
            foreach (RadarObject radObject in RadObjects)
            {
                Vector3 radarPos = (radObject.Owner.transform.position - _playerPos.position);
                Vector3 dotPos = radObject.Owner.transform.position;

                dotPos.y = _playerPos.position.y;

                float distToObject = Vector3.Distance(_playerPos.position, dotPos) * _mapScale;
                float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg - 270;
                radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
                radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);
                radObject.Icon.transform.SetParent(_radarTransform);
                radObject.Icon.transform.position = new Vector3(radarPos.x, radarPos.z, 0) + _radarTransform.position;
                
            }
        }

        public void Execute()
        {         
            DrawRadarDots();
        }
    }    
}