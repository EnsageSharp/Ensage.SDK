// <copyright file="VpkBrowser.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.VPK
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;

    using Ensage.SDK.Renderer.VPK.Resources;
    using Ensage.SDK.Renderer.VPK.Utils;

    internal sealed class VpkBrowser
    {
        public const char DirectorySeparatorChar = '/';

        // ReSharper disable once InconsistentNaming
        private const int MAGIC = 0x55AA1234;

        private readonly Dictionary<string, PackageEntry> entries = new Dictionary<string, PackageEntry>();

        private readonly string vpkDirPath;

        private readonly string vpkFullPath;

        private BinaryReader reader;

        public VpkBrowser()
        {
            this.vpkFullPath = Path.Combine(Game.GamePath, "game", "dota", "pak01_dir.vpk");
            this.vpkDirPath = this.vpkFullPath.Substring(0, this.vpkFullPath.Length - 8);
        }

        public Bitmap GetBitmap(string fileName)
        {
            if (!this.entries.TryGetValue(fileName.Replace('\\', DirectorySeparatorChar), out var entry))
            {
                return null;
            }

            var buffer = new byte[entry.TotalLength];
            if (entry.SmallData.Length > 0)
            {
                entry.SmallData.CopyTo(buffer, 0);
            }

            using (var fs = new FileStream($"{this.vpkDirPath}_{entry.ArchiveIndex:D3}.vpk", FileMode.Open, FileAccess.Read))
            {
                fs.Seek(entry.Offset, SeekOrigin.Begin);
                fs.Read(buffer, entry.SmallData.Length, (int)entry.Length);
            }

            using (var stream = new MemoryStream(buffer))
            {
                return new ResourceFile(stream, entry.TypeName).GetBitmap();
            }
        }

        public void ReadFiles()
        {
            try
            {
                this.reader = new BinaryReader(new FileStream(this.vpkFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));

                if (this.reader.ReadUInt32() != MAGIC)
                {
                    throw new InvalidDataException("Given file is not a VPK.");
                }

                var version = this.reader.ReadUInt32();
                var treeSize = this.reader.ReadUInt32();
                var fileDataSectionSize = this.reader.ReadUInt32();
                var archiveMd5SectionSize = this.reader.ReadUInt32();
                var otherMd5SectionSize = this.reader.ReadUInt32();
                var signatureSectionSize = this.reader.ReadUInt32();

                this.ReadEntries();
            }
            finally
            {
                this.reader.Dispose();
            }
        }

        private void ReadEntries()
        {
            var extensions = new HashSet<string>
            {
                "vtex_c",
                "png"
            };

            var folders = new HashSet<string>
            {
                "panorama/images"
            };

            while (true)
            {
                var typeName = this.reader.ReadNullTermString(Encoding.UTF8);
                if (typeName.Length == 0)
                {
                    break;
                }

                if (typeName == " ")
                {
                    typeName = string.Empty;
                }

                while (true)
                {
                    var directoryName = this.reader.ReadNullTermString(Encoding.UTF8);
                    if (directoryName.Length == 0)
                    {
                        break;
                    }

                    if (directoryName == " ")
                    {
                        directoryName = null;
                    }

                    while (true)
                    {
                        var fileName = this.reader.ReadNullTermString(Encoding.UTF8);
                        if (fileName.Length == 0)
                        {
                            break;
                        }

                        var entry = new PackageEntry
                        {
                            FileName = fileName,
                            DirectoryName = directoryName,
                            TypeName = typeName,
                            Crc32 = this.reader.ReadUInt32(),
                            SmallData = new byte[this.reader.ReadUInt16()],
                            ArchiveIndex = this.reader.ReadUInt16(),
                            Offset = this.reader.ReadUInt32(),
                            Length = this.reader.ReadUInt32()
                        };

                        if (this.reader.ReadUInt16() != 0xFFFF)
                        {
                            throw new FormatException("Invalid terminator.");
                        }

                        if (entry.SmallData.Length > 0)
                        {
                            this.reader.Read(entry.SmallData, 0, entry.SmallData.Length);
                        }

                        if (!extensions.Contains(typeName) || folders.All(x => directoryName?.StartsWith(x) != true))
                        {
                            continue;
                        }

                        this.entries[entry.GetFullPath()] = entry;
                    }
                }
            }
        }
    }
}