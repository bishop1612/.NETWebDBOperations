using CRUDWebAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CRUDWebAPI.Services
{
    public class MongoDBService
	{
        private readonly IMongoCollection<EmployeeLaborHours> _laborhours;

        public MongoDBService(IOptions<MongoConnect> mongoConn)
        {
            MongoClient client = new MongoClient(mongoConn.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoConn.Value.DatabaseName);
            _laborhours = database.GetCollection<EmployeeLaborHours>(mongoConn.Value.CollectionName);
        }

        public async Task<List<EmployeeLaborHours>> GetAsync() {
            return await _laborhours.Find(new BsonDocument()).ToListAsync();
        }

        public async Task CreateAsync(EmployeeLaborHours laborhour) {
            await _laborhours.InsertOneAsync(laborhour);
            return;
        }

        public async Task UpdateEntry(int id, double billhours, double nonbillhours, DateTime weekenddate) {
            // Add Data which we wants to update to the List
            var updateDefination = new List<UpdateDefinition<EmployeeLaborHours>>();
            FilterDefinition<EmployeeLaborHours> filter = Builders<EmployeeLaborHours>.Filter.Eq("Employee_id", id);
            UpdateDefinition<EmployeeLaborHours> update;
            update = Builders<EmployeeLaborHours>.Update.Set("Billable_Hours", billhours);
            updateDefination.Add(update);

            update = Builders<EmployeeLaborHours>.Update.Set("Non_Billable_Hours", nonbillhours);
            updateDefination.Add(update);

            update = Builders<EmployeeLaborHours>.Update.Set("Week_Ending_Date", weekenddate);
            updateDefination.Add(update);

            var combinedUpdate = Builders<EmployeeLaborHours>.Update.Combine(updateDefination);
            await _laborhours.UpdateOneAsync(filter, combinedUpdate);

        }

        public async Task DeleteAsync(int id) {
            FilterDefinition<EmployeeLaborHours> filter = Builders<EmployeeLaborHours>.Filter.Eq("Employee_id", id);
            await _laborhours.DeleteOneAsync(filter);
            return;
        }

    }
}

