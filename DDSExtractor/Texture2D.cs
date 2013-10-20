using System;
using System.Collections;
using System.IO;

namespace DDSExtractor
{
    class Texture2D : BinaryFileHandler
    {

        /// <summary>
        /// Magic number indication unreal package or compressed chunk
        /// </summary>
        const int HEADER_MAGIC = -1641380927;

        #region local variables
        private System.Collections.Hashtable nameTable;
        private System.Collections.Hashtable property = new Hashtable();

        #endregion

        #region public properties
        /*
        PixelFormat:

        PF_A16B16G16R16,
        PF_A16B16G16R16F,
        PF_A2B10G10R10,
        PF_A32B32G32R32F,
        PF_A8R8G8B8,
        PF_D24,
        PF_FloatRGBA_Full,
        PF_DEPCRECATED_FloatRGBA_Full,
        PF_DepthStencil,
        PF_DXT1,                            // ok
        PF_DXT3,                            // ok
        PF_DXT5,                            // ok
        PF_FilteredShadowDepth,
        PF_FloatRGB,
        PF_G16,
        PF_G16R16,
        PF_G16R16F,
        PF_G32R32F,
        PF_G8,                              // ok
        PF_MAX,
        PF_NormalMap_HQ,                    // ok, 3dc
        PF_NormalMap_LQ,
        PF_R32F,
        PF_ShadowDepth,
        PF_Unknown,
        PF_UYVY
        */

        /* 
        Texture Compression
        
        TC_Default,
        TC_Normalmap,
        TC_Displacementmap,
        TC_NormalmapAlpha,
        TC_NormalmapHQ,
        TC_Grayscale,
        TC_HighDynamicRange,
        TC_MAX,
        TC_TakeDown
        */

        /*
        Texture Filter

        TF_Nearest,
        TF_Linear,
        TF_MAX
        */

        public struct BulkDataInfo
        {
            public int Format;
            public int UncompressedSize;
            public int CompressedSize;
            public int OffsetToData;
            public int Width;
            public int Height;
            public long OffsetInUpk;
            public byte[] Footer;

            public override string ToString()
            {
                string formatstring;
                if (Format == 0) formatstring = "uncompressed";
                else if (Format == 16) formatstring = "compressed";
                else if (Format == 17) formatstring = "no data?";
                else if (Format == 33) formatstring = "no data";
                else formatstring = "?";
                return Format.ToString() + "(" + formatstring + "):" + Width.ToString() + "x" + Height.ToString() + ",off:" + OffsetToData.ToString() + ",cs:" + CompressedSize.ToString() + ",uc:" + UncompressedSize.ToString();
            }
        }

        private BulkDataInfo[] bulk;

        private bool m_hasExportableData = false;

        public BulkDataInfo[] BulkData
        {
            get { return bulk; }
        }

        private long startOfRawData;

        public uint Width
        {
            get
            {
                return Convert.ToUInt32(property["SizeX"]);
            }
        }

        public uint Height
        {
            get
            {
                return Convert.ToUInt32(property["SizeY"]);
            }
        }

        public long FirstOffsetInUpk
        {
            get
            {
                return bulk[0].OffsetInUpk;
            }
        }


        #endregion

        
        public Texture2D(System.Collections.Hashtable nameTable, string path) :
            base(path)
        {
            this.nameTable = nameTable;

            GetAllProperties();
            GetBulkDataInfo();
        }

        private void GetAllProperties()
        {
            const int startOffset = 4;

            fs.Seek(startOffset, SeekOrigin.Begin);

            while (addProperty())
            {
            }

            startOfRawData = fs.Position;
        }

        private string ResolveName(int index)
        {
            return (string)nameTable[index];
        }

        private string ResolveName(long index)
        {
            return ResolveName((int)index);
        }

        private bool addProperty()
        {
            string name = ResolveName(reader.ReadInt64());
            if (name == "None")
            {
                return false;
            }

            string type = ResolveName(reader.ReadInt64());
            long length = reader.ReadInt64();

            switch (type)
            {
                case "IntProperty":
                    property[name] = reader.ReadInt32();
                    break;

                case "BoolProperty":
                    property[name] = reader.ReadBoolean();
                    break;

                case "ByteProperty":
                    if (length == 8)
                    {
                        reader.ReadInt64(); // skip category
                        property[name] = ResolveName(reader.ReadInt64());
                    }
                    else if (length == 1)
                    {
                        property[name] = reader.ReadByte();
                    }
                    break;

                default:
                    // throw new Exception("Unsupported type " + type);
                    break;

            }

            return true;
        }


        private byte[] GetExportableBulkData(int index)
        {
            if (m_hasExportableData && (bulk[index].Format == 0 || bulk[index].Format == 16))
            {
                byte[] returnData;
                if (bulk[index].Format == 16)
                {
                    using (MemoryStream stream = new MemoryStream(bulk[index].UncompressedSize))
                    {
                        DecompressLZOChunk(fs, stream, bulk[index].OffsetToData, bulk[index].CompressedSize, 0, bulk[index].UncompressedSize);
                        returnData = stream.GetBuffer();
                    }
                    //stream.Close();
                    //stream = null;
                }
                else
                {
                    returnData = new byte[bulk[index].UncompressedSize];
                    fs.Seek(bulk[index].OffsetToData, SeekOrigin.Begin);
                    fs.Read(returnData, 0, returnData.Length);
                }
                return returnData;
            }
            else
            {
                return null;
            }
        }

        public int[] GetExportableBulkDataIndices()
        {
            if (m_hasExportableData)
            {
                ArrayList indices = new ArrayList();
                for (int i = 0; i < bulk.Length; i++)
                {
                    if (bulk[i].Format == 0 || bulk[i].Format == 16) indices.Add(i);
                }
                return (int[])indices.ToArray(typeof(int));
            }
            return null;
        }

        private void GetBulkDataInfo()
        {

            fs.Seek(startOfRawData + 12, SeekOrigin.Begin);
            int blockOffset = reader.ReadInt32();
            int numberOfBlocks = reader.ReadInt32();
            if (numberOfBlocks > 0)
            {
                bulk = new BulkDataInfo[numberOfBlocks];
                for (int i = 0; i < numberOfBlocks; i++)
                {
                    bulk[i].Format = reader.ReadInt32();
                    if (bulk[i].Format == 0 || bulk[i].Format == 16)
                    {
                        bulk[i].UncompressedSize = reader.ReadInt32();
                        bulk[i].CompressedSize = reader.ReadInt32();
                        bulk[i].OffsetInUpk = reader.ReadInt32(); // offset to data in original file
                        bulk[i].OffsetToData = (int)fs.Position;
                        fs.Seek(bulk[i].CompressedSize, SeekOrigin.Current);
                        bulk[i].Footer = reader.ReadBytes(8);
                        bulk[i].Width = BitConverter.ToInt32(bulk[i].Footer, 0);
                        bulk[i].Height = BitConverter.ToInt32(bulk[i].Footer, 4);
                        m_hasExportableData = true;
                    }
                    else if (bulk[i].Format == 17 || bulk[i].Format == 33)
                    {
                        bulk[i].UncompressedSize = reader.ReadInt32();
                        bulk[i].CompressedSize = reader.ReadInt32();
                        bulk[i].OffsetInUpk = reader.ReadInt32(); // offset to data in original file
                        bulk[i].OffsetToData = (int)fs.Position;
                        bulk[i].Width = reader.ReadInt32();
                        bulk[i].Height = reader.ReadInt32();
                    }
                    else
                    {
                        throw new Exception("ERROR Texture2D.BulkData: unknown format (" + bulk[i].Format.ToString() + ")");
                    }
                }
            }
        }

        internal void Save()
        {
            string ddsFileName = path.Substring(0, path.Length - ".Texture2D".Length) + ".dds";
            DDSFile file = new DDSFile();

            int[] mips = GetExportableBulkDataIndices();

            string format = (string)property["Format"];
            file.SetPixelFormat(format);
            file.SetSizeAndMipmapCount(Width, Height, (uint)mips.Length);

            for (int i = 0; i < mips.Length; i++)
            {
                file.SetData(GetExportableBulkData(mips[i]), i);
            }

            file.WriteFile(ddsFileName);
        }

        public static void DecompressLZOChunk(FileStream sourceStream, Stream outStream, int coffset, int csize, int uoffset, int usize)
        {

            if (outStream.Length != uoffset)
            {
                throw new Exception("ERROR: wrong size before decompression:" + uoffset.ToString() + ":" + outStream.Position.ToString());
            }

            sourceStream.Seek(coffset, SeekOrigin.Begin);

            byte[] buffer = new byte[8];
            sourceStream.Read(buffer, 0, 8);

            int header = BitConverter.ToInt32(buffer, 0);
            if (header != HEADER_MAGIC)
            {
                throw new Exception("ERROR: invalid compression header.");
            }

            uint blockSize = BitConverter.ToUInt32(buffer, 4);

            if (blockSize != 0x20000)
            {
                throw new Exception("Warning: blockSize " + blockSize + " != 0x20000");
            }

            sourceStream.Read(buffer, 0, 8);
            uint compressedSize = BitConverter.ToUInt32(buffer, 0);
            uint uncompressedSize = BitConverter.ToUInt32(buffer, 4);

            int numBlocks = (int)Math.Ceiling((float)uncompressedSize / (float)blockSize);

            if (numBlocks != 1)
            {
                throw new Exception("Warning: numBlocks " + numBlocks + " != 1");
            }

            long offset = 16 + numBlocks * 8;
            for (int i = 0; i < numBlocks; i++)
            {
                sourceStream.Seek(coffset + 16 + i * 8, SeekOrigin.Begin);
                sourceStream.Read(buffer, 0, 8);
                uint compressedSize2 = BitConverter.ToUInt32(buffer, 0);
                uint uncompressedSize2 = BitConverter.ToUInt32(buffer, 4);
                byte[] uncompressed = new byte[uncompressedSize2];
                byte[] compressed = new byte[compressedSize2];
                sourceStream.Seek(coffset + offset, SeekOrigin.Begin);
                sourceStream.Read(compressed, 0, (int)compressedSize2);

                byte[] recompressed, uncompressed2 = new byte[uncompressedSize2];
                try
                {
                    ManagedLZO.MiniLZO.Decompress(compressed, uncompressed);

                    ManagedLZO.MiniLZO.Compress(uncompressed, out recompressed);
                    ManagedLZO.MiniLZO.Decompress(recompressed, uncompressed2);
                    for (int j = 0; j < uncompressed.Length; j++)
                    {
                        if (uncompressed[j] != uncompressed2[j])
                        {
                            throw new Exception("Failed to recompress");
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                outStream.Write(uncompressed, 0, uncompressed.Length);

                offset += compressedSize2;
            }
            if (offset != csize)
            {
                throw new Exception("ERROR: :" + csize.ToString() + ":" + offset.ToString());
            }

            if (outStream.Length != uoffset + usize)
            {
                throw new Exception("ERROR: wrong size after decompression:" + (uoffset + usize).ToString() + ":" + outStream.Position.ToString());
            }
        }

        internal BulkDataInfo FindCompressedDataHeader(Mipmap mipmap)
        {
            BulkDataInfo info = bulk[mipmap.MipIdx];
            if (info.Width == mipmap.Width && info.Height == mipmap.Height
                || info.Width == 4 && mipmap.Width < 4 && info.Height == 4 && mipmap.Height < 4)
            {
                return info;
            }
            throw new Exception("BulkDataInfo for specified mipmap was not found.");
        }
    }
}
