using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Geekbrains
{
    public abstract class InteractiveObject : MonoBehaviour, IInteractable, IDisposable
    {
        public bool IsInteractable { get; } = true;

        protected abstract void Interaction(Player _player);

        private void Start()
        {
            Action();
        }

        public void Action()
        {
            if (TryGetComponent(out Renderer renderer))
            {
                renderer.material.color = Random.ColorHSV();
            }
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

        public void Dispose()
        {
            Destroy(gameObject); 
        }
    }
}