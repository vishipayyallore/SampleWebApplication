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
    public class EmailCleanupController : ApiController
    {
        public HttpResponseMessage Get(string thandle, string beforeDays)
        {
            HttpStatusCode returnCode = HttpStatusCode.OK;

            try 
            {
                new TableService().DeleteEmail(thandle, Convert.ToDouble(beforeDays));
            }
            catch(Exception ex)
            {
                returnCode = HttpStatusCode.InternalServerError;
            }

            return new HttpResponseMessage(returnCode);
        }
    }
}
