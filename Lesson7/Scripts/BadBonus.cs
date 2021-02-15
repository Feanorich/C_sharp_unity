
using System;
using UnityEngine;
using static UnityEngine.Random;

namespace Geekbrains
{
    public sealed class BadBonus : InteractiveObject, IFlay, IRotation
    {
        #region Fields
        
        [SerializeField] private bool _isAllowScaling;
        
        public Color _color;
        public event Action<string, Color> OnCaughtPlayerChange = delegate (string str, Color color) { };
        private float _lengthFlay;
        private float _speedRotation;

        #endregion

        #region UnityMethods 

        private void Awake()
        {
            _lengthFlay = Range(2.0f, 5.0f);
            _speedRotation = Range(10.0f, 50.0f);
            _color = GetComponent<Renderer>().material.color;
        }

        #endregion

        #region Methods 
        
        protected override void Interaction(Player _player)
        {
            OnCaughtPlayerChange.Invoke(gameObject.name, _color);
        }

        public override void Execute()
        {
            if (!IsInteractable) { return; }
            Flay();
            Rotation();
        }

        public void Flay()
        {
            transform.localPosition = new Vector3(transform.localPosition.x,
                Mathf.PingPong(Time.time, _lengthFlay),
                transform.localPosition.z);
        }

        public void Rotation()
        {
            transform.Rotate(Vector3.up * (Time.deltaTime * _speedRotation), Space.World);
        }

        #endregion

        private void OnDrawGizmos()
        {
            Gizmos.DrawIcon(transform.position.Shift(0.0f, 0.5f, 0.0f), "bad.png", _isAllowScaling);
        }
        private void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            Gizmos.DrawIcon(transform.position.Shift(0.0f, 0.6f, 0.0f), "good.png",
                _isAllowScaling, Color.red);
#endif
        }
    }
}
