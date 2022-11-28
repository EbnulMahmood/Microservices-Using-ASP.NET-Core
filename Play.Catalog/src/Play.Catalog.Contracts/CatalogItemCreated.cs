namespace Play.Catalog.Contracts
{
    public sealed class CatalogItemCreated
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
