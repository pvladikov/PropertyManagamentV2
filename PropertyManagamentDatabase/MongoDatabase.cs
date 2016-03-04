using MongoDB.Bson;
using MongoDB.Driver;
using PropertyManagamentDatabase.Interface;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyManagametTypes;
using System.Linq.Expressions;

namespace PropertyManagamentDatabase
{
    public class MongoDatabase<T> : IDatabase<T> where T : EntityBase
    {
        public const string CONNECTION_STRING_NAME = "db";
        public const string DATABASE_NAME = "propertymanagament";
        public const string COLLECTION_NAME = "pm";

        private readonly IMongoClient _client;
        private readonly IMongoDatabase _db;

        public MongoDatabase()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
            _client = new MongoClient(connectionString);
            _db = _client.GetDatabase(DATABASE_NAME);
        }

        private IMongoCollection<T> collection
        {
            get
            {
                return _db.GetCollection<T>(COLLECTION_NAME);
            }
            set
            {
                collection = value;
            }
        }

        public IQueryable<T> GetAll
        {
            get
            {
                return collection.AsQueryable<T>();
            }
            set
            {
                GetAll = value;
            }
        }

        public async Task Delete(T item )
        {            
             var filter_builder = Builders<T>.Filter;
             var filter = filter_builder.Eq("_id", ObjectId.Parse(item.Id));

            await collection.DeleteOneAsync(filter);

            // var result = collection.DeleteOneAsync(filter);
            //return result.DeletedCount == 1;

        }

        public async Task Create(T item)
        {
            await collection.InsertOneAsync(item);
        }

        public void DeleteAll()
        {
            _db.DropCollection(typeof(T).Name);
        }

        public async Task Update(T item)
        {          

            var filter = Builders<T>.Filter.Eq(s => s.Id, item.Id);
            await collection.ReplaceOneAsync(filter, item);
            //var result = await collection.ReplaceOneAsync(filter, item);
            //return result.ModifiedCount == 1;

        }

        public T GetByID(string id)
        {            

            var filter = Builders<T>.Filter.Eq(s => s.Id, id);
            var result = collection.Find(filter).FirstOrDefault();
            return result;
        }
       
    }
}
