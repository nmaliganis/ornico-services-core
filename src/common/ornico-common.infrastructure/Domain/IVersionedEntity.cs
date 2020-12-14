namespace ornico.common.infrastructure.Domain
{
    public interface IVersionedEntity
    {
        int Revision { get; set; }
    }
}