using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public static class NotificationQuene
    {
        private static readonly object _sync = new object();
        private static List<NotificationQueneEntry> quene = new List<NotificationQueneEntry>();

        public static void AddNotification(NotificationQueneEntry n)
        {
            lock (_sync)
            {
                quene.Add(n);
            }
        }

        public static void Update()
        {
            lock (_sync)
            {
                for (int i = 0; i < quene.Count; i++)
                {
                    NotificationQueneEntry n = quene[i];
                    if (n.timer.ElapsedMilliseconds > n.timeout)
                    {
                        n.Execute();
                        n.timer.Stop();
                        quene.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }
}
