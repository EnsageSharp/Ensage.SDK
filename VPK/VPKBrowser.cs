// <copyright file="VPKBrowser.cs" company="Ensage">
//    Copyright (c) 2018 Ensage.
// </copyright>

namespace Ensage.SDK.VPK
{
    using System;
    using System.ComponentModel.Composition;
    using System.IO;
    using System.Linq;

    using Ensage.SDK.VPK.Content;

    using PlaySharp.Toolkit.Helper.Annotations;

    using SteamDatabase.ValvePak;

    [Export]
    public sealed class VpkBrowser : IDisposable
    {
        private readonly Package package;

        private bool disposed;

        public VpkBrowser()
            : this(Path.Combine(Game.GamePath, "game", "dota", "pak01_dir.vpk"))
        {
        }

        public VpkBrowser(string fileName)
        {
            if (Path.GetExtension(fileName) != ".vpk")
            {
                throw new ArgumentException("only vpk files are valid");
            }

            this.package = new Package();
            this.package.Read(fileName);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        [CanBeNull]
        public Stream FindImage(string fileName)
        {
            var entry = this.package.FindEntry(fileName);
            if (entry != null)
            {
                switch (entry.TypeName)
                {
                    case "png":
                        {
                            this.package.ReadEntry(entry, out var buffer);
                            return new MemoryStream(buffer);
                        }

                    case "vtex_c":
                        {
                            this.package.ReadEntry(entry, out var buffer);
                            using (var stream = new MemoryStream(buffer))
                            {
                                var resourceFile = new ResourceFile(stream);
                                var vtex = resourceFile.ResourceEntries.OfType<VTex>().FirstOrDefault();
                                if (vtex != null)
                                {
                                    return vtex.DataStream;
                                }
                            }
                        }

                        break;

                    default:
                        throw new ArgumentException($"{entry.TypeName} is not supported yet");
                }
            }

            return null;
        }

        private void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.package.Dispose();
            }

            this.disposed = true;
        }
    }
}