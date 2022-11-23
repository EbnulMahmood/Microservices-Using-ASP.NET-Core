using Play.Common.Service.IEntities;

namespace Play.Catalog.Service.Entities
{
    public sealed class Item : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
