using System;
using System.IO;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using FlipSideModels;
using Microsoft.Extensions.Configuration;
using FlipSideMVCConfig;

namespace FlipSideDataAccess
{
    public class BaseRepository
    {
        public T QueryFirstOrDefault<T>(string sql, object parameters = null)
        {
            using (var connection = new ConnectionFactory())
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

        //{
            class Program
            {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // <== compile failing here
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            Console.WriteLine(Configuration.GetConnectionString("con"));
            Console.WriteLine("Press a key...");
            Console.ReadKey();
        }
    }
}




    private IDbConnection CreateConnection()
        {
            // Properly initialize your connection here.
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
            IConfigurationRoot configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                    .SetBasePath(projectPath)
                    .AddJsonFile("appsettings.json")
                   .Build();
            var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FlipSide"].ConnectionString);
            //var connection = new SqlConnection("Data Source=Windows-68KGE2N;Initial Catalog=FlipSide;Integrated Security=True;");

            return connection;
        }
    }
}