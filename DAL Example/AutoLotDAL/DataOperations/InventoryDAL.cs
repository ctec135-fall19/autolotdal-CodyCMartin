using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace AutoLotDAL.DataOperations
{
    public class InventoryDAL
    {
        private readonly string _connectionString; public InventoryDAL() : this
            (@"Data Source = (local-db)\mssqllocaldb.AutoLot;Integrated Security=true;Initial Catalog=AutoLot")
        {
        }
        public InventoryDAL(string connectionString) => _connectionString = connectionString;

        private SqlConnection _sqlConnection = null;


        // Opens Connection
        private void OpenConnection()

        {
            _sqlConnection = new SqlConnection { ConnectionString = _connectionString }; _sqlConnection.Open();
        }


        // Checks if connection is open, if it is, closes it.
        private void CloseConnection()

        {
            if (_sqlConnection?.State != ConnectionState.Closed) { _sqlConnection?.Close(); }
        }

        public void InsertAuto(string color, string make, string petName)
        {
            OpenConnection();
            // Format and execute SQL statement.   
            string sql = $"Insert Into Inventory (Make, Color, PetName) Values ('{make}', '{color}', '{petName}')";
            // Execute using our connection.  
            using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
            {
                command.CommandType = CommandType.Text; command.ExecuteNonQuery();
            }
            CloseConnection();
        }

        public void InsertAuto(Car car)
        {
            OpenConnection();

            // Format and execute SQL statement.  
            string sql = "Insert Into Inventory (Make, Color, PetName) Values " + $"('{car.Make}', '{car.Color}', '{car.PetName}')";
            // Execute using our connection.  
            using (SqlCommand command = new SqlCommand(sql, _sqlConnection))
            {
                command.CommandType = CommandType.Text; command.ExecuteNonQuery();
            }
            CloseConnection();
        }

        public void DeleteCar(int id)
        {
            OpenConnection();  
            // Get ID of car to delete, then do so.  
            string sql = $"Delete from Inventory where CarId = '{id}'";  
            using (SqlCommand command = new SqlCommand(sql, _sqlConnection))  
            {    try    
                {      command.CommandType = CommandType.Text;      
                       command.ExecuteNonQuery();    
                }    catch (SqlException ex)   
                {      Exception error = new Exception("Sorry! That car is on order!", ex);     
                       throw error;    
                }  
            }  
            CloseConnection(); 
        }

        public void UpdateCarPetName(int id, string newPetName)
        {
            OpenConnection();  
            // Get ID of car to modify the pet name.  
            string sql = $"Update Inventory Set PetName = '{newPetName}' Where CarId = '{id}'";  
            using (SqlCommand command = new SqlCommand(sql, _sqlConnection)) 
            {    command.ExecuteNonQuery();  
            }  
            CloseConnection(); 
        }

        // Input param. 
        SqlParameter param = new SqlParameter 
        {  
            ParameterName = "@carID",  
            SqlDbType = SqlDbType.Int,  Value = car.CarID,  
            Direction = ParameterDirection.Input 
        }; 
        command.Parameters.Add(param);

        // Return output param. 
        carPetName = (string)command.Parameters["@petName"].Value;




    }
}
