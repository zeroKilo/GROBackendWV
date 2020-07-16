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
using Be.Windows.Forms;
using Ionic.Zlib;

namespace GROExplorerWV
{
    public partial class Form1 : Form
    {
        YETIFile yeti;
        public Form1()
        {
            InitializeComponent();
        }

        private void openYetibigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "yeti.big|yeti.big";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                yeti = new YETIFile(d.FileName, progressBar1);
                RefreshTree();
            }
        }

        private void RefreshTree()
        {
            tv1.Nodes.Clear();
            TreeNode root = new TreeNode("/");
            tv1.Nodes.Add(root);
            progressBar1.Value = 0;
            progressBar1.Maximum = yeti.files.Count;
            int count = 0;
            foreach (YETIFile.YETIFileEntry e in yeti.files)
            {
                AddFile(root, (e.path + "/" + e.name).Substring(1).Split('/'));
                if ((count++ % 1000) == 0)
                    progressBar1.Value = count;
            }
            root.Expand();
            progressBar1.Value = 0;
        }

        private void AddFile(TreeNode t, string[] parts)
        {
            if (parts.Length == 1)
                t.Nodes.Add(parts[0]);
            else
            {
                TreeNode sub = null;
                foreach(TreeNode s in t.Nodes)
                    if (s.Text == parts[0])
                    {
                        sub = s;
                        break;
                    }
                if (sub == null)
                {
                    sub = new TreeNode(parts[0]);
                    t.Nodes.Add(sub);
                }
                List<string> l = new List<string>();
                for (int i = 1; i < parts.Length; i++)
                    l.Add(parts[i]);
                AddFile(sub, l.ToArray());
            }
        }

        private void tv1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode t = tv1.SelectedNode;
            if (t == null)
                return;
            string fname = t.Text;
            string path = "";
            while (t.Parent != null && t.Parent.Text != "/")
            {
                t = t.Parent;
                path = "/" + t.Text + path;
            }
            foreach(YETIFile.YETIFileEntry file in yeti.files)
                if (file.name == fname && file.path == path)
                {
                    if (file.offset == 0xFFFFFFFF)
                        return;
                    uint address = yeti.dataOffset + file.offset * 8;
                    FileStream fs = new FileStream(yeti.myPath, FileMode.Open, FileAccess.Read);
                    fs.Seek(address, 0);
                    if (file.zip == 0)
                    {
                        uint size = YETIFile.ReadU32(fs);
                        byte[] buff = new byte[size];
                        fs.Read(buff, 0, (int)size);
                        hb1.ByteProvider = new DynamicByteProvider(buff);
                    }
                    else
                    {
                        uint csize = YETIFile.ReadU32(fs);
                        uint ucsize = YETIFile.ReadU32(fs);
                        byte[] buff = new byte[csize];
                        fs.Read(buff, 0, (int)csize);
                        hb1.ByteProvider = new DynamicByteProvider(Decompress(buff));
                    }
                    fs.Close();
                }                
        }

        public static byte[] Decompress(byte[] data)
        {
            ZlibStream s = new ZlibStream(new MemoryStream(data), Ionic.Zlib.CompressionMode.Decompress);
            MemoryStream result = new MemoryStream();
            s.CopyTo(result);
            return result.ToArray();
        }

        private void sortTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tv1.Visible = false;
            tv1.Sort();
            tv1.Visible = true;
        }

        private void exportHEXPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog d = new SaveFileDialog();
            d.Filter = "*.hex|*.hex";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MemoryStream m = new MemoryStream();
                for (long i = 0; i < hb1.ByteProvider.Length; i++)
                    m.WriteByte(hb1.ByteProvider.ReadByte(i));
                File.WriteAllBytes(d.FileName, m.ToArray());
                MessageBox.Show("Done.");
            }
        }

        private void processLoadReporttxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (yeti == null)
                return;
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "LoadReport.txt|LoadReport.txt";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                string[] lines = File.ReadAllLines(d.FileName);
                try
                {
                    foreach (string line in lines)
                        if (line.StartsWith("LOAD"))
                        {
                            string tmp = line;
                            string[] parts = line.Split(' ');
                            string ext = parts[1].Substring(5, 3);
                            uint key = Convert.ToUInt32(parts[3].Replace("]", ""), 16);
                            if (parts[3].Length != 9)
                                tmp = "LOAD type(" + ext + ") [key: " + key.ToString("x8") + "]  " + parts[5];
                            string info = " //";
                            foreach (YETIFile.YETIFileEntry file in yeti.files)
                                if (file.key == key)
                                {
                                    info += file.path + "/" + file.name + "." + ext;
                                    break;
                                }
                            sb.AppendLine(tmp + info);
                        }
                        else
                            sb.AppendLine(line);
                }
                catch { }
                File.WriteAllText(d.FileName + ".fixed.txt", sb.ToString());
                MessageBox.Show("Done.");
            }
        }
    }
}
