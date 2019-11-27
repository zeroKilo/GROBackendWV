using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DareDebuggerWV
{
    public static class Log
    {
        public static RichTextBox box = null;

        public static void WriteLine(string s, bool onlyToFile = false, object color = null)
        {
            if (box == null) return;
            try
            {
                box.Invoke(new Action(delegate
                {
                    string stamp = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " : ";
                    if (!onlyToFile)
                    {
                        Color c;
                        if (color != null)
                            c = (Color)color;
                        else
                            c = Color.Black;
                        box.SelectionStart = box.TextLength;
                        box.SelectionLength = 0;
                        box.SelectionColor = c;
                        box.AppendText(stamp + s + "\n");
                        box.SelectionColor = box.ForeColor;
                        box.ScrollToCaret();
                    }
                    File.AppendAllText("log.txt", stamp + s + "\n");
                }));
            }
            catch { }
        }
    }
}
