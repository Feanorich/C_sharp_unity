using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Geekbrains
{
    internal class MyClass
    {
        public CoroutineController _сc;

        public MyClass()
        {
            (float time, int episodes) _season = (5, 3);
            
            Timer(_season).StartCoroutine(out _сc);     
        }
        
        public void ChangeCoroutineState()
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                Debug.Log("P");
                _сc.PauseResume();
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                Debug.Log("S");
                _сc.Stop();
            }
        }

        IEnumerator Timer((float time, int episodes) season)
        {
            for (int i = 1; i <= season.episodes; i++)
            {
                Debug.Log($"__________эпизод {i} - {Time.time}");
                yield return new WaitForSeconds(season.time);
            }                        
            
            Debug.Log($"_______________конец - {Time.time}");
        }
    }
    
    
}
