using UnityEngine;

namespace Geekbrains
{
    public sealed class BadBonus : InteractiveObject, IFlay, IRotation
    {
        [SerializeField] private float deceleration = 2;
        [SerializeField] private float decelerationTime = 15;
        private float _lengthFlay;
        private float _speedRotation;

        private void Awake()
        {
            _lengthFlay = Random.Range(1.0f, 5.0f);
            _speedRotation = Random.Range(10.0f, 50.0f);
        }

        protected override void Interaction(Player _player)
        {
            _player.Bonuses.Add(new SpeedBonus(-deceleration, decelerationTime));
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
    }
}