using UnityEngine;
using UnityEngine.UI;

namespace Geekbrains
{
    public sealed class RadarObj : MonoBehaviour
    {
        [SerializeField] private Image _ico;

        public void SetIco(string _path)
        {
            _ico = Resources.Load<Image>(_path);
        }

        public void OnValidate()
        {
            _ico = Resources.Load<Image>("MiniMap/RadarObject");
        }

        private void OnDisable()
        {
            Radar.RemoveRadarObject(gameObject);
        }

        public void OnEnable()
        {
            Radar.RegisterRadarObject(gameObject, _ico);
        }
    }
}

