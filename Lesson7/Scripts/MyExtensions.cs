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
        /// <summary>
        /// Сдвиг точки по осям
        /// </summary>
        /// <returns></returns>
        public static Vector3 Shift(this Vector3 V, float X = 0.0f, float Y = 0.0f, float Z = 0.0f)
        {
            V.x += X;
            V.y += Y;
            V.z += Z;
            return V;
        }
    }
}
