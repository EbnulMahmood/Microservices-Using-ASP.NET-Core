using Play.Common.Service.IEntities;

namespace Play.Inventory.Service.Entities
{
    public sealed class CatalogItem : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
