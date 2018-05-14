using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeServer
{
    class Program
    {
        private static NamedPipeServerStream pipeServer;
        private static string pipeName = "testpipe";
        static void Main(string[] args)
        {
            WaitData();
        }

        private static void WaitData()
        {
            try
            {
                pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 1);
                pipeServer.WaitForConnection();
                pipeServer.ReadMode = PipeTransmissionMode.Byte;
                using (StreamReader sr = new StreamReader(pipeServer))
                {
                    string con = sr.ReadToEnd();
                    Console.WriteLine(con);
                    var rs = "我是返回数据";
                    var b = Encoding.Default.GetBytes(rs);
                    pipeServer.Write(b, 0, b.Length);
                    Console.ReadLine();
                }
                pipeServer.Disconnect();
                pipeServer.Dispose();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
