namespace Play.Catalog.Contracts
{
    public sealed class CatalogItemUpdated
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
