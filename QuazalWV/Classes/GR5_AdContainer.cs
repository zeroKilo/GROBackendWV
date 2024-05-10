using System.IO;

namespace QuazalWV
{
    public class GR5_AdContainer
    {
        public uint Id {  get; set; }
        public uint AdServerId { get; set; }
        public string DesignerName { get; set; }
        public byte AdInterval { get; set; }
        public byte ContainerLocation { get; set; }

        public void ToBuffer(Stream s)
        {
            Helper.WriteU32(s, Id);
            Helper.WriteU32(s, AdServerId);
            Helper.WriteString(s, DesignerName);
            Helper.WriteU8(s, AdInterval);
            Helper.WriteU8(s, ContainerLocation);
        }
    }
}
