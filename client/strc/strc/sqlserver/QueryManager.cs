using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

using strc.config;
using strc.model;
using strc.security;
using System.Data;

namespace strc.sqlserver
{
    public class QueryManager
    {
        public static async Task<int> ExecuteInt32Async(string query)
        {
            using (SqlConnection connecton = new SqlConnection(Settings.Default.connection))
            using (SqlCommand command = new SqlCommand(query, connecton))
            {
                await connecton.OpenAsync();

                try
                {
                    object result =
                        await command.ExecuteScalarAsync();

                    return ( int )result;
                }
                catch (Exception e)
                {
                    await ErrorManager.ReportAsync(e);
                    return -1;
                }
            }
        }

        public static async Task<string[]> ExecuteVectorAsync(string query)
        {
            List<string> vec = new List<string>();

            using (SqlConnection connection = new SqlConnection(Settings.Default.connection))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                        while (await reader.ReadAsync())
                            vec.Add(reader.GetString(0));
                }
                catch (Exception e)
                {
                    await ErrorManager.ReportAsync(e);
                }
            }

            return vec.ToArray();
        }

        public static async Task<DataTable> ExecuteMapAsync(string query)
        {
            DataTable table = new DataTable();

            using (SqlConnection c = new SqlConnection(Settings.Default.connection))
            using (SqlCommand command = new SqlCommand(query, c))
            {
                try
                {
                    await c.OpenAsync();

                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        List<object> vals = new List<object>();
                        DataColumn column = new DataColumn();
                        
                        

                        table.Rows.Add();
                        
                    }
                }
                catch (Exception e)
                {
                    await ErrorManager.ReportAsync(e);
                }
            }

            return table;
        }

        public static async Task<bool> InsertUserAsync(User user)
        {
            int count = await ExecuteInt32Async("SELECT COUNT(*) FROM [users]") + 1;


            using (SqlConnection c = new SqlConnection(Settings.Default.connection))
            using (SqlCommand command = new SqlCommand("", c))
            {
                await c.OpenAsync();

                try 
                {
                    command.CommandText =
                    $"INSERT [users] (id, name, sname, phone, gid)" +
                    $"VALUES ({count}, '{user.Name}', '{user.Surname}', '{user.Phone}', {user.Gid})";

                    await command.ExecuteNonQueryAsync();

                    return true;
                }
                catch (Exception e) 
                {
                    await ErrorManager.ReportAsync(e);
                    return false;
                }

            }
        }

        public static async Task<bool> InsertGroupAsync(Group group)
        {
            string query = 
                 "INSERT [groups] (id, title, content)" + 
                $"VALUES ({group.Id}, '{group.Title}', '{group.Content}')";

            using (SqlConnection c = new SqlConnection(Settings.Default.connection))
            using (SqlCommand command = new SqlCommand(query, c))
            {
                await c.OpenAsync();

                try 
                {
                    await command.ExecuteNonQueryAsync();
                }
                catch(Exception e) 
                {
                    await ErrorManager.ReportAsync(e);
                    return false;
                }

                return true;
            }
        }
    }
}
