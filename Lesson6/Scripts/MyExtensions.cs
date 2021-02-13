using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Geekbrains
{
    public static class MyExtensions
    {
        public static T GetOrAddComponent<T>(this GameObject child) where T : Component
        {
            T result = child.GetComponent<T>() ?? child.gameObject.AddComponent<T>();
            return result;
        }
    }
}
