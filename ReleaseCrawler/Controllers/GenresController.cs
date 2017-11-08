﻿using ReleaseCrowler.Models;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ReleaseCrawler.Controllers
{
    public class GenresController : ApiController
    {
        DataContext db = new DataContext();
        
        public HttpResponseMessage Get()
        {
            var genres = db.Genres.Select(m => m.Name);
            Request.Properties["Count"] = genres.Count();

            return Request.CreateResponse(HttpStatusCode.OK, genres.ToList(), MediaTypeHeaderValue.Parse("application/json"));
        }
    }
}
