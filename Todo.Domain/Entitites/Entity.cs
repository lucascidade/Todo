namespace Todo.Domain.Entitites
{
    public abstract class Entity : IEquatable<Entity>
    {
        public Entity() => Id = Guid.NewGuid();
        public Guid Id { get; private set; }

        public bool Equals(Entity? other) => Id == other?.Id;
    }
}