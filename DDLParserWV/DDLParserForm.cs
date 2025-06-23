using Be.Windows.Forms;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DDLParserWV
{
    public partial class DDLParserForm : Form
    {
        public BPTFile BPTFile { get; set; }
        public bool Ok { get; set; } = true;
        public string Json { get; set; }
        public string Markdown { get; set; }
        public StringBuilder DebugOutput { get; set; }

        public DDLParserForm()
        {
            InitializeComponent();
            tabControl1.SelectedTab = tabPage2;
        }

        private void ScanBinaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            if (d.ShowDialog() == DialogResult.OK)
            {
                Ok = true;
                byte[] data = File.ReadAllBytes(d.FileName);
                hb1.ByteProvider = new DynamicByteProvider(data);
                MemoryStream m = new MemoryStream(data);
                DebugOutput = new StringBuilder();
                var file = new BPTFile(Path.GetFileName(d.FileName));
                uint magic;
                while (m.Position < data.Length)
                {
                    try
                    {
                        magic = Utils.ReadU32(m);
                        if (magic == Utils.BPT_MAGIC)
                        {
                            ParseTree tree = new ParseTree(m, DebugOutput);
                            file.ParseTrees.Add(tree);
                        }
                    }
                    catch (Exception ex)
                    {
                        Ok = false;
                        Log($"[ERROR] {ex}");
                        Log($"[ERROR] Position = 0x{m.Position:X8}");
                        rtb1.Text = DebugOutput.ToString();
                    }
                }
                if (Ok)
                {
                    BPTFile = file;
                    try
                    {
                        Json = BPTFile.ToJson();
                        Markdown = BPTFile.ToMarkdown();
                    }
                    catch (Exception ex)
                    {
                        Ok = false;
                        Log($"[ERROR] {ex}");
                        rtb1.Text = DebugOutput.ToString();
                    }

                    if (radioButtonJson.Checked)
                        rtb1.Text = Json;
                    else if (radioButtonMarkdown.Checked)
                        rtb1.Text = Markdown;
                    else
                        rtb1.Text = DebugOutput.ToString();
                }
            }
        }

        private void RadioButtonJson_CheckedChanged(object sender, EventArgs e)
        {
            if (BPTFile != null && Ok)
                rtb1.Text = Json;
        }

        private void RadioButtonMarkdown_CheckedChanged(object sender, EventArgs e)
        {
            if (BPTFile != null && Ok)
                rtb1.Text = Markdown;
        }

        private void RadioButtonDebug_CheckedChanged(object sender, EventArgs e)
        {
            if (BPTFile != null && Ok)
                rtb1.Text = DebugOutput.ToString();
        }

        public void Log(string s)
        {
            DebugOutput.AppendLine(s);
        }
    }
}
