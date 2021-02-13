﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Geekbrains
{
    public abstract class InteractiveObject : MonoBehaviour, IInteractable, IDisposable, IExecute
    {
        public bool IsInteractable { get; } = true;

        public event Action<GameObject> IsDestroyed = delegate (GameObject ex) { };
        protected abstract void Interaction(Player _player);
        /// <summary>
        /// Выполнение объектом действия
        /// </summary>
        public abstract void Execute();

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
            IsDestroyed.Invoke(gameObject);
            Destroy(gameObject); 
        }
    }
}