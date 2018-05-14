using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.IO;
using System.Security.Principal;

namespace PipeClient
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
                NamedPipeClientStream pipeClient = new NamedPipeClientStream("localhost", "testpipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.None);
                pipeClient.Connect();
                using (StreamWriter sw = new StreamWriter(pipeClient))
                {
                    string str = Console.ReadLine();
                    sw.WriteLine(str);
                    
                    //var len = pipeClient.ReadByte();
                    //Byte[] b = new Byte[len];
                    //pipeClient.Read(b, 0, len);
                    //var response = Encoding.Default.GetString(b);
                    //Console.WriteLine(response);
                    sw.Flush();
                    Console.ReadLine();
                }
                pipeClient.Dispose();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
