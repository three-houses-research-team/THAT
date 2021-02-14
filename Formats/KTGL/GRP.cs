using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THAT.Formats.KTGL
{
    class GRP
    {
        public int Hash { get; set; }
        public int Unk1 { get; set; }
        public int SM53Count { get; set; }
        public int SM61Count { get; set; }
        public int Unk2 { get; set; }
        public int MeshesCount1 { get; set; }
        public int MeshesCount2 { get; set; }
        public int Unk3 { get; set; }

        public void Read(EndianBinaryReader grp)
        {
            Hash = grp.ReadInt32();
            Unk1 = grp.ReadInt32();
            SM53Count = grp.ReadInt32();
            SM61Count = grp.ReadInt32();
            Unk2 = grp.ReadInt32();
            MeshesCount1 = grp.ReadInt32();
            MeshesCount2 = grp.ReadInt32();
            Unk3 = grp.ReadInt32();
        }

        public void Write(EndianBinaryWriter grp)
        {
            grp.WriteInt32(Hash);
            grp.WriteInt32(Unk1);
            grp.WriteInt32(SM53Count);
            grp.WriteInt32(SM61Count);
            grp.WriteInt32(Unk2);
            grp.WriteInt32(MeshesCount1);
            grp.WriteInt32(MeshesCount2);
            grp.WriteInt32(Unk3);
        }
    }
}
