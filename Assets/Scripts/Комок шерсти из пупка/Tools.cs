using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace LibraryForGames
{
    public static class Tools
    {
        public static void LogColor(string message, Color color)
        {
            Debug.Log(string.Format("<color=#{0:X2}{1:X2}{2:X2}>{3}</color>", (byte)(color.r * 255f), (byte)(color.g * 255f), (byte)(color.b * 255f), message));
        }

        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            int index = Random.Range(0, enumerable.Count());
            return enumerable.ElementAt(index);
        }  
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.OrderBy(n => Random.Range(int.MinValue,int.MaxValue));            
        }          
        

        public static bool IsAlmostEquals(this float a, float b, float delta = 0.01f) => Mathf.Abs(a - b) < delta;

        public static bool IsAlmostEquals(this Vector2 a, Vector2 b, float delta = 0.01f)
        {
            return a.x.IsAlmostEquals(b.x, delta) && a.y.IsAlmostEquals(b.y, delta);
        }

        public static bool IsAlmostEquals(this Vector3 a, Vector3 b, float delta = 0.01f)
        {
            return a.x.IsAlmostEquals(b.x, delta) && a.y.IsAlmostEquals(b.y, delta) && a.z.IsAlmostEquals(b.z, delta);
        }

    }
}
