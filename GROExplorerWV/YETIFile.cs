using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GROExplorerWV
{
    public class YETIFile
    {
        public class YETIFileEntry
        {
            public uint offset;
            public string name;
            public string path;
            public ushort folder;
            public uint flags;
            public uint zip;
            public uint key;
            public YETIFileEntry(Stream s)
            {
                offset = ReadU32(s);
                key = ReadU32(s);
                s.Read(new byte[6], 0, 6);
                folder = ReadU16(s);
                ReadU32(s);
                flags = ReadU32(s);
                ReadU32(s);
                ReadU32(s);
                name = "";
                for (int i = 0; i < 0x3C; i++)
                {
                    byte b = (byte)s.ReadByte();
                    if (b != 0)
                        name += (char)b;
                }
                ReadU32(s);
                zip = ReadU32(s);
            }

            public override string ToString()
            {
                return name;
            }
        }

        public List<YETIFileEntry> files = new List<YETIFileEntry>();
        public string myPath;
        public uint baseOffset;
        public uint folderOffset;
        public uint dataOffset;

        public YETIFile(string filename, ProgressBar pb)
        {
            myPath = filename;
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            uint magic = ReadU32(fs);
            if (magic == 0x47494259)
            {
                ReadU32(fs);
                ReadU32(fs);
                ReadU32(fs);
                uint offset = ReadU32(fs);
                fs.Seek(offset, 0);
                ReadU16(fs);
                ushort fcount = ReadU16(fs);
                uint count = ReadU32(fs);
                fs.Read(new byte[0x78], 0, 0x78);
                pb.Value = 0;
                pb.Maximum = (int)count;
                for (int i = 0; i < count; i++)
                {
                    files.Add(new YETIFileEntry(fs));
                    if ((i % 1000) == 0)
                    {
                        pb.Value = i;
                        Application.DoEvents();
                    }
                }
                baseOffset = offset + 0x80;
                folderOffset = baseOffset + count * 100;                
                while ((folderOffset & 8) != 0)
                    folderOffset++;
                dataOffset = (uint)(folderOffset + fcount * 64);
                while ((dataOffset & 8) != 0)
                    dataOffset++;
                pb.Value = 0;
                pb.Maximum = (int)count;
                for (int i = 0; i < count; i++)
                {
                    files[i].path = GetPath(fs, files[i].folder);
                    if ((i % 1000) == 0)
                    {
                        pb.Value = i;
                        Application.DoEvents();
                    }
                }
                pb.Value = 0;
            }
            fs.Close();
        }

        private string GetPath(Stream s, ushort folder)
        {
            ushort f = folder;
            if (folder == 0xffff)
                return "";
            string result = "";
            while (f != 0xffff)
            {
                s.Seek(folderOffset + f * 0x40, 0);
                ReadU32(s);
                f = ReadU16(s);
                ReadU16(s);
                ReadU16(s);
                string name = "";
                for (int i = 0; i < 0x36; i++)
                {
                    byte b = (byte)s.ReadByte();
                    if (b != 0)
                        name += (char)b;
                    else
                        break;
                }
                if (name != "/")
                    result = "/" + name + result;
            }
            return result;
        }

        public static uint ReadU32(Stream s)
        {
            byte[] buff = new byte[4];
            s.Read(buff, 0, 4);
            return BitConverter.ToUInt32(buff, 0);
        }

        public static ushort ReadU16(Stream s)
        {
            byte[] buff = new byte[2];
            s.Read(buff, 0, 2);
            return BitConverter.ToUInt16(buff, 0);
        }
    }
}
