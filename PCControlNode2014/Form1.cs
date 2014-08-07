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
using System.Threading;
using System.Collections.Concurrent;

namespace PCControlNode2014
{
    public partial class Form1 : Form
    {
        UdpClient udpClient1;
        Thread threadReceiveData;
        Thread threadTestOutPut;
        Thread threadSolveData;
        ConcurrentQueue<Byte> receiveDataQueue;
        ConcurrentQueue<List<Byte>> receiveCmdQueue;
        delegate void tbDelegate(TextBox textBox, Byte appendData);
        Byte srcNetAddress = 0x07;
        Byte desNetAddress = 0x0A;
        const int FrameMaxLength = 32;

        public Form1()
        {
            InitializeComponent();

            receiveDataQueue = new System.Collections.Concurrent.ConcurrentQueue<Byte>();

            receiveCmdQueue = new ConcurrentQueue<List<Byte>>();
            udpClient1 = new UdpClient(4567);

            threadReceiveData = new Thread(new ThreadStart(ReceiveThread));
            threadReceiveData.IsBackground = true;
            threadReceiveData.Start();

#if true
            threadTestOutPut = new Thread(() => TestOutput(tbReceiveDataTest, receiveDataQueue));
            threadTestOutPut.IsBackground = true;
            threadTestOutPut.Start();
#endif

#if false
            threadSolveData = new Thread(() => SolveData(receiveDataQueue, receiveCmdQueue));
            threadSolveData.IsBackground = true;
            threadSolveData.Start();
#endif
        }

        private void btnSendTest_Click(object sender, EventArgs e)
        {
            byte[] testBytes = { 0x11, 0x22 };
            udpClient1.Send(testBytes, testBytes.Length, new System.Net.IPEndPoint(IPAddress.Parse("192.168.7.255"), 4567));
        }

        private void ReceiveThread()
        {
            IPEndPoint iPEndPoint_1 = new IPEndPoint(IPAddress.Parse("192.168.7.255"), 4567);
            IPEndPoint iPEndPoint_2 = new IPEndPoint(IPAddress.Parse("192.168.7.0"), 4567);
            while (true)
            {
                byte[] Data1 = udpClient1.Receive(ref iPEndPoint_1);
                foreach (Byte data in Data1)
                {
                    receiveDataQueue.Enqueue(data);
                }
                Thread.Sleep(50);
            }
        }

        void AppendTextbox(TextBox textbox, Byte appendData)
        {
            textbox.Text += appendData.ToString("x2") + "\t";
            textbox.SelectionStart = textbox.TextLength;
            textbox.ScrollToCaret();
        }

        void TestOutput(TextBox textbox, ConcurrentQueue<Byte> concurrentQueue)
        {
            while (true)
            {
                Byte tmpByte;
                while (concurrentQueue.TryDequeue(out tmpByte))
                {
                    Invoke(new tbDelegate(AppendTextbox), new object[] { textbox, tmpByte });
                }
                Thread.Sleep(50);
            }
        }

        void SolveData(ConcurrentQueue<Byte> src, ConcurrentQueue<List<Byte>> des)
        {
            //[不足]没有预定义表大小
            int frame_site_cnt = 0;
            List<Byte> frame = new List<byte>();
            while (true)
            {
                Byte tmpByte;
                while (src.TryDequeue(out tmpByte))
                {
                    if (0 == frame_site_cnt)	//接收帧头
                    {
                        if (0xAA == tmpByte)
                        {
                            frame[frame_site_cnt++] = tmpByte;
                        }
                    }
                    else if (1 == frame_site_cnt)	//接收帧头
                    {
                        if (0xBB == tmpByte)
                        {
                            frame[frame_site_cnt++] = tmpByte;
                        }
                        else
                        {
                            frame_site_cnt = 0;
                        }
                    }
                    else if (2 == frame_site_cnt)   //接收源地址
                    {
                        if (true)
                        {
                            frame[frame_site_cnt++] = tmpByte;
                        }
                    }
                    else if (3 == frame_site_cnt)   //接收目的地址
                    {
                        if (true)
                        {
                            frame[frame_site_cnt++] = tmpByte;
                        }
                    }
                    else if (4 == frame_site_cnt)   //接收长度
                    {
                        if (tmpByte + 6 < FrameMaxLength)
                        {
                            frame[frame_site_cnt++] = tmpByte;
                        }
                        else
                        {
                            frame_site_cnt = 0;
                        }
                    }
                    else if (frame_site_cnt > 4 && frame_site_cnt <= frame[4]+4)   //接收数据区
                    {
                        frame[frame_site_cnt++] = tmpByte;
                    }
                    else if (frame[4] + 4 + 1 == frame_site_cnt)	//接收校验字节	
                    {
                        frame[frame_site_cnt++] = tmpByte;
                        List<Byte> checkData = new List<byte>(tmpByte);
                        checkData.RemoveAt(checkData.Count - 1);
                        checkData.RemoveRange(0, 2);
                        if (CheckSum(checkData) == tmpByte)
                        {
                            des.Enqueue(frame);
                            frame_site_cnt = 0;
                        }
                        else
                        {
                            frame_site_cnt = 0;
                        }
                    }
                }
                Thread.Sleep(50);
            }
        }

        byte CheckSum(List<byte> databuf)
        {
            //[不足]没有使用常量
            byte sum = 0x00;
            foreach (byte data in databuf)
            {
                sum ^= data;
            }
            return sum;
        }
    }
}
