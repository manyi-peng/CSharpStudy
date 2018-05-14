using System;
using System.Collections.Generic;

namespace IntExtension
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var str = "肯德基";
            Console.WriteLine(str.ToInt());

            var a = "a";
            var b = "a";
            Console.WriteLine("a Equals b :{0}", a.Equals(b));
            //字符串是特殊类型，所以值相同的字符串a,b的内存地址相同
            Console.WriteLine("a hashcode {0}", a.GetHashCode());
            Console.WriteLine("b hashcode {0}", b.GetHashCode());
            var p1 = new Person { name = "a", age = 20 };
            var p2 = new Person { name = "a", age = 20 };
            Console.WriteLine("p1 Equals p2:{0}", p1.Equals(p2));
            //HashCode返回的是对象的内存地址
            Console.WriteLine("p1 hashcode {0}", p1.GetHashCode());
            Console.WriteLine("p2 hashcode {0}", p2.GetHashCode());
            Dictionary<object, int> dic = new Dictionary<object, int>();
            dic.Add(p1, 10);
            dic.Add(p2, 10);
            Console.WriteLine(dic[p1]);
            Console.WriteLine(dic[p2]);
            
            //C#链式写法
            HtmlHelp.MyAttachment().SetWidth(10).SetName("pic").Write();

            Console.ReadLine();
        }
    }

    public class Person
    {
        public override bool Equals(object obj)
        {
            var p = obj as Person;
            return (p.name.Equals(this.name) && p.age == this.age);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string name { get; set; }
        public int age { get; set; }
    }

    public class Attachment
    {

        private int Width { get; set; }
        private string Name { get; set; }

        public Attachment SetWidth(int width)
        {
            this.Width = width;
            return this;
        }

        public Attachment SetName(string name)
        {
            this.Name = name;
            return this;
        }

        public void Write()
        {
            Console.WriteLine("Attachment Name is {0} and Width is {1}", this.Name, this.Width);
        }

    }

    public class HtmlHelp
    {
        public static Attachment MyAttachment()
        {
            return new Attachment();
        }
    }

    public static class Extend
    {
        public static int ToInt(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var result = 0;
                int.TryParse(value, out result);
                return result;
            }
            else
            {
                return 0;
            }
        }
    }


}
