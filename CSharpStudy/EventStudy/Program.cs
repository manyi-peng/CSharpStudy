using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            People.StartComputer();
            Console.Read();
        }

        /// <summary>
        /// 发布器
        /// </summary>
        public class Computer
        {
            public int tick { get; set; }
            public delegate void RunHandler(string name, DateTime time);
            //定义时间
            public event RunHandler Click;

            public event EventHandler<People> dbClick;

            public void OnClick(string name, DateTime time)
            {
                if (Click != null)
                {
                    Click(name, time);
                    Console.WriteLine("这是调用发布者默认的时间处理程序");
                }
                else
                {
                    Console.WriteLine("event not fire!");
                }

            }

            public void OndbClick()
            {
                if (dbClick == null)
                {
                    Console.WriteLine("调用对象默认的处理逻辑");
                }
                else
                {
                    dbClick(this, null);
                    Console.WriteLine("调用注册的事件");
                }
            }

        }

        /// <summary>
        /// 订阅器
        /// </summary>
        public class People
        {
            public static void StartComputer()
            {
                Computer computer = new Computer();
                //事件注册
                computer.Click += Computer_Start;
                computer.Click += Computer_Start1;
                computer.OnClick("people", DateTime.Now);
                computer.dbClick += Computer_dbClick;
                computer.dbClick += Computer_dbClick1;
                computer.OndbClick();
            }

            private static void Computer_dbClick1(object sender, People e)
            {
                throw new NotImplementedException();
            }

            private static void Computer_dbClick(object sender, People e)
            {
                throw new NotImplementedException();
            }

            private static void Computer_Start1(string name, DateTime time)
            {
                Console.WriteLine("这是调用事件订阅者自定义的事件处理程序2");
            }

            private static void Computer_Start(string name, DateTime time)
            {
                Console.WriteLine("这是调用事件订阅者自定义的事件处理程序");
            }
        }
    }
}
