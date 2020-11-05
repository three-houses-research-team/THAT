using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using G1Tool.IO;
using System;
using System.Linq;
using Ionic.Zlib;

namespace G1Tool.Formats
{
    class GZip
    {
        public static byte[] Decompress(byte[] file)
        {
            using (EndianBinaryReader r = new EndianBinaryReader(new MemoryStream(file), Endianness.Little))
            {
                uint splitSize = r.ReadUInt32();
                uint entryCount = r.ReadUInt32();
                uint uncompSize = r.ReadUInt32();

                uint[] splits = r.ReadUInt32s((int)entryCount);
                byte[] output = new byte[uncompSize];

                r.SeekBegin((r.Position + 0x7F) & ~0x7F); // Align

                using (EndianBinaryWriter w = new EndianBinaryWriter(new MemoryStream(output), Endianness.Little))
                {
                    for (int i = 0; i < entryCount; i++)
                    {
                        uint cur_comp = r.ReadUInt32();
                        if (i == entryCount - 1)
                        {
                            if (cur_comp != splits[i] - 4)
                                w.Write(splits[i]);
                            else
                            {
                                using (System.IO.Compression.GZipStream deflate = new System.IO.Compression.GZipStream(new MemoryStream(r.ReadBytes((int)cur_comp)), System.IO.Compression.CompressionMode.Decompress))
                                    deflate.CopyTo(w.BaseStream);
                            }
                        }
                        else
                        {
                            if (cur_comp == splits[i] - 4)
                            {
                                using (System.IO.Compression.GZipStream deflate = new System.IO.Compression.GZipStream(new MemoryStream(r.ReadBytes((int)cur_comp)), System.IO.Compression.CompressionMode.Decompress))
                                    deflate.CopyTo(w.BaseStream);
                            }
                        }

                        r.SeekBegin(r.Position + 0x7F & ~0x7F); // Align
                    }
                }
                return output;
            }
        }
        public static byte[] Compress(byte[] file)
        {
            int splitSize = 0x10000;
            int partitionCount = ((file.Length - 1) / 0x10000) + 1;

            var partitionList = new List<byte[]>();
            using (var reader = new BinaryReader(new MemoryStream(file)))
            {
                for (int i = 0; i < partitionCount; i++)
                {
                    var encoding = new ZlibCodec();
                    encoding.InitializeDeflate(Ionic.Zlib.CompressionLevel.BestCompression);    // RFC1950/1951/1952 encoding needed
                    byte[] buffer = ZlibStream.CompressBuffer(reader.ReadBytes(splitSize));
                    encoding.EndDeflate();
                    partitionList.Add(buffer);
                }

            }
            using (var ms = new MemoryStream())
            using (var br = new BinaryWriter(ms))
            {
                // Header
                br.Write(splitSize);
                br.Write(partitionCount);
                br.Write(file.Length);  // Uncompressed file length
                for (int i = 0; i < partitionList.Count; i++)
                {
                    br.Write(partitionList[i].Length + 4);  // Entry size (partition size member + partition)
                }
                long currentOffset = br.BaseStream.Position;
                // Entries
                for (int i = 0; i < partitionList.Count; i++)
                {
                    br.BaseStream.Position = currentOffset + 0x7F & ~0x7F; // Aligning
                    br.Write(partitionList[i].Length);
                    br.BaseStream.Write(partitionList[i], 0x0, partitionList[i].Length);
                    currentOffset = br.BaseStream.Position;
                }
                return ms.ToArray();
            }
        }
    }
}