using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq.Expressions;
using UserLoginApp.DataAccess.Conrete.Mongo.Model;
using UserLoginApp.DataAccess.Interfaces;
using UserLoginApp.Entities.Attributes;
using UserLoginApp.Entities.Interfaces;

namespace UserLoginApp.DataAccess.Conrete.Mongo
{
    public class GenericRepositoryMD<TEntity> : IGenericRepositoryMD<TEntity>
          where TEntity : IEntity
    {
        private readonly IMongoCollection<TEntity> _collection;
        public GenericRepositoryMD(IOptions<MongoSettingsModel> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DbName);

            _collection = mongoDatabase.GetCollection<TEntity>(GetCollectionName(typeof(TEntity)));

        }
        private protected string? GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(typeof(BsonCollectionAttribute),true)
                .FirstOrDefault()).CollectionName;
        }
        public void Add(TEntity entity)
        {
            _collection.InsertOne(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> expression)
        {
            return _collection.Find(expression).FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll()
        {
            
            return _collection.Find(x=>true).ToEnumerable();
        }
        public IEnumerable<TEntity> GetAllByFilter(Expression<Func<TEntity, bool>> expression)
        {
            return _collection.Find(expression).ToEnumerable();
        }

        public void Remove(Expression<Func<TEntity, bool>> expression)
        {
            _collection.FindOneAndDelete(expression);
        }

        public void Update(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, entity.Id);
            _collection.FindOneAndReplace(filter, entity);
        }

        

    }
}
