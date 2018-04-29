// <copyright file="VTex.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.VPK.Content
{
    using System;
    using System.IO;
    using System.Text;

    public class VTex : ResourceEntry
    {
        public Stream DataStream { get; }

        public ushort Depth { get; }

        public ushort Flags { get; }

        public VTexFormat Format { get; }

        public byte NumMipLevels { get; }

        public ushort Height { get; }

        public float[] Reflectivity { get; }

        public ushort Version { get; }

        public ushort Width { get; }

        public VTex(BinaryReader reader)
            : base(reader)
        {
            reader.BaseStream.Position = 0x260;
            var yCoCg = false;
            for (var i = 0; i < 15; i++)
            {
                var encoding = Encoding.UTF8;
                var characterSize = encoding.GetByteCount("e");
                string text;
                using (var ms = new MemoryStream())
                {
                    while (true)
                    {
                        var data = new byte[characterSize];
                        reader.Read(data, 0, characterSize);
                        if (encoding.GetString(data, 0, characterSize) == "\0")
                        {
                            break;
                        }

                        ms.Write(data, 0, data.Length);
                    }

                    text = encoding.GetString(ms.ToArray());
                }

                if (string.IsNullOrEmpty(text))
                {
                    reader.BaseStream.Position += 32;
                    continue;
                }

                if (text == "CompileTexture")
                {
                    continue;
                }

                if(text == "Texture Compiler Version Image YCoCg Conversion")
                {
                    yCoCg = true;
                    break;
                }
            }

            reader.BaseStream.Position = this.StreamStartPosition + this.Offset;

            this.Version = reader.ReadUInt16();
            if (this.Version != 1)
            {
                throw new InvalidDataException($"invalid version {this.Version}");
            }

            this.Flags = reader.ReadUInt16();
            this.Reflectivity = new[] { reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle() };
            this.Width = reader.ReadUInt16();
            this.Height = reader.ReadUInt16();
            this.Depth = reader.ReadUInt16();
            this.Format = (VTexFormat)reader.ReadByte();
            this.NumMipLevels = reader.ReadByte();

            // TODO: ...
            reader.BaseStream.Position = this.Data;

            switch (Format)
            {
                case VTexFormat.IMAGE_FORMAT_PNG:
                    this.DataStream = new MemoryStream(reader.ReadBytes((int)reader.BaseStream.Length));
                    break;

                case VTexFormat.IMAGE_FORMAT_RGBA8888:
                    for (ushort i = 0; i < Depth && i < 0xFF; ++i)
                    {
                        for (var j = NumMipLevels; j > 0; j--)
                        {
                            if (j == 1) break;

                            for (var k = 0; k < Height / Math.Pow(2.0, j - 1); ++k)
                            {
                                reader.BaseStream.Position += (int)((4 * Width) / Math.Pow(2.0f, j - 1));
                            }
                        }

                        this.DataStream = DDSImage.ReadRGBA8888(reader, Width, Height);
                    }
                    break;

                case VTexFormat.IMAGE_FORMAT_RGBA16161616F:
                    this.DataStream = DDSImage.ReadRGBA16161616F(reader, Width, Height);
                    break;

                case VTexFormat.IMAGE_FORMAT_DXT1:
                    for (ushort i = 0; i < Depth && i < 0xFF; ++i)
                    {
                        for (var j = NumMipLevels; j > 0; j--)
                        {
                            if (j == 1) break;

                            for (var k = 0; k < Height / Math.Pow(2.0, j + 1); ++k)
                            {
                                for (var l = 0; l < Width / Math.Pow(2.0, j + 1); ++l)
                                {
                                    reader.BaseStream.Position += 8;
                                }
                            }
                        }

                        this.DataStream = DDSImage.UncompressDXT1(reader, Width, Height);
                    }

                    break;

                case VTexFormat.IMAGE_FORMAT_DXT5:
                    for (ushort i = 0; i < Depth && i < 0xFF; ++i)
                    {
                        for (var j = NumMipLevels; j > 0; j--)
                        {
                            if (j == 1) break;

                            for (var k = 0; k < Height / Math.Pow(2.0, j + 1); ++k)
                            {
                                for (var l = 0; l < Width / Math.Pow(2.0, j + 1); ++l)
                                {
                                    reader.BaseStream.Position += 16;
                                }
                            }
                        }

                        this.DataStream = DDSImage.UncompressDXT5(reader, Width, Height, yCoCg);
                    }
                    break;
            }

            reader.BaseStream.Position = this.StreamEndPosition;
        }
    }
}