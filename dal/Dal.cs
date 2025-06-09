using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.people;
using MySql.Data.MySqlClient;

namespace Malshinon.dal
{
    internal class Dal
    {
        private string ConnStr = "server=localhost;username=root;password=;database=malshinon";
        private MySqlConnection Conn;

        public Dal()
        {
            this.Conn = new MySqlConnection(this.ConnStr);
        }

        public MySqlCommand Command(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, this.Conn);
            return cmd;
        }

        public void InsertNewPerson(People people)
        {
            string query = @"INSERT INTO people (first_name, last_name, secret_code, type)
                             VALUES (@first_name, @last_name, @secret_code, @type)";
            try
            {
                this.Conn.Open();
                var cmd = this.Command(query);
                cmd.Parameters.AddWithValue("@first_name", people.firstName);
                cmd.Parameters.AddWithValue("@last_name", people.lastName);
                cmd.Parameters.AddWithValue("@secret_code", people.secretCode);
                cmd.Parameters.AddWithValue("@type", people.type);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error adding people" + ex.Message);
            }
            finally
            {
                this.Conn.Close();  
            }
        }
    }
}
