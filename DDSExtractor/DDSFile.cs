using System;
using System.IO;
using System.Runtime.InteropServices;

namespace DDSExtractor
{

    public class DDSFile
    {

        public enum FOUR_CC : uint
        {
            None = 0,
            DXT1 = 827611204,
            DXT2 = 844388420,
            DXT3 = 861165636,
            DXT4 = 877942852,
            DXT5 = 894720068,
            ATI2 = 843666497,
            A16B16G16R16 = 0x00000024,
            G16R16F = 0x00000070,
            A16B16G16R16F = 0x00000071,
            R32F = 0x00000072,
            G32R32F = 0x00000073,
            A32B32G32R32F = 0x00000074,
        }

        [Flags]
        public enum DDSD_FLAGS : uint
        {
            None = 0,
            DDSD_CAPS = 0x00000001,
            DDSD_HEIGHT = 0x00000002,
            DDSD_WIDTH = 0x00000004,
            DDSD_PITCH = 0x00000008,
            DDSD_BACKBUFFERCOUNT = 0x00000020,
            DDSD_ZBUFFERBITDEPTH = 0x00000040,
            DDSD_ALPHABITDEPTH = 0x00000080,
            DDSD_LPSURFACE = 0x00000800,
            DDSD_PIXELFORMAT = 0x00001000,
            DDSD_CKDESTOVERLAY = 0x00002000,
            DDSD_CKDESTBLT = 0x00004000,
            DDSD_CKSRCOVERLAY = 0x00008000,
            DDSD_CKSRCBLT = 0x00010000,
            DDSD_MIPMAPCOUNT = 0x00020000,
            DDSD_REFRESHRATE = 0x00040000,
            DDSD_LINEARSIZE = 0x00080000,
            DDSD_TEXTURESTAGE = 0x00100000,
            DDSD_FVF = 0x00200000,
            DDSD_SRCVBHANDLE = 0x00400000,
            DDSD_DEPTH = 0x00800000,
            DDSD_ALL = 0x00fff9ee
        }

        [Flags]
        public enum DDPF_FLAGS : uint
        {
            None = 0,
            DDPF_ALPHAPIXELS = 0x00000001,
            DDPF_ALPHA = 0x00000002,
            DDPF_FOURCC = 0x00000004,
            DDPF_PALETTEINDEXED4 = 0x00000008,
            DDPF_PALETTEINDEXEDTO8 = 0x00000010,
            DDPF_PALETTEINDEXED8 = 0x00000020,
            DDPF_RGB = 0x00000040,
            DDPF_COMPRESSED = 0x00000080,
            DDPF_RGBTOYUV = 0x00000100,
            DDPF_YUV = 0x00000200,
            DDPF_ZBUFFER = 0x00000400,
            DDPF_PALETTEINDEXED1 = 0x00000800,
            DDPF_PALETTEINDEXED2 = 0x00001000,
            DDPF_ZPIXELS = 0x00002000,
            DDPF_STENCILBUFFER = 0x00004000,
            DDPF_ALPHAPREMULT = 0x00008000,
            DDPF_LUMINANCE = 0x00020000,
            DDPF_BUMPLUMINANCE = 0x00040000,
            DDPF_BUMPDUDV = 0x00080000
        }

        [Flags]
        public enum DDSCAPS_FLAGS : uint
        {
            None = 0,
            //DDSCAPS_PRIMARYSURFACELEFT = 0x00000000,
            DDSCAPS_RESERVED1 = 0x00000001,
            DDSCAPS_ALPHA = 0x00000002,
            DDSCAPS_BACKBUFFER = 0x00000004,
            DDSCAPS_COMPLEX = 0x00000008,
            DDSCAPS_FLIP = 0x00000010,
            DDSCAPS_FRONTBUFFER = 0x00000020,
            DDSCAPS_OFFSCREENPLAIN = 0x00000040,
            DDSCAPS_OVERLAY = 0x00000080,
            DDSCAPS_PALETTE = 0x00000100,
            DDSCAPS_PRIMARYSURFACE = 0x00000200,
            DDSCAPS_RESERVED3 = 0x00000400,
            DDSCAPS_SYSTEMMEMORY = 0x00000800,
            DDSCAPS_TEXTURE = 0x00001000,
            DDSCAPS_3DDEVICE = 0x00002000,
            DDSCAPS_VIDEOMEMORY = 0x00004000,
            DDSCAPS_VISIBLE = 0x00008000,
            DDSCAPS_WRITEONLY = 0x00010000,
            DDSCAPS_ZBUFFER = 0x00020000,
            DDSCAPS_OWNDC = 0x00040000,
            DDSCAPS_LIVEVIDEO = 0x00080000,
            DDSCAPS_HWCODEC = 0x00100000,
            DDSCAPS_MODEX = 0x00200000,
            DDSCAPS_MIPMAP = 0x00400000,
            DDSCAPS_RESERVED2 = 0x00800000,
            DDSCAPS_ALLOCONLOAD = 0x04000000,
            DDSCAPS_VIDEOPORT = 0x08000000,
            DDSCAPS_LOCALVIDMEM = 0x10000000,
            DDSCAPS_NONLOCALVIDMEM = 0x20000000,
            DDSCAPS_STANDARDVGAMODE = 0x40000000,
            DDSCAPS_OPTIMIZED = 0x80000000
        }

        [Flags]
        public enum DDSCAPS2_FLAGS : uint
        {
            None = 0,
            //DDSCAPS2_HARDWAREDEINTERLACE = 0x00000000,
            DDSCAPS2_RESERVED4 = 0x00000002,
            DDSCAPS2_HINTDYNAMIC = 0x00000004,
            DDSCAPS2_HINTSTATIC = 0x00000008,
            DDSCAPS2_TEXTUREMANAGE = 0x00000010,
            DDSCAPS2_RESERVED1 = 0x00000020,
            DDSCAPS2_RESERVED2 = 0x00000040,
            DDSCAPS2_OPAQUE = 0x00000080,
            DDSCAPS2_HINTANTIALIASING = 0x00000100,
            DDSCAPS2_CUBEMAP = 0x00000200,
            DDSCAPS2_CUBEMAP_POSITIVEX = 0x00000400,
            DDSCAPS2_CUBEMAP_NEGATIVEX = 0x00000800,
            DDSCAPS2_CUBEMAP_POSITIVEY = 0x00001000,
            DDSCAPS2_CUBEMAP_NEGATIVEY = 0x00002000,
            DDSCAPS2_CUBEMAP_POSITIVEZ = 0x00004000,
            DDSCAPS2_CUBEMAP_NEGATIVEZ = 0x00008000,
            DDSCAPS2_CUBEMAP_ALLFACES = 0x0000fc00,
            DDSCAPS2_MIPMAPSUBLEVEL = 0x00010000,
            DDSCAPS2_D3DTEXTUREMANAGE = 0x00020000,
            DDSCAPS2_DONOTPERSIST = 0x00040000,
            DDSCAPS2_STEREOSURFACELEFT = 0x00080000,
            DDSCAPS2_VOLUME = 0x00200000,
            DDSCAPS2_NOTUSERLOCKABLE = 0x00400000,
            DDSCAPS2_POINTS = 0x00800000,
            DDSCAPS2_RTPATCHES = 0x01000000,
            DDSCAPS2_NPATCHES = 0x02000000,
            DDSCAPS2_RESERVED3 = 0x04000000,
            DDSCAPS2_DISCARDBACKBUFFER = 0x10000000,
            DDSCAPS2_ENABLEALPHACHANNEL = 0x20000000,
            DDSCAPS2_EXTENDEDFORMATPRIMARY = 0x40000000,
            DDSCAPS2_ADDITIONALPRIMARY = 0x80000000
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct DDPIXELFORMAT
        {
            public uint dwSize;
            public DDPF_FLAGS dwFlags;
            public FOUR_CC dwFourCC;
            public uint dwRGBBitCount;
            public uint dwRBitMask;
            public uint dwGBitMask;
            public uint dwBBitMask;
            public uint dwAlphaBitMask;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct DDSCAPS2
        {
            public DDSCAPS_FLAGS dwCaps1;
            public DDSCAPS2_FLAGS dwCaps2;
            public uint dwDDSX;
            public uint dwReserved;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct DDSHEAD
        {
            public uint dwMagic;
            public uint dwSize;
            [MarshalAs(UnmanagedType.U4)]
            public DDSD_FLAGS dwFlags;
            public uint dwHeight;
            public uint dwWidth;
            public uint dwPitchOrLinearSize;
            public uint dwDepth;
            public uint dwMipMapCount;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 11)]
            public uint[] dwReserved1;          // 11

            //[MarshalAs( UnmanagedType.Struct, SizeConst=8)]
            public DDPIXELFORMAT sPixelFormat;
            //[MarshalAs( UnmanagedType.Struct, SizeConst = 4 )]
            public DDSCAPS2 sCaps;
            public uint dwReserved2;
        }

        public DDSHEAD header;
        private byte[] mipmapData;

        public uint MipmapCount { get { return header.dwMipMapCount; } }

        private uint SizeFactor
        {
            get
            {
                switch (header.sPixelFormat.dwFourCC)
                {
                    case FOUR_CC.DXT1:
                        return 4;

                    case FOUR_CC.DXT3:
                    case FOUR_CC.DXT5:
                    case FOUR_CC.ATI2:
                        return 8;
                    
                    case FOUR_CC.R32F:
                    case FOUR_CC.G16R16F:
                        return 32;
                    
                    case FOUR_CC.G32R32F:
                    case FOUR_CC.A16B16G16R16:
                    case FOUR_CC.A16B16G16R16F:
                        return 64;
                    
                    case FOUR_CC.A32B32G32R32F:
                        return 128;

                    case FOUR_CC.None:
                        return header.sPixelFormat.dwRGBBitCount;

                    default:
                        throw new Exception("Unknown pixel format");
                }
            }
        }

        public uint[] RequiredMipSizes 
        {
            get
            {
                uint mc = header.dwMipMapCount;
                uint[] sizes = new uint[mc];
                uint _w = header.dwWidth;
                uint _h = header.dwHeight;

                for (int i = 0; i < mc; i++)
                {
                    sizes[i] = (_w * _h * SizeFactor) / 8;
                    if (sizes[i] < SizeFactor * 2) sizes[i] = SizeFactor * 2;
                    _w = _w / 2;
                    _h = _h / 2;
                }

                return sizes;
            }
        }

        public uint RequiredMipSizeAll
        {
            get
            {
                uint sum = 0;
                foreach (uint x in RequiredMipSizes)
                {
                    sum += x;
                }
                return sum;
            }
        }

        public void SetPixelFormat(string format)
        {
            header.sPixelFormat.dwSize = 32;
            header.sPixelFormat.dwRGBBitCount = 0;
            header.sPixelFormat.dwRBitMask = 0;
            header.sPixelFormat.dwGBitMask = 0;
            header.sPixelFormat.dwBBitMask = 0;
            header.sPixelFormat.dwAlphaBitMask = 0;
            switch (format)
            {
                case "PF_DXT1":
                    header.sPixelFormat.dwFourCC = FOUR_CC.DXT1;
                    header.sPixelFormat.dwFlags = DDPF_FLAGS.DDPF_FOURCC;
                    break;
                case "PF_DXT3":
                    header.sPixelFormat.dwFourCC = FOUR_CC.DXT3;
                    header.sPixelFormat.dwFlags = DDPF_FLAGS.DDPF_FOURCC;
                    break;
                case "PF_DXT5":
                    header.sPixelFormat.dwFourCC = FOUR_CC.DXT5;
                    header.sPixelFormat.dwFlags = DDPF_FLAGS.DDPF_FOURCC;
                    break;
                case "PF_NormalMap_HQ":
                    header.sPixelFormat.dwFourCC = FOUR_CC.ATI2;
                    header.sPixelFormat.dwFlags = DDPF_FLAGS.DDPF_FOURCC;
                    break;
                case "PF_R32F":
                    header.sPixelFormat.dwFourCC = FOUR_CC.R32F;
                    header.sPixelFormat.dwFlags = DDPF_FLAGS.DDPF_FOURCC;
                    break;
                case "PF_G32R32F":
                    header.sPixelFormat.dwFourCC = FOUR_CC.G32R32F;
                    header.sPixelFormat.dwFlags = DDPF_FLAGS.DDPF_FOURCC;
                    break;
                case "PF_G16R16F":
                    header.sPixelFormat.dwFourCC = FOUR_CC.G16R16F;
                    header.sPixelFormat.dwFlags = DDPF_FLAGS.DDPF_FOURCC;
                    break;
                case "PF_A16B16G16R16":
                    header.sPixelFormat.dwFourCC = FOUR_CC.A16B16G16R16;
                    header.sPixelFormat.dwFlags = DDPF_FLAGS.DDPF_FOURCC;
                    break;
                case "PF_A16B16G16R16F":
                    header.sPixelFormat.dwFourCC = FOUR_CC.A16B16G16R16F;
                    header.sPixelFormat.dwFlags = DDPF_FLAGS.DDPF_FOURCC;
                    break;
                case "PF_A32B32G32R32F":
                    header.sPixelFormat.dwFourCC = FOUR_CC.A32B32G32R32F;
                    header.sPixelFormat.dwFlags = DDPF_FLAGS.DDPF_FOURCC;
                    break;
                case "PF_A8R8G8B8": // fourcc 21
                    // SizeFactor = 32;
                    header.sPixelFormat.dwRGBBitCount = 32;
                    header.sPixelFormat.dwRBitMask = 0x00ff0000;
                    header.sPixelFormat.dwGBitMask = 0x0000ff00;
                    header.sPixelFormat.dwBBitMask = 0x000000ff;
                    header.sPixelFormat.dwAlphaBitMask = 0xff000000;
                    header.sPixelFormat.dwFourCC = FOUR_CC.None;
                    header.sPixelFormat.dwFlags = (DDPF_FLAGS.DDPF_RGB | DDPF_FLAGS.DDPF_ALPHAPIXELS);
                    break;
                case "PF_G8":      // L8, fourcc 50
                    // SizeFactor = 8;
                    header.sPixelFormat.dwRGBBitCount = 8;
                    header.sPixelFormat.dwRBitMask = 0x000000ff;
                    header.sPixelFormat.dwGBitMask = 0;
                    header.sPixelFormat.dwBBitMask = 0;
                    header.sPixelFormat.dwAlphaBitMask = 0;
                    header.sPixelFormat.dwFourCC = FOUR_CC.None;
                    header.sPixelFormat.dwFlags = DDPF_FLAGS.DDPF_LUMINANCE;
                    break;
                case "PF_G16":      // L16, fourcc 81
                    // SizeFactor = 16;
                    header.sPixelFormat.dwRGBBitCount = 16;
                    header.sPixelFormat.dwRBitMask = 0x0000ffff;
                    header.sPixelFormat.dwGBitMask = 0;
                    header.sPixelFormat.dwBBitMask = 0;
                    header.sPixelFormat.dwAlphaBitMask = 0;
                    header.sPixelFormat.dwFourCC = FOUR_CC.None;
                    header.sPixelFormat.dwFlags = DDPF_FLAGS.DDPF_LUMINANCE;
                    break;
                case "PF_G16R16":      // fourcc 34
                    // SizeFactor = 32;
                    header.sPixelFormat.dwRGBBitCount = 32;
                    header.sPixelFormat.dwRBitMask = 0x0000ffff;
                    header.sPixelFormat.dwGBitMask = 0xffff0000;
                    header.sPixelFormat.dwBBitMask = 0;
                    header.sPixelFormat.dwAlphaBitMask = 0;
                    header.sPixelFormat.dwFourCC = FOUR_CC.None;
                    header.sPixelFormat.dwFlags = DDPF_FLAGS.DDPF_RGB;
                    break;
                default:
                    /* not implemented
                     * 
                     * PF_Unknown,         
                     * fourcc 1498831189  PF_UYVY,    //UYVY format (PC98 compliance).
                     * PF_FloatRGB,		// A RGB FP format with platform-specific implementation, for use with render targets
                     * PF_FloatRGBA,		// A RGBA FP format with platform-specific implementation, for use with render targets
                     * PF_DepthStencil,	// A depth+stencil format with platform-specific implementation, for use with render targets
                     * PF_ShadowDepth,		// A depth format with platform-specific implementation, for use with render targets
                     * PF_FilteredShadowDepth, // A depth format with platform-specific implementation, that can be filtered by hardware
                     * PF_FloatRGBA_Full
                     * PF_DEPRECATED_FloatRGBA_Full
                     * fourcc 31 PF_A2B10G10R10, // A 32-bit pixel format that uses 10 bits for each color and 2 bits for alpha.
                     * PF_D24
                     * PF_NormalMap_LQ
                    */
                    throw new Exception("Not implemented or unknown format.");
            }
        }

        public void SetSizeAndMipmapCount(uint w, uint h, uint mc)
        {
            header.dwHeight = h;
            header.dwWidth = w;
            header.dwMipMapCount = mc;

            // The number of bytes per scan line in an uncompressed texture; the total number of bytes in the top level texture for a compressed texture. The pitch must be DWORD aligned.
            // DDSD_LINEARSIZE = Required when pitch is provided for a compressed texture.
            header.dwFlags |= DDSD_FLAGS.DDSD_LINEARSIZE;
            header.dwPitchOrLinearSize = (w * h * SizeFactor) / 8;


            // DDSCAPS_TEXTURE = Required            
            // DDSCAPS_COMPLEX = Optional; must be used on any file that contains more than one surface (a mipmap, a cubic environment map, or volume texture).
            // DDSCAPS_MIPMAP = Optional; should be used for a mipmap.
            header.sCaps.dwCaps1 = (DDSCAPS_FLAGS.DDSCAPS_TEXTURE | DDSCAPS_FLAGS.DDSCAPS_MIPMAP | DDSCAPS_FLAGS.DDSCAPS_COMPLEX);
        }

        public void SetData(byte[] data, int mipidx)
        {
            if (mipmapData == null || mipmapData.Length != RequiredMipSizeAll) mipmapData = new byte[RequiredMipSizeAll];
            if (RequiredMipSizes == null || mipidx > RequiredMipSizes.Length - 1)
            {
                throw new Exception("cannot set data...");
            }
            int size = (int)RequiredMipSizes[mipidx];

            if (data == null)
            {
                data = new byte[size];
            }
            else if (data.Length != size)
            {
                throw new Exception("data has wrong length...");
            }

            int offset = 0;

            for (int i = 0; i < mipidx; i++)
            {
                offset += (int)RequiredMipSizes[i];
            }
            Buffer.BlockCopy(data, 0, mipmapData, offset, size);
        }

        internal Mipmap GetMipmap(int mipidx)
        {
            if (RequiredMipSizes == null 
                || mipidx > RequiredMipSizes.Length - 1
                || mipmapData == null
                || mipmapData.Length != RequiredMipSizeAll)
            {
                throw new Exception("Precondition is violated");
            }

            int size = (int)RequiredMipSizes[mipidx];
            int offset = 0;
            for (int i = 0; i < mipidx; i++)
            {
                offset += (int)RequiredMipSizes[i];
            }
            byte[] data = new byte[size];
            Buffer.BlockCopy(mipmapData, offset, data, 0, Convert.ToInt32(RequiredMipSizes[mipidx]));
            return new Mipmap(Convert.ToInt32(header.dwWidth), Convert.ToInt32(header.dwHeight), mipidx, size, data);
        }

        public void SetDepth(uint depth)
        {
            header.dwDepth = depth;
            if (depth > 0)
            {
                // DDSD_DEPTH = Required in a depth texture
                header.dwFlags |= DDSD_FLAGS.DDSD_DEPTH;
            }
        }

        public void SetSpecialTexture(DDSCAPS2_FLAGS flag)
        {
            //DDSCAPS2_CUBEMAP = Required for a cube map.
            //DDSCAPS2_CUBEMAP_POSITIVEX, DDSCAPS2_CUBEMAP_NEGATIVEX, DDSCAPS2_CUBEMAP_POSITIVEY, DDSCAPS2_CUBEMAP_NEGATIVEY, DDSCAPS2_CUBEMAP_POSITIVEZ, DDSCAPS2_CUBEMAP_NEGATIVEZ = Required when these surfaces are stored in a cube map.
            //DDSCAPS2_VOLUME = Required for a volume texture.
            if (flag == DDSCAPS2_FLAGS.DDSCAPS2_VOLUME)
            {
                header.sCaps.dwCaps2 = DDSCAPS2_FLAGS.DDSCAPS2_VOLUME;
            }
            else if (flag == DDSCAPS2_FLAGS.DDSCAPS2_CUBEMAP)
            {
                header.sCaps.dwCaps2 = (DDSCAPS2_FLAGS.DDSCAPS2_CUBEMAP | DDSCAPS2_FLAGS.DDSCAPS2_CUBEMAP_POSITIVEX | DDSCAPS2_FLAGS.DDSCAPS2_CUBEMAP_NEGATIVEX | DDSCAPS2_FLAGS.DDSCAPS2_CUBEMAP_POSITIVEY | DDSCAPS2_FLAGS.DDSCAPS2_CUBEMAP_NEGATIVEY | DDSCAPS2_FLAGS.DDSCAPS2_CUBEMAP_POSITIVEZ | DDSCAPS2_FLAGS.DDSCAPS2_CUBEMAP_NEGATIVEZ);
            }
        }

        public DDSFile()
        {
            header.dwMagic = 0x20534444; // DDS
            header.dwSize = 124;
            header.dwFlags = (DDSD_FLAGS.DDSD_CAPS | DDSD_FLAGS.DDSD_WIDTH | DDSD_FLAGS.DDSD_HEIGHT | DDSD_FLAGS.DDSD_PIXELFORMAT | DDSD_FLAGS.DDSD_MIPMAPCOUNT);
        }

        public void WriteFile(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            byte[] buf = new byte[Marshal.SizeOf(typeof(DDSHEAD))];
            buf = RawSerialize(header);
            fs.Write(buf, 0, buf.Length);
            fs.Write(mipmapData, 0, mipmapData.Length);
            fs.Flush();
            fs.Close();
            fs.Dispose();
            fs = null;
        }

        public static byte[] RawSerialize(object anything)
        {
            int rawsize = Marshal.SizeOf(anything);
            IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.StructureToPtr(anything, buffer, false);
            byte[] rawdatas = new byte[rawsize];
            Marshal.Copy(buffer, rawdatas, 0, rawsize);
            Marshal.FreeHGlobal(buffer);
            return rawdatas;
        }

        public static object RawDeserialize(byte[] rawdatas, Type anytype)
        {
            int rawsize = Marshal.SizeOf(anytype);
            if (rawsize > rawdatas.Length)
                return null;
            IntPtr buffer = Marshal.AllocHGlobal(rawsize);
            Marshal.Copy(rawdatas, 0, buffer, rawsize);
            object retobj = Marshal.PtrToStructure(buffer, anytype);
            Marshal.FreeHGlobal(buffer);
            return retobj;
        }

        public static DDSFile ReadFile(string fileName)
        {
            DDSFile file = new DDSFile();

            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read)) {

                byte[] buf = new byte[Marshal.SizeOf(typeof(DDSHEAD))];
                int length = fs.Read(buf, 0, Marshal.SizeOf(typeof(DDSHEAD)));
                if (length != Marshal.SizeOf(typeof(DDSHEAD)))
                {
                    throw new Exception("Could not read the whole header.");
                }
                file.header = (DDSHEAD)RawDeserialize(buf, typeof(DDSHEAD));
                
                file.SetSizeAndMipmapCount(file.header.dwWidth, file.header.dwHeight, file.header.dwMipMapCount);
                file.mipmapData = new byte[file.RequiredMipSizeAll];
                length = fs.Read(file.mipmapData, 0, Convert.ToInt32(file.RequiredMipSizeAll));
            
                if (length != file.RequiredMipSizeAll)
                {
                    throw new Exception("Could not read the whole mipmaps.");
                }

                if (fs.Length != fs.Position) 
                {
                    throw new Exception("There are rest bytes");
                }
            }

            return file;
        }
    }
}
