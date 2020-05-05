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

namespace GRPBackendWV
{
    public partial class UDPProcessor : Form
    {
        public UDPProcessor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                StringReader sr = new StringReader(rtb1.Text);
                string line;
                List<string> lines = new List<string>();
                while ((line = sr.ReadLine()) != null)
                    lines.Add(line);
                StringBuilder sb = new StringBuilder();
                foreach (string l in lines)
                {
                    byte[] data = makeArray(l.Trim());
                    QPacket qp = new QPacket(data);
                    sb.AppendLine(qp.ToStringDetailed());
                }
                rtb1.Text = sb.ToString();
            }
            catch { MessageBox.Show("Error"); }
        }

        private byte[] makeArray(string s)
        {
            MemoryStream m = new MemoryStream();
            for (int i = 0; i < s.Length / 2; i++)
                m.WriteByte(Convert.ToByte(s.Substring(i * 2, 2), 16));
            return m.ToArray();
        }
    }
}
