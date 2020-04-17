using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseWeaponService_GetTemplateWeaponMaps : RMCPacketReply
    {
        public List<Map_U32_VectorGR5_Weapon> TemplateWeaponList = new List<Map_U32_VectorGR5_Weapon>();
        public List<Map_U32_VectorU32> WeaponCompatibilityBridge = new List<Map_U32_VectorU32>();
        public List<Map_U32_VectorU32> TemplateComponentLists = new List<Map_U32_VectorU32>();
        public List<Map_U32_VectorGR5_Component> Components = new List<Map_U32_VectorGR5_Component>();

        public RMCPacketResponseWeaponService_GetTemplateWeaponMaps()
        {
            TemplateWeaponList = DBHelper.GetTemplateWeaponList();
            WeaponCompatibilityBridge = DBHelper.GetWeaponCompatibilityBridge();
            TemplateComponentLists = DBHelper.GetTemplateComponentLists();
            Components = DBHelper.GetComponents();
        }

        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)TemplateWeaponList.Count);
            foreach (Map_U32_VectorGR5_Weapon w in TemplateWeaponList)
                w.toBuffer(m);
            Helper.WriteU32(m, (uint)WeaponCompatibilityBridge.Count);
            foreach (Map_U32_VectorU32 u in WeaponCompatibilityBridge)
                u.toBuffer(m);
            Helper.WriteU32(m, (uint)TemplateComponentLists.Count);
            foreach (Map_U32_VectorU32 u in TemplateComponentLists)
                u.toBuffer(m);
            Helper.WriteU32(m, (uint)Components.Count);
            foreach (Map_U32_VectorGR5_Component c in Components)
                c.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseWeaponService_GetTemplateWeaponMaps]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
