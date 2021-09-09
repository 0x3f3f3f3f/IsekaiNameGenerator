using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace Megumin.Isekai.Name
{
    internal static class IEnumerableExtension
    {
        static Random random = new Random(DateTimeOffset.UtcNow.GetHashCode());
        public static T Random<T>(this ICollection<T> enumerable)
        {
            if (enumerable.Count > 0)
            {
                return enumerable.ElementAt(random.Next(0, enumerable.Count));
            }
            return default;
        }

        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            var count = enumerable.Count();
            if (count > 0)
            {
                return enumerable.ElementAt(random.Next(0, count));
            }
            return default;
        }
    }


    public class Generator
    {
        static Dictionary<int, Generator> generators = new Dictionary<int, Generator>();
        static Dictionary<int, List<string>> toneCharactor = new Dictionary<int, List<string>>()
        {
            { 1,new List<string>()},
            { 2,new List<string>()},
            { 3,new List<string>()},
            { 4,new List<string>()},
            { 5,new List<string>()},
        };

        /// <summary>
        /// 声调模式
        /// </summary>
        List<string> ToneMode = new List<string>();

        /// <summary>
        /// 根据字典初始化生成器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nameDict"></param>
        public static void InitDict<T>(T nameDict)
            where T : IEnumerable<string>
        {


            foreach (var name in nameDict)
            {
                if (name.Length > 0 && name.Length < 10)
                {
                    if (generators.TryGetValue(name.Length, out var generator))
                    {

                    }
                    else
                    {
                        generator = new Generator(name.Length);
                        generators.Add(name.Length, generator);
                    }

                    generator.InitName(name);
                }
            }
        }

        public static string Create(int nameLength)
        {
            if (generators.TryGetValue(nameLength, out var generator))
            {
                return generator.Create();
            }

            return "No Generator";
        }



        Random random = new Random(DateTimeOffset.UtcNow.GetHashCode());
        internal protected Generator(int length)
        {
            Length = length;
        }

        public int Length { get; }



        public string Create()
        {
            if (ToneMode.Count > 0)
            {
                return "No Tone Mode";
            }
            var mode = ToneMode.Random();

            var result = "";
            foreach (var item in mode)
            {
                int tone = int.Parse(item.ToString());
                var cdict = toneCharactor[tone];
                result += cdict.Random();
            }

            return result;
        }

        private void InitName(string name)
        {
            if (name.Length != Length)
            {
                throw new ArgumentException();
            }

        }
    }
}



