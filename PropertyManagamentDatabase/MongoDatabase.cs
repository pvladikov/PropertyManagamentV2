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

        public IQueryable<T> Query
        {
            get
            {
                return collection.AsQueryable<T>();
            }
            set
            {
                Query = value;
            }
        }

        public bool Delete(T item )
        {            
             var filter_builder = Builders<T>.Filter;
             var filter = filter_builder.Eq("_id", ObjectId.Parse(item.Id));

            var result = collection.DeleteOne(filter);

            return result.DeletedCount == 1;

        }

        public void Create(T item)
        {
            collection.InsertOne(item);
        }

        public void DeleteAll()
        {
            _db.DropCollection(typeof(T).Name);
        }

        public bool Update(T item)
        {          

            var filter = Builders<T>.Filter.Eq(s => s.Id, item.Id);
            var result = collection.ReplaceOne(filter, item);
             return result.ModifiedCount == 1;

        }

        public T GetByID(string id)
        {            

            var filter = Builders<T>.Filter.Eq(s => s.Id, id);
            var result = collection.Find(filter).FirstOrDefault();
            return result;
        }
       
    }
}
