namespace Play.Common.Service.Settings.Contracts
{
    public interface IMongoDbSettings
    {
        string ConnectionString { get; init; }
        string DatabaseName { get; init; }
        string CollectionName { get; set; }
    }
}
