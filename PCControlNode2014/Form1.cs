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
        Thread threadSolveData;
        ConcurrentQueue<Byte> receiveDataQueue;
        ConcurrentQueue<List<Byte>> receiveCmdQueue;
        delegate void tbDelegate_1(TextBox textBox, Byte appendData);
        delegate void tbDelegate_2(TextBox textBox, List<Byte> appendData);
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

            threadSolveData = new Thread(() => SolveData(receiveDataQueue, receiveCmdQueue));
            threadSolveData.IsBackground = true;
            threadSolveData.Start();

            threadSolveData = new Thread(() => TestOutputFrame(tbReceiveDataTest, receiveCmdQueue));
            threadSolveData.IsBackground = true;
            threadSolveData.Start();
        }

        private void btnSendTest_Click(object sender, EventArgs e)
        {
            byte[] testBytes = { 0x11, 0x22 };
            udpClient1.Send(testBytes, testBytes.Length, new System.Net.IPEndPoint(IPAddress.Parse("192.168.7.255"), 4567));
        }

        private void ReceiveThread()
        {
            IPEndPoint iPEndPoint_1 = new IPEndPoint(IPAddress.Parse("192.168.7.255"), 4567);
            while (true)
            {
                byte[] Data1 = udpClient1.Receive(ref iPEndPoint_1);
                foreach (Byte data in Data1)
                {
                    receiveDataQueue.Enqueue(data);
                }
                //Thread.Sleep(50);
            }
        }

        void AppendTextbox(TextBox textbox, Byte appendData)
        {
            textbox.Text += appendData.ToString("x2") + "\t";
            textbox.SelectionStart = textbox.TextLength;
            textbox.ScrollToCaret();
        }

        void AppendTextbox_2(TextBox textbox, List<Byte> appendData)
        {
            foreach (Byte tmpByte in appendData)
            {
                textbox.Text += tmpByte.ToString("x2");
            }
            textbox.Text += "\r\n";
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
                    Invoke(new tbDelegate_1(AppendTextbox), new object[] { textbox, tmpByte });
                }
                //Thread.Sleep(50);
            }
        }

        void TestOutputFrame(TextBox textbox, ConcurrentQueue<List<Byte>> concurrentQueue)
        {
            while (true)
            {
                List<Byte> tmpBytes;
                while (concurrentQueue.TryDequeue(out tmpBytes))
                {
                    Invoke(new tbDelegate_2(AppendTextbox_2), new object[] { textbox, new List<Byte>(tmpBytes) });
                }
                //Thread.Sleep(50);
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
                            frame.Add(tmpByte);
                            frame_site_cnt++;
                        }
                    }
                    else if (1 == frame_site_cnt)	//接收帧头
                    {
                        if (0xBB == tmpByte)
                        {
                            frame.Add(tmpByte);
                            frame_site_cnt++;
                        }
                        else
                        {
                            frame_site_cnt = 0;
                            frame.Clear();
                        }
                    }
                    else if (2 == frame_site_cnt)   //接收源地址
                    {
                        if (true)
                        {
                            frame.Add(tmpByte);
                            frame_site_cnt++;
                        }
                    }
                    else if (3 == frame_site_cnt)   //接收目的地址
                    {
                        if (true)
                        {
                            frame.Add(tmpByte);
                            frame_site_cnt++;
                        }
                    }
                    else if (4 == frame_site_cnt)   //接收长度
                    {
                        if (tmpByte + 6 < FrameMaxLength)
                        {
                            frame.Add(tmpByte);
                            frame_site_cnt++;
                        }
                        else
                        {
                            frame_site_cnt = 0;
                            frame.Clear();
                        }
                    }
                    else if (frame_site_cnt > 4 && frame_site_cnt <= frame[4]+4)   //接收数据区
                    {
                        frame.Add(tmpByte);
                        frame_site_cnt++;
                    }
                    else if (frame[4] + 4 + 1 == frame_site_cnt)	//接收校验字节	
                    {
                        frame.Add(tmpByte);
                        frame_site_cnt++;
                        List<Byte> checkData = new List<byte>(frame);
                        checkData.RemoveAt(checkData.Count - 1);
                        checkData.RemoveRange(0, 2);
                        if (CheckSum(checkData) == tmpByte)
                        {
                            des.Enqueue(new List<Byte>(frame));
                            frame_site_cnt = 0;
                            frame.Clear();
                        }
                        else
                        {
                            frame_site_cnt = 0;
                            frame.Clear();
                        }
                    }
                }
                //Thread.Sleep(10);
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
