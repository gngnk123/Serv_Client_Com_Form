using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace RecInfoPort
{
    public partial class Form1 : Form
    {
        TcpClient ClientSocket = new TcpClient();
        NetworkStream ServerStream = default(NetworkStream);
        String Readdata = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientSocket.Connect(textBox1.Text, Int32.Parse(textBox2.Text));

            Thread CtThread = new Thread(GetMessage);
            CtThread.Start();



        }

        private void GetMessage()
        {
            string ReturnData;

            while (true)
            {
                ServerStream = ClientSocket.GetStream();
                var Buffsize = ClientSocket.ReceiveBufferSize;
                byte[] InStream = new byte[Buffsize];

                ServerStream.Read(InStream, 0, Buffsize);

                ReturnData = System.Text.Encoding.ASCII.GetString(InStream);

                Readdata = ReturnData;
                msg();


            }
        }
        private void msg()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(msg));
            }
            else
            {
                textBox4.Text = Readdata;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] outstream = Encoding.ASCII.GetBytes(textBox3.Text);

            ServerStream.Write(outstream, 0, outstream.Length);
            ServerStream.Flush();
        }
    }
}