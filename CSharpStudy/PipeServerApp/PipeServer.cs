using System;
using System.Windows.Forms;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;
using System.Text;

namespace PipeServerApp
{
    public partial class PipeServer : Form
    {
        private NamedPipeServerStream _pipeServer;
        private const string _pipeName = "admin";
        private Encoding encoding = Encoding.UTF8;
        public PipeServer()
        {
            InitializeComponent();
            _pipeServer = new NamedPipeServerStream(_pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous | PipeOptions.WriteThrough);
            //AsyncConnectedCallback 委托
            //将_pipeServer保存到state中
            _pipeServer.BeginWaitForConnection(AsyncConnectedCallback, _pipeServer);
        }
        //委托的实现
        private void AsyncConnectedCallback(IAsyncResult ir)
        {
            try
            {
                var pipeServer = (NamedPipeServerStream)ir.AsyncState;
                pipeServer.EndWaitForConnection(ir);
                var count = pipeServer.ReadByte();
                byte[] data = new byte[count];
                pipeServer.Read(data, 0, count);
                string con = encoding.GetString(data);
                txtReceive.Invoke(new Action(() => { txtReceive.Text = con; }));
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void PipeServer_Load(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            OnSend();
        }

        private void OnSend()
        {
            if (_pipeServer.IsConnected)
            {
                try
                {
                    string message = txtSend.Text.Trim();
                    if (string.IsNullOrEmpty(message))
                    {
                        MessageBox.Show("输入发送的数据");
                        txtSend.Focus();
                        return;
                    }
                    byte[] data = encoding.GetBytes(message);
                    _pipeServer.Write(data, 0, data.Length);
                    _pipeServer.Flush();
                    //_pipeServer.WaitForPipeDrain();
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
