using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Geekbrains
{
    public class Exercise4 : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                {"four",4 },
                {"two",2 },
                {"one",1 },
                {"three",3 },
            };

            //исходный код
            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
            foreach (var pair in d)
            {
                Debug.Log($"(исходник) {pair.Key} - {pair.Value}");
            }

            //развернем сортировку
            var d1 = from u in dict
                     orderby u.Value ascending 
                     select u;

            foreach (var pair in d1)
            {
                Debug.Log($"(развернули) {pair.Key} - {pair.Value}");
            }

            //свернем через лямбду
            var d2 = dict.OrderBy( pair => pair.Value );
            foreach (var pair in d2)
            {
                Debug.Log($"(свернули) {pair.Key} - {pair.Value}");
            }
        }

        
    }
}
