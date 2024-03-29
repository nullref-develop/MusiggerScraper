﻿using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.AccessControl;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ReleaseCrowler.Controllers
{
    public class ProxyController : ApiController
    {
        [System.Web.Http.Route("api/proxy/{*id}")]
        public HttpResponseMessage Get(string id)
        {
            using (var client = new WebClient())
            {
                var fullUrl = "http://freake.ru/" + id;

                var path = HttpContext.Current.Server.MapPath("~/images/") + id.Replace('/', '\\');

                if (!File.Exists(path))
                {
                    var directoryName = Path.GetDirectoryName(path);

                    if (!Directory.Exists(directoryName))
                        Directory.CreateDirectory(directoryName);

                    client.DownloadFile(fullUrl, path);
                }

                HttpResponseMessage response = new HttpResponseMessage();
                response.Content = new StreamContent(new FileStream(path, FileMode.Open)); // this file stream will be closed by lower layers of web api for you once the response is completed.
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

                response.Headers.ETag = new EntityTagHeaderValue(@"""" + id + @"""");

                var cacheControl = new CacheControlHeaderValue();
                cacheControl.Public = true;
                cacheControl.MaxAge = new System.TimeSpan(365,0,0,0,0);

                response.Headers.CacheControl = cacheControl;


                return response;
            }

        }
    }
}
