using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Geekbrains
{
    public class Exercise3 : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Dictionary<int, int> count = new Dictionary<int, int>();

            List<int> col = new List<int> { 1, 2, 3, 4, 8, 6, 7, 4, 9, 10, 11, 4, 13, 14, 8 };
            col.Sort();

            foreach (int i in col)
            {
                var _counts = from c in col
                              where c % 2 == 0
                              where c == i
                              select c;

                if (_counts.Count() > 0) 
                    count[i] = _counts.Count();
            }

            foreach (var c in count)
            {
                Debug.Log($"{c.Key} в количестве: {c.Value}");
            }
        }


    }
}

