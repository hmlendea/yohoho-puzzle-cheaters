using System;
using System.Collections.Generic;

namespace YohohoPuzzleCheaters.Infrastructure.Extensions
{
    /// <summary>
    /// List extensions.
    /// </summary>
    public static class ListExtensions
    {
        public static List<List<T>> SplitInChunksBySize<T>(this List<T> me, int itemsPerChunk)
        {
            var list = new List<List<T>>();

            for (int i = 0; i < me.Count; i += itemsPerChunk)
            {
                list.Add(me.GetRange(i, Math.Min(itemsPerChunk, me.Count - i)));
            }

            return list;
        }

        public static List<List<T>> SplitInChunksByCount<T>(this List<T> me, int chunksCount)
        {
            return me.SplitInChunksBySize(me.Count / chunksCount);
        }
    }
}
