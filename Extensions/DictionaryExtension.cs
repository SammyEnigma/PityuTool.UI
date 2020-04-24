using System.Collections.Generic;

namespace PityuTool.UI.Extensions
{
    internal static class DictionaryExtension
    {

        public static bool IsExistKeyAndValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TValue value)
        {
            foreach (TKey key in dictionary.Keys)
            {
                TValue temp = dictionary[key];
                if (temp.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool ExistCheckByKeyAndValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            foreach (TKey ky in dictionary.Keys)
            {
                TValue temp = dictionary[ky];
                if (temp.Equals(value) && dictionary.ContainsKey(key))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
