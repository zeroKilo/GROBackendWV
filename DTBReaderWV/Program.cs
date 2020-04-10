using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTBReaderWV
{
    class Program
    {
        public class Column
        {
            public string Name;
            public uint type;
            public object defaultVal;
            public Column(Stream s)
            {
                Name = ReadString(s);
                type = ReadU32(s);
                switch (type)
                {
                    case 3:
                        defaultVal = ReadString(s);
                        break;
                    case 1:
                    case 4:
                        defaultVal = ReadU32(s);
                        break;
                    default:
                        throw new Exception("Unknown Column Type 0x" + type);
                }
            }

            public string ReadValue(Stream s)
            {
                string result = "";
                switch (type)
                {
                    case 3:
                        result = ReadString(s);
                        break;
                    case 1:
                    case 4:
                        result = "0x" + ReadU32(s).ToString("X8");
                        break;
                    default:
                        throw new Exception("Unknown Column Type 0x" + type);
                }
                return result;
            }
        }

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: DTBReaderWV table.dtb");
                return;
            }
            byte[] buff = File.ReadAllBytes(args[0]);
            MemoryStream m = new MemoryStream(buff);
            uint cols = ReadU32(m);
            uint rows = ReadU32(m);
            List<Column> listCols = new List<Column>();
            for (int i = 0; i < cols; i++)
                listCols.Add(new Column(m));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < cols; i++)
                sb.Append(listCols[i].Name + ";");
            sb.AppendLine();
            for (int r = 0; r < rows; r++)
            {
                for (int i = 0; i < cols; i++)
                    sb.Append(listCols[i].ReadValue(m) + ";");
                sb.AppendLine();
            }
            File.WriteAllText(args[0] + ".csv", sb.ToString());
        }

        public static uint ReadU32(Stream s)
        {
            byte[] buff = new byte[4];
            s.Read(buff, 0, 4);
            return BitConverter.ToUInt32(buff, 0);
        }

        public static string ReadString(Stream s)
        {
            string result = "";
            byte b;
            while ((b = (byte)s.ReadByte()) != 0)
                result += (char)b;
            return result;
        }
    }
}
