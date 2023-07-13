using System;
namespace CRUDWebAPI.Models
{
	public class MongoConnect
	{
		public MongoConnect()
		{

		}
        public string ConnectionURI { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}

