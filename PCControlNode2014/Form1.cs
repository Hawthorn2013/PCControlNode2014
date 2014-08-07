using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace PCControlNode2014
{
    public partial class Form1 : Form
    {
        System.Net.Sockets.UdpClient udpClient1;
        public Form1()
        {
            udpClient1 = new System.Net.Sockets.UdpClient();
            InitializeComponent();
        }

        private void btnSendTest_Click(object sender, EventArgs e)
        {
            byte[] testBytes = { 0x11, 0x22 };
            udpClient1.Send(testBytes, testBytes.Length, new System.Net.IPEndPoint(IPAddress.Parse("192.168.7.255"), 4567));
        }
    }
}
