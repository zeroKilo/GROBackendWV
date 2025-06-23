using System;
using System.Windows.Forms;

namespace DDLParserWV
{
    static class Program
    {
        /// <summary>
        /// The entry point for the app.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DDLParserForm());
        }
    }
}
