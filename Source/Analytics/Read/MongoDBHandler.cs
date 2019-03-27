using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Read
{
    public class MongoDBHandler
    {
        private readonly string connectionString = "mongodb://localhost";

        public MongoDBHandler()
        { }

        private void SetUpConnectionAsync()
        {
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("read_model_database");
            var collection = database.GetCollection<BsonDocument>("CaseReport");
            var list = collection.Find(new BsonDocument()).ToEnumerable();

            foreach (var element in list)
            {
                Console.WriteLine(element);
            }
        }

        public IQueryable<DbCaseEntry> GetQueryable()
        {
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("read_model_database");
            var collection = database.GetCollection<DbCaseEntry>("CaseReport");
            return collection.AsQueryable().AsQueryable();
        }

        public void DeleteAllRecordsFromDB()
        {
            var client = new MongoClient(connectionString);

            //Use the MongoClient to access the server
            IMongoDatabase database = client.GetDatabase("read_model_database");
            database.DropCollection("CaseReport");
        }

        public void InsertRecordToDB(DbCaseEntry dbEntry)
        {
            // Create a MongoClient object by using the connection string
            var client = new MongoClient(connectionString);

            //Use the MongoClient to access the server
            IMongoDatabase database = client.GetDatabase("read_model_database");

            //get mongodb collection
            var collection = database.GetCollection<DbCaseEntry>("CaseReport");
            collection.InsertOneAsync(dbEntry);
        }
    }
}
