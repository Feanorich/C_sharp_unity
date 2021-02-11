using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
    public sealed class PlayerBall : Player
    {
        private Rigidbody _rigidbody;

        private void Start() 
        {
            base.Start();

            _rigidbody = GetComponent<Rigidbody>();
        }

        public override void Move(float x, float y, float z)
        {
            _rigidbody.AddForce(new Vector3(x, y, z) * Speed);
        }

        //private void Update()
        //{
        //    CheckBonuses();
        //}
    }
}