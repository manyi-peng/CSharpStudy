using System;
using System.Windows.Forms;
using System.IO.Pipes;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace PipeFormApp
{
    public partial class PipeClientForm : Form
    {
        private NamedPipeClientStream _pipe;
        private const string _pipeName = "admin";
        private const string _pipeServer = ".";//"192.168.26.63";
        private bool _starting = false;
        private Encoding encoding = Encoding.UTF8;
        public PipeClientForm()
        {
            InitializeComponent();
        }

        private void OnConnect()
        {
            if (_starting)
                return;
            try
            {
                _pipe = new NamedPipeClientStream(_pipeServer, _pipeName, PipeDirection.InOut, PipeOptions.Asynchronous | PipeOptions.WriteThrough);
                _pipe.Connect();
                _pipe.ReadMode = PipeTransmissionMode.Byte;
                string message = "Connected!";
                byte[] data = encoding.GetBytes(message);
                _pipe.BeginWrite(data, 0, data.Length, PipeWriteCallback, _pipe);
                _starting = true;

            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PipeWriteCallback(IAsyncResult ir)
        {
            var pipe = (NamedPipeClientStream)ir.AsyncState;

            pipe.EndWrite(ir);
            pipe.Flush();
            pipe.WaitForPipeDrain();
            while (true)
            {
                var count = pipe.ReadByte();
                byte[] data = new byte[count];
                pipe.Read(data, 0, count);
                string txt = encoding.GetString(data);
                txtReceive.Invoke(new Action(() => { txtReceive.Text = txt; }));
                //pipe.WaitForPipeDrain();
            }


        }
        /// <summary>
        /// 连接服务端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            OnConnect();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            OnReceive();
        }

        private void OnReceive()
        {
            try
            {
                if (_pipe.IsConnected)
                {
                    var count = _pipe.ReadByte();
                    byte[] data = new byte[count];
                    _pipe.Read(data, 0, count);
                    var message = encoding.GetString(data);
                    txtReceive.Text = message;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
