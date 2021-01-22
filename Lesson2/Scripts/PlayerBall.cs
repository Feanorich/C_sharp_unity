using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
    public sealed class PlayerBall : Player
    {
        private void Update()
        {
            Move();
            CheckBonuses();
        }
    }
}