using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.IO;

namespace PipeClientB
{
    class Program
    {
        static void Main(string[] args)
        {
            SendData();
        }
        private static void SendData()
        {
            try
            {
                using (NamedPipeClientStream pipeClient = new NamedPipeClientStream("localhost", "testpipe", PipeDirection.InOut, PipeOptions.None, System.Security.Principal.TokenImpersonationLevel.None))
                {
                    pipeClient.Connect();
                    using (StreamWriter sw = new StreamWriter(pipeClient))
                    {
                        string str = Console.ReadLine();
                        sw.WriteLine(str);
                        sw.Flush();
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
