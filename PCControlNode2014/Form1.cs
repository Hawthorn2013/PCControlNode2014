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
        Thread threadReceiveAndSolveData;
        Thread threadOutputData;
        ConcurrentQueue<Byte> receiveDataQueue;
        ConcurrentQueue<List<Byte>> receiveDataQueue_2;
        ConcurrentQueue<List<Byte>> receiveCmdQueue;
        ConcurrentQueue<FramePacket> framePacketQueue;
        delegate void tbDelegate_1(TextBox textBox, Byte appendData);
        delegate void tbDelegate_2(TextBox textBox, List<Byte> appendData);
        const int FrameMaxLength = 32;
        public enum FramePacketType
        {
            Correct,
            ChechSumError,
            Uncompleted,
        }
        struct FramePacket
        {
            public FramePacketType type;
            public IPAddress srcIP;
            public List<Byte> frame;
        }

        public Form1()
        {
            InitializeComponent();

            receiveDataQueue = new System.Collections.Concurrent.ConcurrentQueue<Byte>();

            receiveCmdQueue = new ConcurrentQueue<List<Byte>>();
            framePacketQueue = new ConcurrentQueue<FramePacket>();
            udpClient1 = new UdpClient(4567);

            threadReceiveAndSolveData = new Thread(() => ReceiveAndSolveData(framePacketQueue));
            threadReceiveAndSolveData.IsBackground = true;
            threadReceiveAndSolveData.Start();

            threadOutputData = new Thread(() => TestOutputFrame_2(tbReceiveDataTest, framePacketQueue));
            threadOutputData.IsBackground = true;
            threadOutputData.Start();
        }

        private void btnSendTest_Click(object sender, EventArgs e)
        {
            byte[] testBytes = { 0x11, 0x22 };
            udpClient1.Send(testBytes, testBytes.Length, new System.Net.IPEndPoint(IPAddress.Parse("192.168.7.255"), 4567));
        }

        private void ReceiveAndSolveData(ConcurrentQueue<FramePacket> queue)
        {
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("192.168.7.255"), 4567);
            while (true)
            {
                byte[] Data = udpClient1.Receive(ref iPEndPoint);
                int frame_site_cnt = 0;
                List<Byte> frame = new List<byte>();
                foreach (Byte tmpByte in Data)
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
                    else if (frame_site_cnt > 4 && frame_site_cnt <= frame[4] + 4)   //接收数据区
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
                            FramePacket framePacket;
                            framePacket.type = FramePacketType.Correct;
                            framePacket.srcIP = iPEndPoint.Address;
                            framePacket.frame = new List<byte>(frame);
                            queue.Enqueue(framePacket);
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

        void TestOutputFrame_2(TextBox textbox, ConcurrentQueue<FramePacket> queue)
        {
            while (true)
            {
                FramePacket framePacket;
                while (queue.TryDequeue(out framePacket))
                {
                    Invoke(new tbDelegate_2(AppendTextbox_2), new object[] { textbox, framePacket.frame });
                }
                //Thread.Sleep(50);
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
