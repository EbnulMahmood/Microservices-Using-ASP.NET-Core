using Play.Common.Service.Settings.Contracts;

namespace Play.Common.Service.Settings
{
    public sealed class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; init; } = null!;
        public string ConnectionString { get; init; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}
