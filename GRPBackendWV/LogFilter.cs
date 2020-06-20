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
    public partial class LogFilter : Form
    {
        public LogFilter()
        {
            InitializeComponent();
        }

        private void label1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void label1_DragDrop(object sender, DragEventArgs e)
        {
            string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            Process(File.ReadAllLines(filePaths[0]));
        }

        List<string> skippers = new List<string>()
        {
            "[NetZ] The scheduler will",
            "[NetZ]   	State",
            "[NetZ]   	Execution count",
            "[NetZ]   Job UDPTransport::TransportJob",
            "BLOOMBERG Trace - Quitting analysis",
            "[NetZ]   Job CallContextRegister::CheckExpiredCalls",
            "[NetZ]   Period:",
            "RENDEZVOUS => # INFO                                             -",
            "LOAD type(",
            "[NetZ] (S:unknown) The scheduler ",
            "[NetZ] (S:unknown)   Job ",
            "[NetZ] (S:unknown)   	State ",
            "[NetZ] (S:unknown)   	Execution ",
            "EndPoint::",
            "[NetZ] Job ",
            "[NetZ]   Job ",
            "[NetZ]   	Current step",
            "(S:5c00002) The scheduler will",
            "(S:5c00002)   Job",
            "(S:5c00002)   	State",
            "(S:5c00002)   	Execution"
        };

        private void Process(string[] lines)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string line in lines)
            {
                if (line.Length == 0 || line.Trim() == "")
                    continue;
                bool found = false;
                foreach (string skip in skippers)
                    if (line.Contains(skip))
                    {
                        found = true;
                        break;
                    }
                if (!found)
                    sb.AppendLine(line);
            }
            richTextBox1.Text = sb.ToString();
        }
    }
}
