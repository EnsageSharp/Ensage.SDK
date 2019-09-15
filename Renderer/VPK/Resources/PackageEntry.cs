// <copyright file="PackageEntry.cs" company="Ensage">
//    Copyright (c) 2019 Ensage.
// </copyright>

namespace Ensage.SDK.Renderer.VPK.Resources
{
    internal class PackageEntry
    {
        public ushort ArchiveIndex { get; set; }

        public uint Crc32 { get; set; }

        public string DirectoryName { get; set; }

        public string FileName { get; set; }

        public uint Length { get; set; }

        public uint Offset { get; set; }

        public byte[] SmallData { get; set; }

        public uint TotalLength
        {
            get
            {
                return this.Length + (uint)this.SmallData.Length;
            }
        }

        public string TypeName { get; set; }

        public string GetFileName()
        {
            var fileName = this.FileName;

            if (this.TypeName != string.Empty)
            {
                fileName += "." + this.TypeName;
            }

            return fileName;
        }

        public string GetFullPath()
        {
            if (this.DirectoryName == null)
            {
                return this.GetFileName();
            }

            return this.DirectoryName + VpkBrowser.DirectorySeparatorChar + this.GetFileName();
        }
    }
}