using AngularJS.Models;
using AngularJS.Utilities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace AngularJS.Services
{
    public class MongoDbService<T> where T : BaseEntity
    {
        public readonly IMongoDatabase Database;
        public readonly IMongoCollection<T> Collection;

        public MongoDbService(string collectionName)
        {
            var client = new MongoClient(GlobalConstants.BookstoreDatabaseSettings.ConnectionString);
            Database = client.GetDatabase(GlobalConstants.BookstoreDatabaseSettings.DatabaseName);
            Collection = Database.GetCollection<T>(collectionName);
        }

        public void CreateCollection(string name) =>
            Database.CreateCollection(name);

        public IMongoCollection<T> GetCollection(string name) =>
            Database.GetCollection<T>(name);

        public List<T> Get() =>
            Collection.Find(book => true).ToList();

        public T Get(string id) =>
            Collection.Find(book => book.Id == id).FirstOrDefault();

        public T Create(T book)
        {
            Collection.InsertOne(book);
            return book;
        }

        public void Update(string id, T bookIn) =>
            Collection.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(T bookIn) =>
            Collection.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            Collection.DeleteOne(book => book.Id == id);

    }
}