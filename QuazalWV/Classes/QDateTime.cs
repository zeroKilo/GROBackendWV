using System;

namespace QuazalWV
{
    public class QDateTime
    {
        public uint Year {  get; set; }
        public byte Month { get; set; }
        public byte Day { get; set; }
        public byte Hour { get; set; }
        public byte Minute { get; set; }
        public byte Second { get; set; }

        public QDateTime(DateTime date)
        {
            Year = (uint)date.Year;
            Month = (byte)date.Month;
            Day = (byte)date.Day;
            Hour = (byte)date.Hour;
            Minute = (byte)date.Minute;
            Second = (byte)date.Second;
        }

        public QDateTime(ulong date)
        {
            Year = (uint)((date >> 26) & 0x3FFF);
            Month = (byte)((date >> 22) & 0xF);
            Day = (byte)((date >> 17) & 0x1F);
            Hour = (byte)((date >> 12) & 0x1F);
            Minute = (byte)((date >> 6) & 0x3F);
            Second = (byte)(date & 0x3F);
        }

        public ulong ToLong()
        {
            ulong date = 0;
            date += (ulong)Year << 26;
            date += (ulong)(Month << 22);
            date += (ulong)(Day << 17);
            date += (ulong)(Hour << 12);
            date += (ulong)(Minute << 6);
            date += Second;
            return date;
        }
    }
}
