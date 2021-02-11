using UnityEngine;

namespace Geekbrains
{
    public sealed class InputController : IExecute
    {
        private readonly Player _playerBase;

        public InputController(Player player)
        {
            _playerBase = player;
        }

        public void Execute()
        {
            _playerBase.Move(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        }
    }
}
