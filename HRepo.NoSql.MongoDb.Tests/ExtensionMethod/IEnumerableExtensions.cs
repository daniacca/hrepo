using System.Collections.Generic;

namespace HRepo.NoSql.MongoDb.Tests.ExtensionMethod
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Iterate over a generic sequence of data by chunks of given size at a time
        /// </summary>
        /// <typeparam name="T">the type of element of the sequence</typeparam>
        /// <param name="sequence">the sequence to iterate</param>
        /// <param name="size">the size of each chunk of every iteration</param>
        /// <returns>a chunk of size element on every iteration</returns>
        public static IEnumerable<IList<T>> ChunksOf<T>(this IEnumerable<T> sequence, int size)
        {
            List<T> chunk = new(size);

            foreach (T element in sequence)
            {
                chunk.Add(element);
                if (chunk.Count == size)
                {
                    yield return chunk;
                    chunk = new List<T>(size);
                }
            }
        }
    }
}
