using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
    public class ExerciseCoroutine : MonoBehaviour
    {
        MyClass _my;
        Ticker _tick;

        private float _currentTime = 0.0f;
        private float _deltaTime = 1.0f;

        void Start()
        {
            Debug.Log("Создадим MyClass");
            _my = new MyClass();
            _currentTime = Time.time;
            _tick = new Ticker();
        }

        void Update()
        {            
            _tick.Tick(Time.time, _my._сc.state.ToString());

            _my.ChangeCoroutineState();
        }



    }
}
