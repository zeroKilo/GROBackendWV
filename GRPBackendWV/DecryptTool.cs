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

namespace GRPBackendWV
{
    public partial class DecryptTool : Form
    {
        public DecryptTool()
        {
            InitializeComponent();
        }

        private byte[] ToBuff(string s)
        {
            MemoryStream m = new MemoryStream();
            for (int i = 0; i < s.Length; i += 2)
                m.WriteByte(Convert.ToByte(s.Substring(i, 2), 16));
            return m.ToArray();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string s = richTextBox1.Text.Trim();
                while (s.Contains(" "))
                    s = s.Replace(" ", "");
                if ((s.Length % 2) != 0)
                    return;
                byte[] data = ToBuff(s);
                data = Helper.Decrypt("CD&ML", data);
                MemoryStream m = new MemoryStream();
                m.Write(data, 1, data.Length - 1);
                data = Helper.Decompress(m.ToArray());
                StringBuilder sb = new StringBuilder();
                foreach (byte b in data)
                    sb.Append(b.ToString("X2"));
                richTextBox2.Text = sb.ToString();
            }
            catch { richTextBox2.Text = "ERROR"; }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                string s = richTextBox2.Text.Trim();
                while (s.Contains(" "))
                    s = s.Replace(" ", "");
                if ((s.Length % 2) != 0)
                    return;
                byte[] data = ToBuff(s);
                uint sizeBefore = (uint)data.Length;
                byte[] buff = Helper.Compress(data);
                byte count = (byte)(sizeBefore / buff.Length);
                if ((sizeBefore % buff.Length) != 0)
                    count++;
                MemoryStream m = new MemoryStream();
                m.WriteByte(count);
                m.Write(buff, 0, buff.Length);
                data = Helper.Encrypt("CD&ML", m.ToArray());
                StringBuilder sb = new StringBuilder();
                foreach (byte b in data)
                    sb.Append(b.ToString("X2"));
                richTextBox1.Text = sb.ToString();
            }
            catch { richTextBox1.Text = "ERROR"; }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                string s = richTextBox1.Text.Trim();
                while (s.Contains(" "))
                    s = s.Replace(" ", "");
                if ((s.Length % 2) != 0)
                    return;
                byte[] data = ToBuff(s);
                data = Helper.Decrypt("CD&ML", data);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in data)
                    sb.Append(b.ToString("X2"));
                richTextBox2.Text = sb.ToString();
            }
            catch { richTextBox2.Text = "ERROR"; }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            try
            {
                string s = richTextBox2.Text.Trim();
                while (s.Contains(" "))
                    s = s.Replace(" ", "");
                if ((s.Length % 2) != 0)
                    return;
                byte[] data = ToBuff(s);
                data = Helper.Encrypt("CD&ML", data);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in data)
                    sb.Append(b.ToString("X2"));
                richTextBox1.Text = sb.ToString();
            }
            catch { richTextBox1.Text = "ERROR"; }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                string s = richTextBox1.Text.Trim();
                while (s.Contains(" "))
                    s = s.Replace(" ", "");
                if ((s.Length % 2) != 0)
                    return;
                byte[] data = ToBuff(s);
                MemoryStream m = new MemoryStream();
                m.Write(data, 1, data.Length - 1);
                data = Helper.Decompress(m.ToArray());
                StringBuilder sb = new StringBuilder();
                foreach (byte b in data)
                    sb.Append(b.ToString("X2"));
                richTextBox2.Text = sb.ToString();
            }
            catch { richTextBox2.Text = "ERROR"; }
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            try
            {
                string s = richTextBox2.Text.Trim();
                while (s.Contains(" "))
                    s = s.Replace(" ", "");
                if ((s.Length % 2) != 0)
                    return;
                byte[] data = ToBuff(s);
                uint sizeBefore = (uint)data.Length;
                byte[] buff = Helper.Compress(data);
                byte count = (byte)(sizeBefore / buff.Length);
                if ((sizeBefore % buff.Length) != 0)
                    count++;
                MemoryStream m = new MemoryStream();
                m.WriteByte(count);
                m.Write(buff, 0, buff.Length);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in data)
                    sb.Append(b.ToString("X2"));
                richTextBox1.Text = sb.ToString();
            }
            catch { richTextBox1.Text = "ERROR"; }
        }
    }
}
