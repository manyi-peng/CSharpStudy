using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskStudy
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Person person = new Person();
            await person.SayAsync();
            person.Calculation();
            await person.CalculationAsync();
            //Console.WriteLine(await person.GetFirstCharactersCountAsync(null, 10000));
            Console.ReadLine();
        }
    }
    public class Person
    {
        //委托的实现
        private void Say()
        {
            Console.WriteLine("task three....");
        }
        public async Task SayAsync()
        {
            await Task.Run(() =>
             {
                 Console.WriteLine("task one....");
             });

            await Task.Run(() =>
              {
                  Console.WriteLine("task two...");
              });
            Action a = Say;
            await Task.Run(a);
        }

        public void Calculation()
        {
            Console.WriteLine("time:{0}", DateTime.Now);
            //Task.Run(() =>
            //{
            //    var n = 0;
            //    for (var i = 0; i < 100000000; i++)
            //    {
            //        n += i;
            //    }
            //    Task.Yield();
            //    Console.WriteLine("Calculation:" + Task.CurrentId.ToString());
            //    Console.WriteLine("Calculation:" + n.ToString());
            //});
            var n = 0;
            for (var i = 0; i < 100000000; i++)
            {
                n += i;
            }
            Console.WriteLine("Calculation:" + n.ToString());
            Console.WriteLine("time2:{0}", DateTime.Now);
            Console.WriteLine("Calculation:method end");
        }

        public async Task CalculationAsync()
        {
            await Task.Run(() =>
             {
                 var n = 0;
                 for (var i = 0; i < 100000000; i++)
                 {
                     n += i;
                 }
                 Console.WriteLine("CalculationAsync:" + Task.CurrentId.ToString());
                 Console.WriteLine("CalculationAsync:" + n.ToString());
             });
            Console.WriteLine("CalculationAsync:Async method end");

        }

        public async Task<string> GetFirstCharactersCountAsync(string url, int count)
        {
            var client = new HttpClient();
            var page = await client.GetStringAsync("http://www.dotnetfoundation.org");
            if (count > page.Length)
            {
                return page;
            }
            else
            {
                return page.Substring(0, count);
            }
        }
    }
}
