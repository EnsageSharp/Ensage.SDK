// <copyright file="ResourceFile.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.VPK.Content
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    public class ResourceFile
    {
        private readonly List<ResourceEntry> resourceEntries = new List<ResourceEntry>();

        public ResourceFile(Stream stream)
            : this(new BinaryReader(stream))
        {
        }

        public ResourceFile(BinaryReader reader)
        {
            this.FileSize = reader.ReadUInt32();
            this.HeaderVersion = reader.ReadUInt16();
            this.Version = reader.ReadUInt16();

            var startOffset = reader.BaseStream.Position;
            var offset = reader.ReadUInt32();
            var count = reader.ReadUInt32();

            reader.BaseStream.Position = startOffset + offset;
            for (var i = 0; i < count; i++)
            {
                var entryType = Encoding.UTF8.GetString(reader.ReadBytes(4));
                switch (entryType)
                {
                    // case "REDI": // TODO
                    // break;
                    case "DATA":
                        // TODO: verify that it's actually a vtex
                        this.resourceEntries.Add(new VTex(reader));
                        break;

                    default: // TODO
                        this.resourceEntries.Add(new ResourceEntry(reader));
                        break;
                }
            }
        }

        public uint FileSize { get; }

        public ushort HeaderVersion { get; }

        public IReadOnlyCollection<ResourceEntry> ResourceEntries
        {
            get
            {
                return this.resourceEntries.AsReadOnly();
            }
        }

        public ushort Version { get; }
    }
}