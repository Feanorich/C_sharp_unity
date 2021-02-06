using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Geekbrains
{
    public static class Exercise2Extensions
    {
        /// <summary>
        /// количество символов в строке
        /// </summary>
        /// <param name="_str">исходнач строка</param>
        /// <param name="_ch">искомый символ</param>
        /// <returns></returns>
        public static int NumberOfCharacters(this string _str, char _ch)
        {
            
            var _counts = from c in _str                          
                          where c == _ch
                          select c;

            return _counts.Count();
        }
    }
}
