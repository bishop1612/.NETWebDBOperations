//Class to filer and get information through EmployeeDetail table 
using System;
using MySqlConnector;
using System.Data;
using System.Data.Common;

namespace CRUDWebAPI.Models
{
	public class EmployeeDetailFilter
	{
		public sqlConnect Db { get; }

		public EmployeeDetailFilter(sqlConnect db)
		{
			Db = db;
		}

        public async Task<EmployeeDetail> FindOneAsync(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `EmployeeDetail` WHERE `id` = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id,
            });
            var result = await ReadAllAsync(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<EmployeeDetail>> GetAllAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `EmployeeDetail` ORDER BY `id` ;";
            return await ReadAllAsync(await cmd.ExecuteReaderAsync());
        }

        private async Task<List<EmployeeDetail>> ReadAllAsync(DbDataReader reader)
        {
            var employees = new List<EmployeeDetail>();
            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    var employee = new EmployeeDetail(Db)
                    {
                        ID = reader.GetInt16(0),
                        Name = reader.GetString(1),
                        Cost_Rate = reader.GetDouble(2),
                        Bill_Rate = reader.GetDouble(3),
                    };
                    employees.Add(employee);
                }
            }
            return employees;
        }
    }
}

