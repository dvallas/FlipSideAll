using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FlipSideModels;

namespace FlipSideDataAccess
{
    public class BaseRepository
    {
        public T QueryFirstOrDefault<T>(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                return connection.QueryFirstOrDefault<T>(sql, parameters);
            }
        }

        public List<T> Query<T>(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                if (sql != null) return connection.Query<T>(sql, parameters).ToList();
                else
                {
                    return new List<T>();
                }
            }
        }

        protected int Execute(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                return connection.Execute(sql, parameters);
            }
        }

        // Other Helpers...

        private IDbConnection CreateConnection()
        {
            //var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlipSide"].ConnectionString);
            var connection = new SqlConnection("Data Source=Windows-68KGE2N;Initial Catalog=FlipSide;Integrated Security=True;");

            // Properly initialize your connection here.
            return connection;
        }
    }
}