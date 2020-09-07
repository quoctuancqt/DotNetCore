using System.Collections.Generic;

namespace DotNetCore.Domain
{
    public abstract class Entity<TId> : Base<Entity<TId>>
    {
        protected Entity() { }

        protected Entity(TId id)
        {
            Id = id;
        }

        public TId Id { get; }

        protected sealed override IEnumerable<object> Equals()
        {
            yield return Id;
        }
    }
}
