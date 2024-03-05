using System.Diagnostics;

namespace OtusDZ_7
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string path = "C:\\Users\\nikgl\\source\\repos\\OtusDZ_7\\OtusDZ_7\\test10Mb.db";
            string path2 = "C:\\Users\\nikgl\\source\\repos\\OtusDZ_7\\OtusDZ_7\\";
            Stopwatch sw = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();

            List<Task> list = new List<Task> { ReadToFile(path), ReadToFile(path), ReadToFile(path) };
            sw.Start();
            Task.WaitAll(list.ToArray());
            sw.Stop();
            Console.WriteLine(sw.Elapsed.TotalMilliseconds);

            sw2.Start();
            await ReadToPath(path2);
            sw2.Stop();
            Console.WriteLine(sw2.Elapsed.TotalMilliseconds);

        }

        /// <summary>
        /// Подсчитывает колличество пробелов из файла
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        static async Task ReadToFile(string path)
        {
             await Task.Run(() =>
            {
                ParseFile(path);
                Task.Delay(1000).Wait();
            });
        }
        
        /// <summary>
        /// Подсчитывает колличество пробелов каждого файла находящегося в директории
        /// </summary>
        /// <param name="path">Путь к директории</param>
        /// <returns></returns>
        static async Task ReadToPath(string path)
        {   
            List<Task>tasks = new List<Task>();
            
            foreach (var item in Directory.GetFiles(path))
            {
                tasks.Add(Task.Run(()=>ParseFile(item)));
            }
            await Task.WhenAll(tasks.ToArray());
        }

        static void ParseFile(string path)
        {
            //int count = sr.ReadToEnd().Count(Char.IsWhiteSpace);
            int count = File.ReadAllText(path).Count(Char.IsWhiteSpace);
            Console.WriteLine($"Поток № {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"Кол-во пробелов: {count}");
        }
    }
}
