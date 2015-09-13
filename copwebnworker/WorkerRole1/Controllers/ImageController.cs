using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CloudSample.API.Media
{
    public class ImageController : ApiController
    {
        public HttpResponseMessage Get(string handle)
        {
            MemoryStream mStream = new BlobService().GetImage(handle);
            mStream.Seek(0, SeekOrigin.Begin);
            
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK) 
            {
                Content = new StreamContent(mStream)
            };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return response;
        }
    }
}
