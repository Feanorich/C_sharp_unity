using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

namespace Geekbrains
{
    public abstract class Player : MonoBehaviour, IDisposable
    {
        public float BaseSpeed = 3.0f;
        public float Speed;
        /// <summary>
        /// список бонусов влияющих на игрока
        /// </summary>
        public List<Bonus> Bonuses;

        private Rigidbody _rigidbody;

        protected void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            Speed = BaseSpeed;
            Bonuses = new List<Bonus>();
        }

        public abstract void Move(float x, float y, float z);

        /// <summary>
        /// Применение влияющих на игрока бонусов, и удаление завершившихся
        /// </summary>
        protected void CheckBonuses()
        {
            Speed = BaseSpeed;
            for (var index = Bonuses.Count - 1; index >= 0; index--)
            {
                if (!Bonuses[index].GetBonus(this))
                {
                    Bonuses.RemoveAt(index);
                    
                }
            }

            Log($"speed: {Speed}");
        }

        public void Dispose()
        {
            for (var index = Bonuses.Count - 1; index >= 0; index--)
            {
                Bonuses[index] = null;
                Bonuses.RemoveAt(index);
            }
            Destroy(gameObject);
            
        }
    }
}
