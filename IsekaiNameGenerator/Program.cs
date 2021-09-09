using Megumin.Isekai.Name;

using System;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;

namespace IsekaiNameGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = LoadDictNames();

            Generator.InitDict(list);



            while (true)
            {
                var k = Console.ReadKey();

                if (k.Key == ConsoleKey.Enter)
                {
                    var name = Generator.Create(4);
                    Console.WriteLine(name);
                }
                else if (k.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        private static List<string> LoadDictNames()
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory() + "/Dict");
            HashSet<string> nameList = new HashSet<string>();
            foreach (var file in files)
            {
                if (file.EndsWith(".txt"))
                {
                    var names = File.ReadAllLines(file);
                    foreach (var item in names)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            nameList.Add(item);
                        }
                    }
                }
                else
                {
                    try
                    {
                        var f = File.ReadAllText(file);
                        var json = JsonDocument.Parse(f);
                        foreach (var name in json.RootElement.EnumerateArray())
                        {
                            try
                            {
                                nameList.Add(name.GetString());
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(name + "\n" + e);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(file + "\n" + e);
                    }
                }

            }

            return nameList.ToList();
        }
    }
}



