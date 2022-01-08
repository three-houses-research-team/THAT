using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace G1Tool.Formats
{
    public class Bin
    {
        public static List<byte[]> FileList { get; set; }
        public static void Read(string path)
        {
            using (FileStream memoryStream = new FileStream(path, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(memoryStream))
            {
                int numFiles = reader.ReadInt32();
                FileList = new List<byte[]>();
                for (int index = 0; index < numFiles; index++)
                {
                    reader.BaseStream.Position = 4 + (0x8 * index);
                    int fileOffset = reader.ReadInt32();
                    int fileSize = reader.ReadInt32();
                    byte[] buffer = new byte[fileSize];
                    reader.BaseStream.Position = fileOffset;
                    reader.Read(buffer, 0, fileSize);
                    FileList.Add(buffer);
                }
            }
        }
        public static void Write(List<byte[]> fileList, string path)
        {
            using (var fs = new FileStream(path, FileMode.Create))
            using (var br = new BinaryWriter(fs))
            {
                // Header
                br.Write(fileList.Count);
                int headerSize = 0x4 + fileList.Count * 0x8;
                int fileOffset = headerSize;
                for (int i = 0; i < fileList.Count; i++)
                {
                    br.BaseStream.Position = 0x4 + i * 0x8;
                    br.Write(fileOffset);   // Offset
                    br.Write(fileList[i].Length);   // Size
                    br.BaseStream.Position = fileOffset;
                    br.BaseStream.Write(fileList[i], 0x0, fileList[i].Length);
                    fileOffset = (int)br.BaseStream.Position;
                }
            }
        }

        public static void Extract(string infile, string outfile)
        {
            if (!Directory.Exists(outfile))
            {
                Directory.CreateDirectory(outfile);
            }
            string ext = "";
            using (FileStream memoryStream = new FileStream(infile, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(memoryStream))
            {
                int numFiles = reader.ReadInt32();
                for (int index = 0; index < numFiles; index++)
                {
                    reader.BaseStream.Position = 4 + (0x8 * index);
                    int fileOffset = reader.ReadInt32();
                    int fileSize = reader.ReadInt32();
                    byte[] buffer = new byte[fileSize];
                    reader.BaseStream.Position = fileOffset;
                    reader.Read(buffer, 0, fileSize);
                    if (buffer.Length == 0)
                    {
                        ext = ".misc";
                    }
                    else
                    {
                        ext = GetMagic(buffer);
                    }
                    File.WriteAllBytes(outfile + Path.DirectorySeparatorChar + index + ext, buffer);
                }
            }
        }

        public static void Build(List<byte[]> fileList, string outfile)
        {
            using (var fs = new FileStream(outfile, FileMode.Create))
            using (var br = new BinaryWriter(fs))
            {
                // Header
                br.Write(fileList.Count);
                int headerSize = 0x4 + fileList.Count * 0x8;
                int fileOffset = headerSize;
                for (int i = 0; i < fileList.Count; i++)
                {
                    br.BaseStream.Position = 0x4 + i * 0x8;
                    br.Write(fileOffset);   // Offset
                    br.Write(fileList[i].Length);   // Size
                    br.BaseStream.Position = fileOffset;
                    br.BaseStream.Write(fileList[i], 0x0, fileList[i].Length);
                    fileOffset = (int)br.BaseStream.Position;
                }
            }
        }

        public static string GetMagic(byte[] inbytes)
        {
            if (inbytes == null || inbytes.Length <= 0)
                return ".bin";
            byte[] header = new byte[4];
            Array.Copy(inbytes, 0, header, 0, 4);
            string magic = BitConverter.ToString(header).ToLower();
            if (magic == "47-54-31-47")
            {
                return ".g1t";
            }
            else if (magic == "5f-4d-31-47")
            {
                return ".g1m";
            }
            else if (magic == "42-47-49-52")
            {
                return ".rigb";
            }
            else if (magic == "53-57-47-51")
            {
                return ".swgq";
            }
            else if (magic == "00-19-12-16")
            {
                return ".data";
            }
            else if (magic == "5f-41-32-47")
            {
                return ".g2a";
            }
            else if (magic == "5f-41-31-47")
            {
                return ".g1a";
            }
            else if (magic == "00-00-01-00")
            {
                return ".gz";
            }
            else 
            {
                return ".bin";
            }
        }
    }
}
