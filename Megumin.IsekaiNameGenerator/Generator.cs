using Megumin.IsekaiNameGenerator;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

using ToolGood.Words.Pinyin;

namespace Megumin.Isekai.Name
{
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
                return generator.CoreCreate();
            }

            return "No Generator";
        }

        public static string Create(string mode)
        {
            if (generators.TryGetValue(mode.Length, out var generator))
            {
                return generator.CoreCreate(mode);
            }

            return "No Generator";
        }

        internal protected Generator(int length)
        {
            Length = length;
        }

        public int Length { get; }

        protected string CoreCreate(string mode = null)
        {
            if (ToneMode.Count == 0)
            {
                return "No Tone Mode";
            }

            if (string.IsNullOrEmpty(mode))
            {
                mode = ToneMode.Random();
            }
            else
            {
                if (mode.Length != Length)
                {
                    Console.WriteLine("warning: input mode length error");
                }
            }

            var result = "";
            try
            {
                foreach (var item in mode)
                {
                    int tone = int.Parse(item.ToString());
                    var cdict = toneCharactor[tone];
                    result += cdict.Random();
                }
            }
            catch (Exception e)
            {
                result = e.ToString();
            }

            return result;
        }

        private void InitName(string name)
        {
            if (name.Length != Length)
            {
                throw new ArgumentException();
            }

            var list = WordsHelper.GetPinyinList(name, true);
            string mode = "";
            for (int i = 0; i < list.Length; i++)
            {
                var tone = ToneHelper.GetTone(list[i]);
                toneCharactor[tone].Add(name[i].ToString());
                mode += tone;
            }
            ToneMode.Add(mode);
        }
    }
}



