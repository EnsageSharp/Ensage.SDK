// <copyright file="VTex.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.VPK.Content
{
    using System.IO;

    public class VTex : ResourceEntry
    {
        public VTex(BinaryReader reader)
            : base(reader)
        {
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

            // TODO: ...
            reader.BaseStream.Position = this.Data;
            this.DataStream = new MemoryStream(reader.ReadBytes((int)reader.BaseStream.Length));

            reader.BaseStream.Position = this.StreamEndPosition;
        }

        public Stream DataStream { get; }

        public ushort Depth { get; }

        public ushort Flags { get; }

        public VTexFormat Format { get; }

        public ushort Height { get; }

        public float[] Reflectivity { get; }

        public ushort Version { get; }

        public ushort Width { get; }
    }
}