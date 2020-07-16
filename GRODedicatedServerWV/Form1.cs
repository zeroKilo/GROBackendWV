using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuazalWV;

namespace GRODedicatedServerWV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Log.logFileName = "dslog.txt";
            Log.ClearLog();
            Log.box = rtb1;
            DBHelper.Init();
            toolStripComboBox1.SelectedIndex = 0;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            uint mapKey = Convert.ToUInt32(toolStripTextBox2.Text, 16);
            SessionInfosParameter.defaultMapKey = mapKey;
            Log.WriteLine(1, "Using mapkey = 0x" + mapKey.ToString("X8"), Color.Red);
            timer1.Enabled = true;
            UDPDedicatedServer.Start();
            toolStripTextBox2.Enabled =
            toolStripButton1.Enabled = false;
            toolStripButton2.Enabled = true;

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            UDPDedicatedServer.Stop();
            toolStripTextBox2.Enabled =
            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = false;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            rtb1.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            NotificationQuene.Update();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UDPDedicatedServer.Stop();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBox1.SelectedIndex)
            {
                default:
                case 0:
                    Log.MinPriority = 1;
                    break;
                case 1:
                    Log.MinPriority = 2;
                    break;
                case 2:
                    Log.MinPriority = 5;
                    break;
                case 3:
                    Log.MinPriority = 10;
                    break;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            uint u = Convert.ToUInt32(toolStripTextBox1.Text.Trim(), 16);
            MessageBox.Show(new DupObj(u).getDesc());
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (DupObj obj in DO_Session.DupObjs)
                listBox1.Items.Add(obj.getDesc());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            rtb2.Text = "";
            int n = listBox1.SelectedIndex;
            if (n < 0 || n >= DO_Session.DupObjs.Count)
                return;
            if (DO_Session.DupObjs[n].Payload != null)
                rtb2.Text = DO_Session.DupObjs[n].Payload.getDesc();
            else
                rtb2.Text = "No Payload";
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            Log.enablePacketLogging = toolStripButton9.Checked;
        }
    }
}
