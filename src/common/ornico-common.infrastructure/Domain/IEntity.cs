using magic.button.common.infrastructure.Domain;

namespace ornico.common.infrastructure.Domain
{
    public interface IEntity<TId> : IVersionedEntity
    {
        TId Id { get; set; }
    }
}
