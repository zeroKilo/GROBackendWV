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
using QuazalWV;

namespace GROBackendWV
{
    public partial class SendNotification : Form
    {
        public SendNotification()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ClientInfo client in Global.clients)
                {
                    NotificationQuene.AddNotification(
                        new NotificationQueneEntry(client, 
                            0,
                            Convert.ToUInt32(textBox1.Text),
                            Convert.ToUInt32(textBox2.Text),
                            Convert.ToUInt32(textBox3.Text),
                            Convert.ToUInt32(textBox4.Text),
                            Convert.ToUInt32(textBox5.Text),
                            Convert.ToUInt32(textBox6.Text),
                            textBox7.Text));
                }
            }
            catch { }
        }
    }
}
