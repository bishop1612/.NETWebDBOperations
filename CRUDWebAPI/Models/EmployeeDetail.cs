// Class for Basic Employee Detail Models Entity Framework
// Can populate and update table fields

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MySqlConnector;
using System.Data;

namespace CRUDWebAPI.Models
{ 
	public class EmployeeDetail
	{
        //Set rules while creating Table
        //Didnt add characteristics using api

        public int ID { get; set; }

        public string Name { get; set; }

        public double Cost_Rate { get; set; }

        public double Bill_Rate { get; set; }

        internal sqlConnect Db { get; set; }

        public EmployeeDetail()
        {
        }

        internal EmployeeDetail(sqlConnect db)
        {
            Db = db;
        }

        public async Task InsertAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO `EmployeeDetail` (`name`, `cost_rate`, `bill_rate`) VALUES (@name, @cost_rate, @bill_rate);";
            BindParams(cmd);
            await cmd.ExecuteNonQueryAsync();
            ID = (int)cmd.LastInsertedId;
        }

        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE `EmployeeDetail` SET `name` = @name, `cost_rate` = @cost_rate, `bill_rate` = @bill_rate WHERE `id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM `EmployeeDetail` WHERE `id` = @id;";
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindParams(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@name",
                DbType = DbType.String,
                Value = Name,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@cost_rate",
                DbType = DbType.Double,
                Value = Cost_Rate,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@bill_rate",
                DbType = DbType.Double,
                Value = Bill_Rate,
            });
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = ID,
            });
        }
    }
}

