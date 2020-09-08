using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GROMemoryToolWV
{
    public partial class Form1 : Form
    {
        const int PROCESS_ALL_ACCESS = 0x1F0FFF;

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        public Process[] process;
        public IntPtr handle = IntPtr.Zero;
        public uint address;

        public class GROStaticList
        {
            public uint count;
            public uint capacity;
            public uint[] elements;
            public uint pList;
        }

        public class BinaryTree
        {
            public uint address;
            public BTNode smallestNode;
            public BTNode biggestNode;
            public BTNode rootNode;
            public uint count;
        }

        public class BTNode
        {
            public uint address;
            public BTNode left;
            public BTNode right;
            public BTNode parent;
            public uint data0;
            public uint data1;
            public uint data2;
        }

        public Form1()
        {
            InitializeComponent();
        }

        public byte[] ReadBuffer(IntPtr handle, uint address, uint size)
        {
            MemoryStream result = new MemoryStream();
            byte[] buf = new byte[size];
            uint total = 0;
            while (total < size)
            {
                int read = 0;
                if (ReadProcessMemory((int)handle, (int)(address + total), buf, (int)(size - total), ref read))
                {
                    result.Write(buf, 0, read);
                    total += (uint)read;
                }
            }
            return result.ToArray();
        }

        public uint ReadDWORD(IntPtr handle, uint address)
        {
            byte[] buff = ReadBuffer(handle, address, 4);
            return BitConverter.ToUInt32(buff, 0);
        }

        public string ReadCString(IntPtr handle, uint address)
        {
            string s = "";
            uint pos = address;
            while (s.Length < 10000)
            {
                byte[] buff = ReadBuffer(handle, pos, 1);
                if (buff[0] == 0)
                    break;
                s += (char)buff[0];
                pos++;
            }
            return s;
        }

        public void GetHandle()
        {
            try
            {
                if (handle != IntPtr.Zero)
                    CloseHandle(handle);
                process = Process.GetProcessesByName("Yeti_Release");
                if (process == null || process.Length == 0)
                {
                    Log("Error: Process 'Yeti_Release' not found!");
                    return;
                }
                handle = OpenProcess(PROCESS_ALL_ACCESS, false, process[0].Id);
            }
            catch { }
        }

        public void GetStartAddress()
        {
            address = 0;
            try
            {
                address = Convert.ToUInt32(toolStripTextBox1.Text.Trim(), 16);
            }
            catch { }
        }


        public void Log(string s)
        {
            rtb1.AppendText(s + "\n");
            rtb1.SelectionStart = rtb1.Text.Length;
            rtb1.ScrollToCaret();
        }

        public void ClearAll()
        {
            rtb1.Text = "";
            listBox1.Items.Clear();
            treeView1.Nodes.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (handle != IntPtr.Zero)
                CloseHandle(handle);
        }

        private void readStaticListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                GetHandle();
                GetStartAddress();
                if (handle == IntPtr.Zero || address == 0)
                    return;
                GROStaticList list = new GROStaticList();
                Log("Count            = " + (list.count = ReadDWORD(handle, address + 4)));
                Log("Capacity         = " + (list.capacity = ReadDWORD(handle, address + 8)));
                Log("List Pointer     = " + (list.pList = ReadDWORD(handle, address + 12)).ToString("X8"));
                if (list.capacity > 100)
                    throw new Exception("Unexpected huge capacity!");
                if (list.count > list.capacity)
                    throw new Exception("Count bigger than capacity!");
                list.elements = new uint[list.capacity];
                Log("Elements");
                Log("========");
                for (uint i = 0; i < list.capacity; i++)
                {
                    list.elements[i] = ReadDWORD(handle, list.pList + i * 4);
                    string s = i.ToString("D2") + " : " + list.elements[i].ToString("X8");
                    listBox1.Items.Add(s);
                    Log(s);
                }
            }
            catch (Exception ex)
            {
                Log("Error : " + ex.Message);
            }
        }

        private void readBinaryTreeToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            try
            {
                ClearAll();
                GetHandle();
                GetStartAddress();
                if (handle == IntPtr.Zero || address == 0)
                    return;
                BinaryTree bt = new BinaryTree();
                bt.address = address;
                Log("Count            = " + (bt.count = ReadDWORD(handle, address + 16)));
                uint root = ReadDWORD(handle, address + 8);
                if (root == 0)
                    throw new Exception("No Nodes found!");
                List<uint> ids = new List<uint>();
                bt.rootNode = ReadNode(root, ids);
                TreeNode t = new TreeNode();
                AddBTNode(t, bt.rootNode);
                treeView1.Nodes.Add(t);
                t.ExpandAll();
                ids.Sort();
                for (int i = 0; i < ids.Count; i++)
                    listBox1.Items.Add(i.ToString("D4") + " : " + ids[i].ToString("X8"));
            }
            catch (Exception ex)
            {
                Log("Error : " + ex.Message);
            }
        }

        private void AddBTNode(TreeNode tn, BTNode bn)
        {
            tn.Text = bn.data0.ToString("X8") + " : " +
                      bn.data1.ToString("X8") + " : " +
                      bn.data2.ToString("X8");
            if (bn.left != null)
            {
                TreeNode t = new TreeNode();
                AddBTNode(t, bn.left);
                tn.Nodes.Add(t);
            }
            if (bn.right != null)
            {
                TreeNode t = new TreeNode();
                AddBTNode(t, bn.right);
                tn.Nodes.Add(t);
            }
        }

        private BTNode ReadNode(uint addr, List<uint> ids)
        {
            BTNode result = new BTNode();
            result.address = addr;
            result.data0 = ReadDWORD(handle, addr + 16);
            ids.Add(result.data0);
            result.data1 = ReadDWORD(handle, addr + 20);
            result.data2 = ReadDWORD(handle, addr + 24);
            uint left = ReadDWORD(handle, addr);
            uint right = ReadDWORD(handle, addr + 4);
            if (left != 0)
                result.left = ReadNode(left, ids);
            if (right != 0)
                result.right = ReadNode(right, ids);
            return result;
        }

        private void readPropModListToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                ClearAll();
                GetHandle();
                GetStartAddress();
                if (handle == IntPtr.Zero || address == 0)
                    return;
                GROStaticList list = new GROStaticList();
                Log("Count            = " + (list.count = ReadDWORD(handle, address + 4)));
                Log("Capacity         = " + (list.capacity = ReadDWORD(handle, address + 8)));
                Log("List Pointer     = " + (list.pList = ReadDWORD(handle, address + 12)).ToString("X8"));
                if (list.capacity > 100)
                    throw new Exception("Unexpected huge capacity!");
                if (list.count > list.capacity)
                    list.count = list.capacity;
                list.elements = new uint[list.capacity];
                Log("Elements");
                Log("========");
                for (uint i = 0; i < list.capacity; i++)
                {
                    list.elements[i] = ReadDWORD(handle, list.pList + i * 4);
                    if (list.elements[i] == 0)
                        continue;
                    PropNode p = ReadPropNode(handle, list.elements[i]);
                    TreeNode t = new TreeNode();
                    AddPropNode(t, p);
                    treeView1.Nodes.Add(t);
                }
            }
            catch (Exception ex)
            {
                Log("Error : " + ex.Message);
            }
        }

        private void AddPropNode(TreeNode t, PropNode p)
        {
            t.Text = p.propID.ToString("X4") + " " + p.name;
            foreach (PropNode sp in p.list)
            {
                TreeNode nt = new TreeNode();
                AddPropNode(nt, sp);
                t.Nodes.Add(nt);
            }
        }

        public PropNode ReadPropNode(IntPtr handle, uint address)
        {
            PropNode p = new PropNode();
            p.propID = ReadDWORD(handle, address) & 0xFFFF;
            p.namePtr = ReadDWORD(handle, address + 4);
            p.subCount = ReadDWORD(handle, address + 8);
            p.listPtr = ReadDWORD(handle, address + 12);
            p.unk1 = ReadDWORD(handle, address + 16);
            p.unk2 = ReadDWORD(handle, address + 20);
            p.unk3 = ReadDWORD(handle, address + 24);
            p.unk4 = ReadDWORD(handle, address + 28);
            if (p.namePtr != 0)
                p.name = ReadCString(handle, p.namePtr);
            else
                p.name = "";
            string s = p.propID.ToString("X4") + " " + p.name;
            Log(s);
            listBox1.Items.Add(s);
            p.list = new List<PropNode>();
            if (p.listPtr != 0)
                for (int i = 0; i < p.subCount; i++)
                    p.list.Add(ReadPropNode(handle, (uint)(p.listPtr + i * 0x20)));
            return p;
        }

        public class PropNode
        {
            public uint propID;
            public uint namePtr;
            public uint subCount;
            public uint listPtr;
            public uint unk1;
            public uint unk2;
            public uint unk3;
            public uint unk4;
            public string name;
            public List<PropNode> list;
        }

        private static ushort ReadWord(Stream s)
        {
            byte[] buff = new byte[2];
            s.Read(buff, 0, 2);
            return BitConverter.ToUInt16(buff, 0);
        }

        private static uint ReadDword(Stream s)
        {
            byte[] buff = new byte[4];
            s.Read(buff, 0, 4);
            return BitConverter.ToUInt32(buff, 0);
        }

        private class BM_MSG
        {
            public ushort index;
            public ushort pIndex;
            public byte pCount;
            public byte flags;

            public BM_MSG(Stream s)
            {
                index = ReadWord(s);
                pIndex = ReadWord(s);
                pCount = (byte)s.ReadByte();
                flags = (byte)s.ReadByte();
            }
        }

        private void readNetBroadcastManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //00000000 msgs1           BM_MSG 1000 dup(?)
            //00001770 dword1770       dd ?
            //00001774 msgs2           BM_MSG3 1000 dup(?)
            //00006594 gap6594         db 46 dup(?)
            //000065C2 MsgStackCount   db ?
            //000065C3 MsgStackCount2  db ?
            //000065C4 gap65C4         db 192 dup(?)
            //00006684 msgs3           BM_MSG2 2000 dup(?)
            //0000C444 msgs4           BM_MSG2 1000 dup(?)
            //0000F324 wordF324        dw ?
            //0000F326 wordF326        dw ?
            //0000F328 paramStack      db 16 dup(?)
            //0000F338 isDefiningMessage db ?
            //0000F339 gapF339         db ?
            //0000F33A gapF33A         db ?
            //0000F33B gapF33B         db ?
            //0000F33C pMsg            dd ?                    ; offset
            //0000F340 paramCounter    db ?
            //0000F341 NetBuffers      BM_MSG4 32 dup(?)
            try
            {
                ClearAll();
                GetHandle();
                GetStartAddress();
                if (handle == IntPtr.Zero || address == 0)
                    return;
                byte[] buff = ReadBuffer(handle, address, 0x2F364);
                MemoryStream m = new MemoryStream(buff);
                List<BM_MSG> msgs = new List<BM_MSG>();
                TreeNode tMsgs = new TreeNode("Messages 0");
                TreeNode copy = tMsgs;
                for (int i = 0; i < 1000; i++)
                {
                    TreeNode t = new TreeNode("Message 0x" + i.ToString("X3"));
                    BM_MSG msg = new BM_MSG(m);
                    t.Nodes.Add(" Index = 0x" + msg.index.ToString("X4"));
                    t.Nodes.Add(" Parameter Index = 0x" + msg.pIndex.ToString("X4"));
                    t.Nodes.Add(" Parameter Count = 0x" + msg.pCount.ToString("X2"));
                    t.Nodes.Add(" Flags = 0x" + msg.flags.ToString("X2"));
                    t.Nodes.Add(" Parameter");
                    if ((msg.flags & 1) == 1)
                        t.Text += "(Created)";
                    if ((msg.flags & 2) == 2)
                        t.Text += "(Defined)";
                    msgs.Add(msg);
                    tMsgs.Nodes.Add(t);
                }
                treeView1.Nodes.Add(tMsgs);

                m.Seek(0x1774, 0);
                tMsgs = new TreeNode("Unknown List 1774");
                for (int i = 0; i < 1000; i++)
                {
                    TreeNode t = new TreeNode("Entry 0x" + i.ToString("X3"));
                    t.Nodes.Add(" DWORD_0 = 0x" + ReadDword(m).ToString("X8"));
                    t.Nodes.Add(" WORD_4 = 0x" + ReadWord(m).ToString("X4"));
                    t.Nodes.Add(" WORD_6 = 0x" + ReadWord(m).ToString("X4"));
                    t.Nodes.Add(" BYTE_8 = 0x" + m.ReadByte().ToString("X2"));
                    t.Nodes.Add(" BYTE_9 = 0x" + m.ReadByte().ToString("X2"));
                    t.Nodes.Add(" WORD_A = 0x" + ReadWord(m).ToString("X4"));
                    t.Nodes.Add(" WORD_C = 0x" + ReadWord(m).ToString("X4"));
                    t.Nodes.Add(" WORD_E = 0x" + ReadWord(m).ToString("X4"));
                    t.Nodes.Add(" DWORD_10 = 0x" + ReadDword(m).ToString("X8"));
                    tMsgs.Nodes.Add(t);
                }
                treeView1.Nodes.Add(tMsgs);

                treeView1.Nodes.Add("Msg Stack Count = 0x" + buff[0x65C2].ToString("X2"));

                treeView1.Nodes.Add("Msg Stack Count 2 = 0x" + buff[0x65C3].ToString("X2"));

                m.Seek(0x6684, 0);
                tMsgs = new TreeNode("Unknown List 6684");
                List<byte> paramTypeList = new List<byte>();
                for (int i = 0; i < 2000; i++)
                {
                    TreeNode t = new TreeNode("Entry 0x" + i.ToString("X3"));
                    t.Nodes.Add(" WORD_0 = 0x" + ReadWord(m).ToString("X4"));
                    t.Nodes.Add(" WORD_2 = 0x" + ReadWord(m).ToString("X4"));
                    t.Nodes.Add(" DWORD_4 = 0x" + ReadDword(m).ToString("X8"));
                    uint type = ReadDword(m);
                    t.Nodes.Add(" DWORD_8 = 0x" + type.ToString("X8"));
                    tMsgs.Nodes.Add(t);
                    paramTypeList.Add((byte)(type & 0xFF));
                }
                treeView1.Nodes.Add(tMsgs);

                for (int i = 0; i < msgs.Count; i++)
                {
                    BM_MSG msg = msgs[i];
                    if (msg.flags == 3)
                    {
                        Log("Message ID = 0x" + i.ToString("X3"));
                        TreeNode t = copy.Nodes[i].Nodes[4];
                        for (int j = 0; j < msg.pCount; j++)
                        {
                            int idx = msg.pIndex + j;
                            byte type = paramTypeList[idx];
                            string name = "Unknown 0x" + type.ToString("X2");
                            switch (type)
                            {
                                case 1:
                                    name = "Integer";
                                    break;
                                case 2:
                                    name = "Float";
                                    break;
                                case 3:
                                    name = "Object";
                                    break;
                                case 4:
                                    name = "Vector";
                                    break;
                                case 5:
                                    name = "Struct";
                                    break;
                            }
                            if ((type & 0x80) != 0)
                                name = "Buffer (Index = " + (type & 0x7F) + ")";
                            t.Nodes.Add(j + " : " + name);
                            Log(" " + j + " : " + name);
                        }
                        Log("");
                    }
                }

                m.Seek(0xC444, 0);
                tMsgs = new TreeNode("Unknown List C444");
                for (int i = 0; i < 1000; i++)
                {
                    TreeNode t = new TreeNode("Entry 0x" + i.ToString("X3"));
                    t.Nodes.Add(" WORD_0 = 0x" + ReadWord(m).ToString("X4"));
                    t.Nodes.Add(" WORD_2 = 0x" + ReadWord(m).ToString("X4"));
                    t.Nodes.Add(" DWORD_4 = 0x" + ReadDword(m).ToString("X8"));
                    t.Nodes.Add(" DWORD_8 = 0x" + ReadDword(m).ToString("X8"));
                    tMsgs.Nodes.Add(t);
                }
                treeView1.Nodes.Add(tMsgs);

                treeView1.Nodes.Add("Is defining Message = 0x" + buff[0xF338].ToString("X2"));

                TreeNode pStack = new TreeNode("Param Stack");
                for (int i = 0; i < 16; i++)
                    pStack.Nodes.Add(buff[0xF328 + i].ToString("X2"));
                treeView1.Nodes.Add(pStack);

                treeView1.Nodes.Add("Param Counter = 0x" + buff[0xF340].ToString("X2"));
            }
            catch (Exception ex)
            {
                Log("Error : " + ex.Message);
            }
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            treeView1.Visible = false;
            treeView1.SelectedNode.ExpandAll();
            treeView1.Visible = true;
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            treeView1.Visible = false;
            Collapse(treeView1.SelectedNode);
            treeView1.Visible = true;
        }

        private void Collapse(TreeNode t)
        {
            foreach (TreeNode n in t.Nodes)
                Collapse(n);
            t.Collapse();
        }

        private void readBankListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                GetHandle();
                GetStartAddress();
                if (handle == IntPtr.Zero || address == 0)
                    return;
                uint listStart = ReadDWORD(handle, address + 0xC);
                uint listEnd = ReadDWORD(handle, address + 0x10);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("List Start = " + listStart.ToString("X8"));
                sb.AppendLine("List End   = " + listEnd.ToString("X8"));
                if (listEnd - listStart > 1000)
                    return;
                sb.AppendLine("Reading Banks...");
                uint count = 0;
                for (uint p = listStart; p < listEnd; p += 4)
                    ReadBank(ReadDWORD(handle, p), count++, sb);
                Log(sb.ToString());
                
            }
            catch (Exception ex)
            {
                Log("Error : " + ex.Message);
            }
        }

        public void ReadBank(uint address, uint index, StringBuilder sb)
        {
            byte ID = (byte)(ReadDWORD(handle, address + 8) & 0xFF);
            uint listStart = ReadDWORD(handle, address + 0x18);
            uint listEnd = ReadDWORD(handle, address + 0x1C);
            sb.AppendLine("Bank #" + index);
            sb.AppendLine(" Address    = " + address.ToString("X8"));
            sb.AppendLine(" ID         = " + ID.ToString("X2"));
            sb.AppendLine(" List Start = " + listStart.ToString("X8"));
            sb.AppendLine(" List End   = " + listEnd.ToString("X8"));
            sb.AppendLine(" Entries:");
            for (uint p = listStart; p < listEnd; p += 8)
                ReadBankEntry(p, sb);
            sb.AppendLine();
        }

        public void ReadBankEntry(uint address, StringBuilder sb)
        {
            byte ID = (byte)(ReadDWORD(handle, address) & 0xFF);
            uint stuff = ReadDWORD(handle, address) >> 8;
            uint pointer = ReadDWORD(handle, address + 4);
            uint assetKey = ReadDWORD(handle, pointer + 4);
            sb.AppendLine("  BankItem " + ID.ToString("X2") + "=" + assetKey.ToString("X8"));
            uint listStart = ReadDWORD(handle, pointer + 0x14);
            uint listEnd = ReadDWORD(handle, pointer + 0x1C);
            uint count = 0;
            for (uint p = listStart; p < listEnd; p += 4)
                ReadBankEntrySub(ReadDWORD(handle,p), count++, sb);
        }

        public void ReadBankEntrySub(uint address, uint index, StringBuilder sb)
        {
            sb.Append("   SubItem " + index.ToString("X2") + " : ");
            sb.Append(" " + ReadDWORD(handle, address).ToString("X8"));
            sb.Append(" " + ReadDWORD(handle, address + 4).ToString("X8"));
            sb.Append(" " + ReadDWORD(handle, address + 8).ToString("X8"));
            sb.AppendLine(" " + ReadDWORD(handle, address + 0xC).ToString("X8"));
        }

        private void readZenNamespacesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                GetHandle();
                foreach (ProcessModule m in process[0].Modules)
                    if (Path.GetFileName(m.FileName) == "AICLASS_PCClient_R_org.dll")
                    {
                        uint baseAddress = (uint)m.BaseAddress.ToInt32();
                        uint address = baseAddress + 0x73E838;
                        uint ptrRoot = ReadDWORD(handle, address);
                        treeView1.Nodes.Add(ReadZenNamespaceNodes(ptrRoot, true));
                        treeView1.ExpandAll();
                    }
            }
            catch (Exception ex)
            {
                Log("Error : " + ex.Message);
            }
        }

        private TreeNode ReadZenNamespaceNodes(uint ptrNode, bool isRoot = false)
        {
            TreeNode t = new TreeNode();
            uint ptrName = ReadDWORD(handle, ptrNode + 0x24);
            uint ptrTypes = ReadDWORD(handle, ptrNode + 0x3C);
            uint countTypes = ReadDWORD(handle, ptrNode + 0x40);
            uint ptrNamespaces = ReadDWORD(handle, ptrNode + 0x44);
            uint countNamespaces = ReadDWORD(handle, ptrNode + 0x48);
            if (ptrName != 0)
                t.Text = "Namespace : " + ReadCString(handle, ptrName);
            for (uint i = 0; i < countNamespaces; i++)
            {
                uint ptrNext = ReadDWORD(handle, ptrNamespaces + i * 8);
                t.Nodes.Add(ReadZenNamespaceNodes(ptrNext));
            }
            for (uint i = 0; i < countTypes; i++)
            {
                uint ptrNext = ReadDWORD(handle, ptrTypes + i * 8);
                t.Nodes.Add(ReadZenType(ptrNext, isRoot));
            }
            return t;
        }

        private TreeNode ReadZenType(uint ptrNode, bool isRoot = false)
        {
            TreeNode t = new TreeNode();
            uint ptrTypeName = ReadDWORD(handle, ptrNode + 0x24);
            uint ptrMethods = ReadDWORD(handle, ptrNode + 0x48);
            uint countMethods = ReadDWORD(handle, ptrNode + 0x4C);
            uint ptrVariables = ReadDWORD(handle, ptrNode + 0x68);
            uint countVariables = ReadDWORD(handle, ptrNode + 0x6C);
            if (ptrTypeName != 0)
                t.Text = "Type : " + ReadCString(handle, ptrTypeName);
            for (uint i = 0; i < countMethods; i++)
            {
                uint ptrNext = ReadDWORD(handle, ptrMethods + i * 8);
                t.Nodes.Add(ReadZenMethod(ptrNext));
            }
            if(!isRoot)
                for (uint i = 0; i < countVariables; i++)
                {
                    uint ptrNext = ReadDWORD(handle, ptrVariables + i * 8);
                    t.Nodes.Add(ReadZenVariable(ptrNext));
                }
            return t;
        }

        private TreeNode ReadZenMethod(uint ptrNode)
        {
            TreeNode t = new TreeNode();
            uint ptrMethodName = ReadDWORD(handle, ptrNode + 0x24);;
            if (ptrMethodName != 0)
                t.Text = "Method : " + ReadCString(handle, ptrMethodName) + "()";
            return t;
        }

        private TreeNode ReadZenVariable(uint ptrNode)
        {
            TreeNode t = new TreeNode();
            uint ptrVarName = ReadDWORD(handle, ptrNode + 0x24); ;
            if (ptrVarName != 0)
                t.Text = "Variable : " + ReadCString(handle, ptrVarName);
            return t;
        }
    }
}
