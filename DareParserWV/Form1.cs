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

namespace DareParserWV
{
    public partial class Form1 : Form
    {
        public StringBuilder sb;

        public Form1()
        {
            InitializeComponent();
            tabControl1.SelectedTab = tabPage2;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "*.hex|*.hex";
            if (d.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(d.FileName);
                hb1.ByteProvider = new DynamicByteProvider(data);
                MemoryStream m = new MemoryStream(data);
                sb = new StringBuilder();
                try
                {
                    while (m.Position < data.Length)
                        Parse(m);
                }
                catch
                {
                    Log("Position = 0x" + m.Position.ToString("X8"));
                }
                rtb1.Text = sb.ToString();
            }
        }

        public string ReadString(Stream s)
        {
            string result = "";
            uint len = ReadU32(s);
            if (len > 1000)
                throw new Exception();
            for (int i = 0; i < len; i++)
                result += (char)s.ReadByte();
            return result;
        }

        public uint ReadU32(Stream s)
        {
            uint result = 0;
            for (int i = 0; i < 4; i++)
            {
                result <<= 8;
                result |= (byte)s.ReadByte();
            }
            return result;
        }

        private void Log(string s)
        {
            sb.AppendLine(s);
        }

        public string MakeTabs(int depth)
        {
            string tabs = "";
            for (int i = 0; i < depth; i++)
                tabs += "\t";
            return tabs;
        }

        public void Parse(Stream m, int depth = 0)
        {
            uint count;
            string tabs = MakeTabs(depth);
            count = ReadU32(m);
            for (int i = 0; i < count; i++)
            {
                byte type = (byte)m.ReadByte();
                switch (type)
                {
                    case 6:
                        ParseVariable(m, depth);
                        break;
                    case 8:
                        ParseUnknown8(m, depth);
                        break;
                    case 0xB:
                        ParsePropertyDeclaration(m, depth);
                        break;
                    case 0xC:
                        ParseUnknownC(m, depth);
                        break;
                    case 0xD:
                        ParseUnknownD(m, depth);
                        break;
                    case 0xF:
                        ParseUnknownF(m, depth);
                        break;
                    case 0x12:
                        ParseUnknown12(m, depth);
                        break;
                    case 0x13:
                        ParseUnitDeclaration(m, depth);
                        break;
                    default:
                        Log("Unknown type found: 0x" + type.ToString("X2"));
                        throw new Exception();
                }
            }
        }

        public void ParseUnitDeclaration(Stream m, int depth = 0)
        {
            string tabs = MakeTabs(depth);
            Log(tabs + "[Unit Declaration]");
            ParseDeclaration(m, depth + 1);
            Log(tabs + "\t[" + ReadString(m) + "]");
            Log(tabs + "\t[" + ReadString(m) + "]");
        }

        public void ParseDeclaration(Stream m, int depth = 0)
        {
            string tabs = MakeTabs(depth);
            Log(tabs + "[Declaration]");
            ParseNamespaceItem(m, depth + 1);
            ParseNamespace(m, depth + 1);
        }

        public void ParseNamespaceItem(Stream m, int depth = 0)
        {
            string tabs = MakeTabs(depth);
            Log(tabs + "[Namespace Item]");
            Log(tabs + "\t[" + ReadString(m) + "]");
            Log(tabs + "\t[" + ReadString(m) + "]");
        }

        public void ParseNamespace(Stream m, int depth = 0)
        {
            string tabs = MakeTabs(depth);
            Log(tabs + "[Namespace]");
            Log(tabs + "\t[" + ReadString(m) + "]");
            Parse(m, depth + 1);
        }

        public void ParseUnknown12(Stream m, int depth = 0)
        {
            string tabs = MakeTabs(depth);
            Log(tabs + "[Unknown_12]");
            ParseDeclaration(m, depth + 1);
            ParseNamedStringList(m, depth + 1);
        }

        public void ParseNamedStringList(Stream m, int depth = 0)
        {
            uint count;
            string tabs = MakeTabs(depth);
            Log(tabs + "[Named String List]");
            Log(tabs + "\t[" + ReadString(m) + "]");
            count = ReadU32(m);
            Log(tabs + "\t[0x" + count.ToString("X8") + "]");
            for (int i = 0; i < count; i++)
                Log(tabs + "\t\t[" + ReadString(m) + "]");
        }

        public void ParseUnknownF(Stream m, int depth = 0)
        {
            string tabs = MakeTabs(depth);
            Log(tabs + "[Unknown_F]");
            ParseDeclaration(m, depth + 1);
            ParseNamespace(m, depth + 1);
        }

        public void ParsePropertyDeclaration(Stream m, int depth = 0)
        {
            string tabs = MakeTabs(depth);
            Log(tabs + "[Property Declaration]");
            ParseNamespaceItem(m, depth + 1);
            ParseNamespace(m, depth + 1);
            Log(tabs + "\t[0x" + ReadU32(m).ToString("X8") + "]");
            Log(tabs + "\t[0x" + ReadU32(m).ToString("X8") + "]");
        }

        public void ParseVariable(Stream m, int depth = 0)
        {
            string tabs = MakeTabs(depth);
            Log(tabs + "[Variable]");
            Log(tabs + "\t[" + ReadString(m) + "]");
            Log(tabs + "\t[" + ReadString(m) + "]");
            ParseDeclarationUse(m, depth + 1);
            Log(tabs + "\t[0x" + ReadU32(m).ToString("X8") + "]");

        }

        public void ParseDeclarationUse(Stream m, int depth = 0)
        {
            string tabs = MakeTabs(depth);
            byte b = (byte)m.ReadByte();
            Log(tabs + "[Sub Type = 0x" + b.ToString("X2") + "]");
            if (b == 18)
                ParseSubType18(m, depth + 1);
            else
                Log(tabs + "\t[" + ReadString(m) + "]");
        }

        public void ParseSubType18(Stream m, int depth = 0)
        {
            string tabs = MakeTabs(depth);
            Log(tabs + "[Sub Type 18]");
            Log(tabs + "\t[" + ReadString(m) + "]");
            Log(tabs + "\t[" + ReadString(m) + "]");
            byte count = (byte)m.ReadByte();
            for (int i = 0; i < count; i++)
                ParseDeclarationUse(m, depth + 1);
        }

        public void ParseUnknownC(Stream m, int depth = 0)
        {
            string tabs = MakeTabs(depth);
            Log(tabs + "[Unknown_C]");
            ParseDeclaration(m, depth + 1);
            Parse(m, depth + 1);
        }

        public void ParseUnknown8(Stream m, int depth = 0)
        {
            string tabs = MakeTabs(depth);
            Log(tabs + "[Unknown_8]");
            ParseUnknownC(m, depth + 1);
            Parse(m, depth + 1);
        }

        public void ParseUnknownD(Stream m, int depth = 0)
        {
            string tabs = MakeTabs(depth);
            Log(tabs + "[Unknown_D]");
            ParseVariable(m, depth + 1);
            ParseDeclarationUse(m, depth + 1);
            Log(tabs + "\t[0x" + ReadU32(m).ToString("X8") + "]");
            Log(tabs + "\t[0x" + ((byte)m.ReadByte()).ToString("X8") + "]");
        }
    }
}
