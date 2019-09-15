// <copyright file="VTex.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.VPK.Resources
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Text;

    using Ensage.SDK.Renderer.VPK.Enums;
    using Ensage.SDK.Renderer.VPK.Utils;

    internal class VTex : IBitmap
    {
        public VTex(BinaryReader reader)
        {
            reader.BaseStream.Position += 8;

            var startOffset = reader.BaseStream.Position;
            var dataOffset = reader.ReadUInt32();
            var count = reader.ReadUInt32();

            reader.BaseStream.Position = startOffset + dataOffset;

            for (var i = 0; i < count; i++)
            {
                if (Encoding.UTF8.GetString(reader.ReadBytes(4)) == "DATA")
                {
                    break;
                }

                reader.BaseStream.Position += 8;
            }

            this.StreamStartPosition = reader.BaseStream.Position;
            this.Offset = reader.ReadUInt32();
            this.Size = reader.ReadUInt32();
            this.StreamEndPosition = reader.BaseStream.Position;

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

                if (text == "Texture Compiler Version Image YCoCg Conversion")
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
            this.Picmip0Res = reader.ReadUInt32();

            var extraDataOffset = reader.ReadUInt32();
            var extraDataCount = reader.ReadUInt32();

            if (extraDataCount > 0)
            {
                reader.BaseStream.Position += extraDataOffset - 8;

                for (var i = 0; i < extraDataCount; i++)
                {
                    var type = (VTexExtraData)reader.ReadUInt32();
                    var offset = reader.ReadUInt32() - 8;
                    var size = reader.ReadUInt32();

                    var prevOffset = reader.BaseStream.Position;

                    reader.BaseStream.Position += offset;

                    if (type == VTexExtraData.FILL_TO_POWER_OF_TWO)
                    {
                        reader.BaseStream.Position += 2;
                        var nw = reader.ReadUInt16();
                        var nh = reader.ReadUInt16();
                        if (nw > 0 && nh > 0 && this.Width >= nw && this.Height >= nh)
                        {
                            this.NonPow2Width = nw;
                            this.Height = nh;
                        }
                    }

                    reader.BaseStream.Position = prevOffset;
                }
            }

            reader.BaseStream.Position = this.Data;
            var width2 = this.NonPow2Width > 0 ? this.NonPow2Width : this.Width;

            switch (this.Format)
            {
                case VTexFormat.IMAGE_FORMAT_PNG:
                    using (var stream = new MemoryStream(reader.ReadBytes((int)reader.BaseStream.Length)))
                    {
                        this.Bitmap = new Bitmap(stream);
                    }

                    break;

                case VTexFormat.IMAGE_FORMAT_BGRA8888:
                    for (ushort i = 0; i < this.Depth && i < 0xFF; ++i)
                    {
                        for (var j = this.NumMipLevels; j > 0; j--)
                        {
                            if (j == 1)
                            {
                                break;
                            }

                            for (var k = 0; k < (this.Height / Math.Pow(2.0, j - 1)); ++k)
                            {
                                reader.BaseStream.Position += (int)((4 * this.Width) / Math.Pow(2.0f, j - 1));
                            }
                        }

                        this.Bitmap = TextureDecompressors.ReadBGRA8888(reader, this.Width, this.Height);
                    }

                    break;
                case VTexFormat.IMAGE_FORMAT_RGBA8888:
                    for (ushort i = 0; i < this.Depth && i < 0xFF; ++i)
                    {
                        for (var j = this.NumMipLevels; j > 0; j--)
                        {
                            if (j == 1)
                            {
                                break;
                            }

                            for (var k = 0; k < (this.Height / Math.Pow(2.0, j - 1)); ++k)
                            {
                                reader.BaseStream.Position += (int)((4 * this.Width) / Math.Pow(2.0f, j - 1));
                            }
                        }

                        this.Bitmap = TextureDecompressors.ReadRGBA8888(reader, this.Width, this.Height);
                    }

                    break;

                case VTexFormat.IMAGE_FORMAT_RGBA16161616F:
                    this.Bitmap = TextureDecompressors.ReadRGBA16161616F(reader, this.Width, this.Height);
                    break;

                case VTexFormat.IMAGE_FORMAT_DXT1:
                    for (ushort i = 0; i < this.Depth && i < 0xFF; ++i)
                    {
                        for (var j = this.NumMipLevels; j > 0; j--)
                        {
                            if (j == 1)
                            {
                                break;
                            }

                            for (var k = 0; k < (this.Height / Math.Pow(2.0, j + 1)); ++k)
                            {
                                for (var l = 0; l < (this.Width / Math.Pow(2.0, j + 1)); ++l)
                                {
                                    reader.BaseStream.Position += 8;
                                }
                            }
                        }

                        this.Bitmap = TextureDecompressors.UncompressDXT1(reader, this.Width, this.Height);
                    }

                    break;

                case VTexFormat.IMAGE_FORMAT_DXT5:
                    for (ushort i = 0; i < this.Depth && i < 0xFF; ++i)
                    {
                        for (var j = this.NumMipLevels; j > 0; j--)
                        {
                            if (j == 1)
                            {
                                break;
                            }

                            for (var k = 0; k < (this.Height / Math.Pow(2.0, j + 1)); ++k)
                            {
                                for (var l = 0; l < (this.Width / Math.Pow(2.0, j + 1)); ++l)
                                {
                                    reader.BaseStream.Position += 16;
                                }
                            }
                        }

                        this.Bitmap = TextureDecompressors.UncompressDXT5(reader, this.Width, this.Height, width2, yCoCg);
                    }

                    break;
                default:
                    throw new ArgumentException("Texture format is not supported", this.Format.ToString());
            }

            reader.BaseStream.Position = this.StreamEndPosition;
        }

        public Bitmap Bitmap { get; }

        public ushort Depth { get; }

        public ushort Flags { get; }

        public VTexFormat Format { get; }

        public ushort Height { get; }

        public ushort NonPow2Width { get; }

        public byte NumMipLevels { get; }

        public uint Picmip0Res { get; }

        public float[] Reflectivity { get; }

        public ushort Version { get; }

        public ushort Width { get; }

        protected uint Data
        {
            get
            {
                return (uint)this.StreamStartPosition + this.Offset + this.Size;
            }
        }

        protected uint Offset { get; }

        protected uint Size { get; }

        protected long StreamEndPosition { get; }

        protected long StreamStartPosition { get; }
    }
}