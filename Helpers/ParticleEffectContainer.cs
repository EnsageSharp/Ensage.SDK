// <copyright file="ParticleEffectContainer.cs" company="Ensage">
//    Copyright (c) 2017 Ensage.
// </copyright>

namespace Ensage.SDK.Helpers
{
    using System;
    using System.Linq;

    using SharpDX;

    public class ParticleEffectContainer : IDisposable, IEquatable<ParticleEffectContainer>
    {
        private bool disposed;

        public ParticleEffectContainer(string name, string file, Entity unit, ParticleAttachment attachment, params object[] controlPoints)
        {
            this.Name = name;
            this.File = file;
            this.Unit = unit;
            this.Attachment = attachment;
            this.ControlPoints = controlPoints;

            this.Effect = new ParticleEffect(file, unit, attachment);
            this.SetControlPoints();
        }

        public ParticleAttachment Attachment { get; }

        public object[] ControlPoints { get; private set; }

        public ParticleEffect Effect { get; private set; }

        public string File { get; }

        public string Name { get; }

        public Entity Unit { get; }

        public static bool operator ==(ParticleEffectContainer left, ParticleEffectContainer right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ParticleEffectContainer left, ParticleEffectContainer right)
        {
            return !Equals(left, right);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Equals(ParticleEffectContainer other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((ParticleEffectContainer)obj);
        }

        public int GetControlPointsHashCode()
        {
            if (this.ControlPoints == null)
            {
                return 0;
            }

            return this.ControlPoints.Sum(o => o.GetHashCode());
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)this.Attachment;
                hashCode = (hashCode * 397) ^ this.GetControlPointsHashCode();
                hashCode = (hashCode * 397) ^ (this.Effect?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (this.File?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (this.Name?.GetHashCode() ?? 0);
                hashCode = (hashCode * 397) ^ (this.Unit?.GetHashCode() ?? 0);

                return hashCode;
            }
        }

        public void SetControlPoints(params object[] controlPoints)
        {
            if (controlPoints?.Length > 0)
            {
                this.ControlPoints = controlPoints;
            }

            for (var i = 0; i < this.ControlPoints.Length; i = i + 2)
            {
                var index = (uint)(int)this.ControlPoints[i];
                var controlPoint = this.ControlPoints[i + 1];

                if (controlPoint is float)
                {
                    this.Effect.SetControlPoint(index, new Vector3((float)controlPoint, 255, 0));
                }
                else if (controlPoint is Color)
                {
                    var controlPointAsColor = (Color)controlPoint;
                    this.Effect.SetControlPoint(index, new Vector3(controlPointAsColor.R, controlPointAsColor.G, controlPointAsColor.B));
                }
                else if (controlPoint is bool)
                {
                    this.Effect.SetControlPoint(index, (bool)controlPoint ? new Vector3(1, 0, 0) : Vector3.Zero);
                }
                else if (controlPoint is Vector3)
                {
                    this.Effect.SetControlPoint(index, (Vector3)controlPoint);
                }
            }

            this.Effect.Restart();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.Effect?.Dispose();
            }

            this.disposed = true;
        }
    }
}