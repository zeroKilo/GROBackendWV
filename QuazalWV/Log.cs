using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace QuazalWV
{
    public static class Log
    {
        public static RichTextBox box = null;
        public static int MinPriority = 10; //1..10 1=less, 10=all
        public static string logFileName = "log.txt";
        public static readonly object _sync = new object();
        public static readonly object _filesync = new object();
        public static StringBuilder logBuffer = new StringBuilder();

        public static void ClearLog()
        {
            if (File.Exists(logFileName))
                File.Delete(logFileName);
            lock (_sync)
            {
                logBuffer = new StringBuilder();
            }
        }

        public static void WriteLine(int priority, string s, object color = null)
        {
            if (box == null) return;
            try
            {
                box.Invoke(new Action(delegate
                {
                    string stamp = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " : [" + priority.ToString("D2") + "]";
                    if (priority <= MinPriority)
                    {
                        Color c;
                        if (color != null)
                            c = (Color)color;
                        else
                            c = Color.Black;
                        if (s.ToLower().Contains("error"))
                            c = Color.Red;
                        box.SelectionStart = box.TextLength;
                        box.SelectionLength = 0;
                        box.SelectionColor = c;
                        box.AppendText(stamp + s + "\n");
                        box.SelectionColor = box.ForeColor;
                        box.ScrollToCaret();                        
                    }
                    lock (_sync)
                    {
                        logBuffer.Append(stamp + s + "\n");
                        new Thread(tSaveLog).Start();
                    }
                }));
            }
            catch { }
        }

        public static void tSaveLog(object obj)
        {
            lock (_filesync)
            {
                string buffer = null;
                lock (_sync)
                {
                    buffer = logBuffer.ToString();
                    logBuffer.Clear();
                }
                if(buffer != null && buffer.Length > 0)
                    File.AppendAllText(logFileName, buffer);
            }
        }
    }
}
