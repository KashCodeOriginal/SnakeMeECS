using UnityEngine;

namespace OOP.Data
{
    public static class Extensions
    {
        public static T ToDeserialized<T>(this string json) => JsonUtility.FromJson<T>(json);

        public static string ToJson(this object obj) => JsonUtility.ToJson(obj);
    }
}