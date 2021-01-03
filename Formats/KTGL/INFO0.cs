using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace THAT.Formats.KTGL
{
    class INFO0
    {
        public class INFO0Entry
        {
            public long EntryID { get; set; }
            public long DecompressedSize { get; set; }
            public long CompressedSize { get; set; }
            public long IsCompressed { get; set; }
            public string FileName { get; set; }
            public byte[] Padding { get; set; }

            public void Read(EndianBinaryReader info)
            {
                EntryID = info.ReadInt64();
                DecompressedSize = info.ReadInt64();
                CompressedSize = info.ReadInt64();
                IsCompressed = info.ReadInt64();
                FileName = info.ReadString(StringBinaryFormat.FixedLength, 256);
            }

            public void Write(EndianBinaryWriter info)
            {
                info.Write(EntryID);
                info.Write(DecompressedSize);
                info.Write(CompressedSize);
                info.Write(IsCompressed);
                info.Write(FileName);
                info.Align(288);
            }
        }

        public class Info0
        {
            public long numOfEntries { get; set; }
            public INFO0Entry INFO0Entry { get; set; }
            public List<INFO0Entry> INFO0Entries { get; set; }

            public void ReadData(string info0_path)
            {
                using (EndianBinaryReader info0 = new EndianBinaryReader(info0_path, Endianness.Little))
                {
                    
                    numOfEntries = info0.Length / 288;
                    if (numOfEntries > 0)
                    {
                        INFO0Entries = new List<INFO0Entry>();
                        for (int i = 0; i < numOfEntries; i++)
                        {
                            INFO0Entry = new INFO0Entry();
                            INFO0Entry.Read(info0);
                            INFO0Entries.Add(INFO0Entry);
                        }
                    }
                }
            }
        }

    }
}
