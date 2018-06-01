using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
//using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using FlipSideModels;

namespace FlipSideWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/TrendingTopics")]
    public class TrendingTopicsController : Controller
    {
        TrendingTopic[] topics = new TrendingTopic[]
        {
            new TrendingTopic { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
            new TrendingTopic { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
            new TrendingTopic { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
        };

        public IEnumerable<TrendingTopic> GetAllTopics()
        {
            return topics;
        }

        //public IHttpActionResult GetTopic(int id)
        //{
        //    var topic = topics.FirstOrDefault((p) => p.Id == id);
        //    if (topic == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(topic);
        //}
    }
}