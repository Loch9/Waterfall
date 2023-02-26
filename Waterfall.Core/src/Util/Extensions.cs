using System;
using System.Collections.Generic;

namespace Waterfall.Core.Util
{
    public static class Extensions
    {
        public static List<T> AddElement<T>(this List<T> list, T element)
        {
            list.Add(element);
            return list;
        }
    }
}
