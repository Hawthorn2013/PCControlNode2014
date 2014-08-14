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
using System.Timers;

namespace PCControlNode2014
{
    public partial class Form1 : Form
    {
        UdpClient udpClient1;
        Thread threadReceiveAndSolveData;
        Thread threadOutputData;
        Thread threadSolveFramePacket;
        System.Timers.Timer timer1;
        ConcurrentQueue<Byte> receiveDataQueue;
        ConcurrentQueue<List<Byte>> receiveCmdQueue;
        ConcurrentQueue<FramePacket> framePacketQueue;
        delegate void tbDelegate_1(TextBox textBox, Byte appendData);
        delegate void tbDelegate_2(TextBox textBox, List<Byte> appendData);
        delegate void lvDelegate(ListViewItem lvi, ListView lv);
        delegate void lvDelegate_2(List<DeviceStatus> dss, ListView lv);
        const int FrameMaxLength = 32;
        Dictionary<UInt16, string> WiFiCMD;
        List<DeviceStatus> devicesStatus;
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

            //lvDevicesStatus.Items.Add();

            receiveDataQueue = new System.Collections.Concurrent.ConcurrentQueue<Byte>();
            devicesStatus = new List<DeviceStatus>(0x0100);
            for (int i = 0; i < 0x0100; i++)
            {
                DeviceStatus ds = new DeviceStatus();
                ds.deviceNO = (Byte)i;
                ds.lastTime = DateTime.Now;
                devicesStatus.Add(ds);
            }
            receiveCmdQueue = new ConcurrentQueue<List<Byte>>();
            framePacketQueue = new ConcurrentQueue<FramePacket>();
            udpClient1 = new UdpClient(4567);

            threadSolveFramePacket = new Thread(() => ReceiveAndSolveData(framePacketQueue));
            threadSolveFramePacket.IsBackground = true;
            threadSolveFramePacket.Start();

            threadReceiveAndSolveData = new Thread(() => SolveFramePacket(framePacketQueue, lvFrame, lvDevicesStatus));
            threadReceiveAndSolveData.IsBackground = true;
            threadReceiveAndSolveData.Start();

            //threadOutputData = new Thread(() => TestOutputFrame_2(tbReceiveDataTest, framePacketQueue));
            //threadOutputData.IsBackground = true;
            //threadOutputData.Start();

            timer1 = new System.Timers.Timer(1000);
            timer1.AutoReset = true;
            //() => UpdateLvDevicesStatus(devicesStatus, lvDevicesStatus)
            timer1.Elapsed += new ElapsedEventHandler(timeArrive);
            timer1.Enabled = true;
            WiFiCMD = new Dictionary<UInt16, string>();
            WiFiCMD.Add(0x0001, "舵 目标");
            WiFiCMD.Add(0x0002, "舵 P");
            WiFiCMD.Add(0x0003, "舵 I");
            WiFiCMD.Add(0x0004, "舵 D");
            WiFiCMD.Add(0x0005, "电 目标");
            WiFiCMD.Add(0x0006, "电 P");
            WiFiCMD.Add(0x0007, "电 I");
            WiFiCMD.Add(0x0008, "电 D");
            WiFiCMD.Add(0x0009, "陀螺仪 开始");
            WiFiCMD.Add(0x000A, "陀螺仪 停止");
            WiFiCMD.Add(0x000B, "舵 中");
            WiFiCMD.Add(0x000C, "舵 左");
            WiFiCMD.Add(0x000D, "舵 右");
            WiFiCMD.Add(0x000E, "舵 写");
            WiFiCMD.Add(0x000F, "舵 发送");
            WiFiCMD.Add(0x0010, "");
            WiFiCMD.Add(0x0011, "获取 当前速度");
            WiFiCMD.Add(0x0012, "停止获取 当前速度");

            WiFiCMD.Add(0x0100, "赛场控制");
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

        private void SolveFramePacket(ConcurrentQueue<FramePacket> queue, ListView tmpLvFrame, ListView tmpLvDevicesStatus)
        {
            while (true)
            {
                FramePacket framePacket;
                while (queue.TryDequeue(out framePacket))
                {
                    ListViewItem lvi = new ListViewItem(framePacket.srcIP.ToString().Split('.')[3]);    //源IP
                    lvi.SubItems.Add("0x" + framePacket.frame[2].ToString("X2"));   //源设备
                    devicesStatus[framePacket.frame[2]].lastTime = DateTime.Now;    //更新设备时间
                    lvi.SubItems.Add("0x" + framePacket.frame[3].ToString("X2"));   //目标设备
                    UInt16 cmd = BitConverter.ToUInt16(new byte[] { framePacket.frame[6], framePacket.frame[5] }, 0);
                    if (null != WiFiCMD[cmd])
                    {
                        lvi.SubItems.Add(WiFiCMD[cmd]);
                    }
                    else
                    {
                        lvi.SubItems.Add(cmd.ToString("X4"));
                    }
                    if (10 == framePacket.frame.Count && ( 0x0001 == cmd || 0x0002 == cmd || 0x0003 == cmd || 0x0004 == cmd || 0x000B == cmd || 0x000C == cmd || 0x000D == cmd ) )
                    {
                        lvi.SubItems.Add(BitConverter.ToUInt16(new byte[] { framePacket.frame[8], framePacket.frame[7] }, 0).ToString());
                    }
                    else if (10 == framePacket.frame.Count && (0x0005 == cmd || 0x0006 == cmd || 0x0007 == cmd || 0x0008 == cmd))
                    {
                        lvi.SubItems.Add(BitConverter.ToInt16(new byte[] { framePacket.frame[8], framePacket.frame[7] }, 0).ToString());
                    }
                    else if (14 == framePacket.frame.Count && 0x000F == cmd)    //显示舵机底层数据
                    {
                        lvi.SubItems.Add(BitConverter.ToUInt16(new byte[] { framePacket.frame[8], framePacket.frame[7] }, 0).ToString()
                             + " " + BitConverter.ToUInt16(new byte[] { framePacket.frame[10], framePacket.frame[9] }, 0).ToString()
                             + " " + BitConverter.ToUInt16(new byte[] { framePacket.frame[12], framePacket.frame[11] }, 0).ToString()
                            );
                    }
                    else if (14 == framePacket.frame.Count && 0x0014 == cmd)    //获取陀螺仪数据积分值
                    {
                        lvi.SubItems.Add(BitConverter.ToInt16(new byte[] { framePacket.frame[8], framePacket.frame[7] }, 0).ToString()
                             + " " + BitConverter.ToInt16(new byte[] { framePacket.frame[10], framePacket.frame[9] }, 0).ToString()
                             + " " + BitConverter.ToInt16(new byte[] { framePacket.frame[12], framePacket.frame[11] }, 0).ToString()
                            );
                    }
                    Invoke(new lvDelegate(AppendListView), new object[] { lvi, tmpLvFrame });
                    Invoke(new lvDelegate_2(UpdateLvDevicesStatus), new object[] { devicesStatus, lvDevicesStatus });
                }
                //Invoke(new lvDelegate_2(UpdateLvDevicesStatus), new object[] { devicesStatus, lvDevicesStatus });
                //Thread.Sleep(10);
            }
        }

        void AppendTextbox(TextBox textbox, Byte appendData)
        {
            textbox.Text += appendData.ToString("x2") + "\t";
            textbox.SelectionStart = textbox.TextLength;
            textbox.ScrollToCaret();
        }

        void AppendListView(ListViewItem lvi, ListView lv)
        {
            lv.Items.Add(lvi);
            lv.Items[lv.Items.Count - 1].EnsureVisible();
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

            //System.Text.UnicodeEncoding converter = new UnicodeEncoding();
            //textbox.Text += converter.GetString(appendData.ToArray<Byte>());
            //textbox.Text += "\r\n";
        }

        void timeArrive(object source, System.Timers.ElapsedEventArgs e)    //更新设备状态
        {
            Invoke(new lvDelegate_2(UpdateLvDevicesStatus), new object[] { devicesStatus, lvDevicesStatus });
        }

        void UpdateLvDevicesStatus(List<DeviceStatus> dss, ListView tmpLvDevicesStatus)
        {
            foreach (ListViewItem lvi in tmpLvDevicesStatus.Items)
            {
                DateTime dt = new DateTime();
                dt = dss[Convert.ToUInt16(lvi.Text)].lastTime;
                if (dt.AddSeconds(3) < DateTime.Now)
                {
                    lvi.SubItems[1].Text = "offline";
                }
                else
                {
                    lvi.SubItems[1].Text = "online";
                }
            }
        }

        void TestOutputFrame_2(TextBox textbox, ConcurrentQueue<FramePacket> queue)
        {
            while (true)
            {
                FramePacket framePacket;
                while (queue.TryDequeue(out framePacket))
                {
                    System.Text.UnicodeEncoding converter = new UnicodeEncoding();
                    //Invoke(new tbDelegate_2(AppendTextbox_2), new object[] { textbox, framePacket.frame });
                    //Invoke(new tbDelegate_2(AppendTextbox_2), new object[] { textbox, new List<Byte>(converter.GetBytes(framePacket.srcIP.ToString())) });
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

        private void btnTestPlay_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofDialog = new OpenFileDialog();
            ofDialog.AddExtension = true;
            ofDialog.CheckFileExists = true;
            ofDialog.CheckPathExists = true;
            ofDialog.Filter = "VCD文件(*.dat)|*.dat|Audio文件(*.avi)|*.avi|WAV文件(*.wav)|*.wav|MP3文件(*.mp3)|*.mp3|所有文件 (*.*)|*.*";
            ofDialog.DefaultExt = "*.mp3";
            if(ofDialog.ShowDialog() == DialogResult.OK)
            {
                this.axWindowsMediaPlayer1.URL = ofDialog.FileName;
            }  
        }
    }


    class DeviceStatus
    {
        public Byte deviceNO;
        public DateTime lastTime;
    }
}
