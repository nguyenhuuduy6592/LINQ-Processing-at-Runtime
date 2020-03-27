using AngularJS.Models;
using AngularJS.Utilities;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace AngularJS.Services
{
    public class MongoDbService<T> where T : BaseEntity
    {
        public readonly IMongoDatabase Database;
        public readonly IMongoCollection<T> Collection;
        private readonly string collectionName;

        public MongoDbService()
        {
            var client = new MongoClient(GlobalConstants.BookstoreDatabaseSettings.ConnectionString);
            Database = client.GetDatabase(GlobalConstants.BookstoreDatabaseSettings.DatabaseName);
        }

        public MongoDbService(string collectionName)
        {
            var client = new MongoClient(GlobalConstants.BookstoreDatabaseSettings.ConnectionString);
            Database = client.GetDatabase(GlobalConstants.BookstoreDatabaseSettings.DatabaseName);
            Collection = Database.GetCollection<T>(collectionName);
            this.collectionName = collectionName;
        }

        public void DropCollection() =>
            Database.DropCollection(collectionName);

        public void CreateCollection() =>
            Database.CreateCollection(collectionName);

        public IMongoCollection<T> GetCollection() =>
            Database.GetCollection<T>(collectionName);

        public IMongoCollection<T> GetCollectionWithReCreation()
        {
            DropCollection();
            CreateCollection();
            return Database.GetCollection<T>(collectionName);
        }

        public List<T> Get() =>
            Collection.Find(x => true).ToList();

        public T Get(string id) =>
            Collection.Find(x => x.Id == id).FirstOrDefault();

        public T Create(T x)
        {
            Collection.InsertOne(x);
            return x;
        }

        public bool IsCollectionExist(string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var options = new ListCollectionNamesOptions { Filter = filter };

            return !Database.ListCollectionNames(options).Any();
        }

        public void InsertMany(List<T> x) =>
            Collection.InsertMany(x);

        public void Update(string id, T entity) =>
            Collection.ReplaceOne(x => x.Id == id, entity);

        public void Remove(T entity) =>
            Collection.DeleteOne(x => x.Id == entity.Id);

        public void Remove(string id) =>
            Collection.DeleteOne(x => x.Id == id);
    }
}