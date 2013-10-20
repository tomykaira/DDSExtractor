using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDSExtractor
{
    class Mipmap
    {
        private int mipidx;
        private int size;
        private byte[] data, compressedData = null;
        private int fullHeight;
        private int fullWidth;


        public int CompressedSize
        {
            get
            {
                return CompressedData.Length;
            }
        }

        public int UncompressedSize
        {
            get
            {
                return data.Length;
            }
        }

        public byte[] CompressedData
        {
            get
            {
                if (compressedData == null)
                {
                    return data;
                }
                else
                {
                    return compressedData;
                }
            }
        }

        public int Width { get { return fullWidth >> mipidx; } }

        public int Height { get { return fullHeight >> mipidx; } }

        public int MipIdx { get { return mipidx; } }

        public Mipmap(int width, int height, int mipidx, int size, byte[] data)
        {
            this.fullWidth = width;
            this.fullHeight = height;
            this.mipidx = mipidx;
            this.size = size;
            this.data = data;
        }

        internal void Compress()
        {
            byte [] compressedBody;
            ManagedLZO.MiniLZO.Compress(this.data, out compressedBody);

            byte[] uncompressed = new byte[this.data.Length];
            ManagedLZO.MiniLZO.Decompress(compressedBody, uncompressed);
            for (int j = 0; j < uncompressed.Length; j++)
            {
                if (uncompressed[j] != this.data[j])
                {
                    throw new Exception("Failed to recompress");
                }
            }

            // magic(4), blockSize(4), compressedSize(4), uncompressedSize(4), compressedSize(4), uncompressedSize(4)
            compressedData = new byte[] {0xC1, 0x83, 0x2a, 0x9e}
                .Concat(BitConverter.GetBytes(0x20000))
                .Concat(BitConverter.GetBytes(compressedBody.Length))
                .Concat(BitConverter.GetBytes(UncompressedSize))
                .Concat(BitConverter.GetBytes(compressedBody.Length))
                .Concat(BitConverter.GetBytes(UncompressedSize))
                .Concat(compressedBody).ToArray();
        }
    }
}
