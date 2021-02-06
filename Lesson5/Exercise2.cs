using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Geekbrains
{
    public class Exercise2 : MonoBehaviour
    {
        public string TestString = "quod licet iovi, non licet bovi";
        public char SearchChar = 'o';

        // Start is called before the first frame update
        void Start()
        {
            int count = TestString.NumberOfCharacters(SearchChar);
            Debug.Log($"В строке:\n {TestString} \n Символов ({SearchChar}) ровно {count}");
            Debug.Log($"Символов ({SearchChar}) ровно {count}");

        }


    }
}

