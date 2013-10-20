using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DDSExtractor
{
    class UPK : BinaryFileHandler
    {

        private readonly BinaryWriter writer;

        public UPK(string path) :
            base(path, true)
        {
            writer = new BinaryWriter(fs);
        }

        private string GenerateBackupFileName(string path)
        {
            DateTime now = DateTime.Now;
            return path + "_" + now.Month + "_" + now.Day + "_" + now.Hour + "_" + now.Second + ".bak";
        }

        internal void ReplaceTexture(Texture2D texture, DDSFile dds)
        {
            for (int mipId = 0; mipId < dds.MipmapCount; mipId++)
            {
                Mipmap mipmap = dds.GetMipmap(mipId);
                Texture2D.BulkDataInfo info = texture.FindCompressedDataHeader(mipmap);;
                if (info.Format == 16) {
                    mipmap.Compress();
                }

                if (mipmap.CompressedSize > info.CompressedSize)
                {
                    throw new Exception("New DDS is larger than the old one");
                }

                //fs.Seek(info.OffsetInUpk - 8, SeekOrigin.Begin);
                //writer.Write(Convert.ToInt32(mipmap.CompressedSize));
                fs.Seek(info.OffsetInUpk, SeekOrigin.Begin);
                writer.Write(mipmap.CompressedData);
                writer.Write(info.Footer);
            }
        }
    }
}
