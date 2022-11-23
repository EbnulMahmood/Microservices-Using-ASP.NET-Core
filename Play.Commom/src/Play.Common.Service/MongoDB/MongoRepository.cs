using MongoDB.Driver;
using Play.Common.Service.IEntities;
using Play.Common.Service.IRepositories;
using Play.Common.Service.Settings.Contracts;
using System.Linq.Expressions;

namespace Play.Common.Service.MongoDB
{
    public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        private readonly IMongoCollection<TEntity> _dbCollection;
        private readonly FilterDefinitionBuilder<TEntity> _filterBuilder = Builders<TEntity>.Filter;

        public MongoRepository(IMongoDbSettings settings)
        {
            try
            {
                _dbCollection = new MongoClient(settings.ConnectionString)
                    .GetDatabase(settings.DatabaseName)
                    .GetCollection<TEntity>(settings.CollectionName);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task<IReadOnlyCollection<TEntity>> LoadAsync()
        {
            return await _dbCollection.Find(_filterBuilder.Empty).ToListAsync();
        }

        public virtual async Task<IReadOnlyCollection<TEntity>> LoadAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbCollection.Find(filter).ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            FilterDefinition<TEntity> filter = _filterBuilder.Eq(entity => entity.Id, id);

            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public virtual async Task CreateAsync(TEntity itemToCreate)
        {
            if (itemToCreate is null) throw new ArgumentNullException(nameof(itemToCreate));

            await _dbCollection.InsertOneAsync(itemToCreate);
        }

        public virtual async Task UpdateAsync(TEntity itemToUpdate)
        {
            if (itemToUpdate is null) throw new ArgumentNullException(nameof(itemToUpdate));

            FilterDefinition<TEntity> filter = _filterBuilder.Eq(existingEntity => existingEntity.Id, itemToUpdate.Id);

            await _dbCollection.ReplaceOneAsync(filter, itemToUpdate);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            FilterDefinition<TEntity> filter = _filterBuilder.Eq(entity => entity.Id, id);

            await _dbCollection.DeleteOneAsync(filter);
        }
    }
}
