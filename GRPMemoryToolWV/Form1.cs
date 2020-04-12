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

namespace GRPMemoryToolWV
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

        public IntPtr handle = IntPtr.Zero;
        public uint address;
        
        public class GRPStaticList
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
                if (ReadProcessMemory((int)handle,(int)( address + total), buf,(int)( size - total), ref read))
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

        public void GetHandle()
        {
            try
            {
                if (handle != IntPtr.Zero)
                    CloseHandle(handle);
                Process[] process = Process.GetProcessesByName("Yeti_Release");
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
                GRPStaticList list = new GRPStaticList();
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
                AddNode(t, bt.rootNode);
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

        private void AddNode(TreeNode tn, BTNode bn)
        {
            tn.Text = bn.data0.ToString("X8") + " : " +
                      bn.data1.ToString("X8") + " : " +
                      bn.data2.ToString("X8") ;
            if (bn.left != null)
            {
                TreeNode t = new TreeNode();
                AddNode(t, bn.left);
                tn.Nodes.Add(t);
            }
            if (bn.right != null)
            {
                TreeNode t = new TreeNode();
                AddNode(t, bn.right);
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
    }
}
