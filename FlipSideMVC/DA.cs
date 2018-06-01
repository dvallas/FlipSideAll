
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using Dapper;
using FlipSideModels;
using FlipSideMVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FlipSideDataAccess
{

    public class DA
    {
        private readonly FlipSideDataContext _context;

        public DA()
        {
            ;}
        public DA(FlipSideDataContext context)
        {
            _context = context;
        }



        public List<Story> ReadStories(DateTime date)
        {
            return new Repository().Query<Story>("SELECT * FROM story order by DateRan");
        }

        public Story ReadStory(int id)
        {
            return new Repository().QueryFirstOrDefault<Story>(
                "SELECT * FROM story where id=" + id.ToString() + " order by DateRan");
        }

        public int WriteStory(Story story)
        {
            var query = "INSERT INTO [dbo].[story]([dateRan], [slug], [summary], [byline], [lean], [link], [topic]) " +
                        $" VALUES('{story.dateRan}','{story.slug}','{story.summary}','{story.byline}','{story.lean}','{story.link}', '{story.topic}' )";
            new Repository().Query<string>(query);
            return 1;
        }
    }

    class Repository
    {
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(FlipSide.Controllers.HomeController
                ._iconfiguration.GetConnectionString("FlipSide"));
        }

      public T QueryFirstOrDefault<T>(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                var cs = ConfigurationManager.ConnectionStrings[0].ConnectionString;
                return connection.QueryFirstOrDefault<T>(sql, parameters);
            }
        }


        public List<T> Query<T>(string sql, object parameters = null)
        {
            //var test = FlipSide.Controllers.HomeController._iconfiguration;
            using (var connection = CreateConnection())
                //new SqlConnection(test.GetConnectionString("FlipSide")))
                //new SqlConnection(ConfigurationManager.ConnectionStrings["FlipSide"].ConnectionString))
            {
                if (sql != null) return connection.Query<T>(sql, parameters).ToList();
                return new List<T>();
            }
        }

        protected int Execute(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                return connection.Execute(sql, parameters);
            }
        }


    }

}

