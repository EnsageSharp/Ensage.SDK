namespace Ensage.SDK.Helpers
{
    using SharpDX;

    public class EntityOrPosition
    {
        private Vector3 position;

        public EntityOrPosition(Entity entity)
        {
            this.Entity = entity;
            this.position = this.Entity.NetworkPosition;
        }

        public EntityOrPosition(Vector3 position)
        {
            this.position = position;
        }

        public Entity Entity { get; private set; }

        public Vector3 Position
        {
            get
            {
                if (this.Entity == null)
                {
                    return this.position;
                }

                if (this.Entity.IsValid)
                {
                    this.position = this.Entity.NetworkPosition;
                }
                else
                {
                    this.Entity = null;
                }

                return this.position;
            }
        }
    }
}