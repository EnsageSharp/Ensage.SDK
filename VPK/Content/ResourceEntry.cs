// <copyright file="ResourceEntry.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.VPK.Content
{
    using System.IO;

    public class ResourceEntry
    {
        public ResourceEntry(BinaryReader reader)
        {
            this.StreamStartPosition = reader.BaseStream.Position;
            this.Offset = reader.ReadUInt32();
            this.Size = reader.ReadUInt32();
            this.StreamEndPosition = reader.BaseStream.Position;
        }

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