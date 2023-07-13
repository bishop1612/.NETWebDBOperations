using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CRUDWebAPI.Models
{
	public class EmployeeLaborHours
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Employee_id")]
        [JsonPropertyName("Employee_id")]
        public int EmployeeID { get; set; }

        [BsonElement("Billable_Hours")]
        [JsonPropertyName("Billable_Hours")]
        public double Bill_Hours { get; set; }

        [BsonElement("Non_Billable_Hours")]
        [JsonPropertyName("Non_Billable_Hours")]
        public double Non_Bill_Hours { get; set; }

        [BsonElement("Week_Ending_Date")]
        [JsonPropertyName("Week_Ending_Date")]
        public DateTime Week_Ending_Date { get; set; }
	}
}

