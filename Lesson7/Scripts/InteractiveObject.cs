using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Geekbrains
{
    public abstract class InteractiveObject : MonoBehaviour, IInteractable, IDisposable, IExecute
    {
        public bool IsInteractable { get; } = true;

        public event Action<GameObject> IsDestroyed = delegate (GameObject ex) { };

        #region IInteractable
        protected abstract void Interaction(Player _player);
        #endregion

        #region IExecute
        /// <summary>
        /// Выполнение объектом действия
        /// </summary>
        public abstract void Execute();
        #endregion

        #region UnityMethods

        private void Start()
        {
            Action();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsInteractable || !other.CompareTag("Player"))
            {
                return;
            }
            Interaction(other.GetComponent<Player>());

            Dispose();
        }

        #endregion
        public void Action()
        {
            if (TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = Random.ColorHSV();
            }
        }

        #region IDisposable
        public void Dispose()
        {
            IsDestroyed.Invoke(gameObject);
            Destroy(gameObject); 
        }

        #endregion
    }
}