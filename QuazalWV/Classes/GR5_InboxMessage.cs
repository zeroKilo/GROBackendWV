using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuazalWV
{
    public class GR5_InboxMessage
    {
        public enum TYPE
        {
            /// <summary>
            /// 'Object received.\n Please click the Get Item button to retrieve/apply.'
            /// </summary>
            ItemRetrieved = 1,
            /// <summary>
            /// '%s added you to their friends list. If you would like to add %s to your friends list as well, please click the "Add Friend" button below.'
            /// </summary>
            AddedToFriendList = 2,
            /// <summary>
            /// 'Object received.\n Please click the Get Item button to retrieve/apply.'
            /// </summary>
            ItemRetrieved2 = 3,
            /// <summary>
            /// 'This item has been removed from your inventory.'
            /// </summary>
            ItemRemoved = 4
        }

        public uint Id { get; set; }
        public uint Unk { get; set; }
        public uint SenderId { get; set; }
        public TYPE Type { get; set; }
        public string Text { get; set; }
        public ulong Date { get; set; }
        /// <summary>
        /// Set to 0 if new.
        /// </summary>
        public uint IsNew { get; set; }

        public void toBuffer(Stream s)
        {
            Helper.WriteU32(s, Id);
            Helper.WriteU32(s, Unk);
            Helper.WriteU32(s, SenderId);
            Helper.WriteU32(s, (uint)Type);
            Helper.WriteString(s, Text);
            Helper.WriteU64(s, Date);
            Helper.WriteU32(s, IsNew);
        }
    }
}
