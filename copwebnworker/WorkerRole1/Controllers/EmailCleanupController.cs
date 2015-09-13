using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WorkerRole1.Services;

namespace WorkerRole1.Controllers
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
            catch(Exception)
            {
                returnCode = HttpStatusCode.InternalServerError;
            }

            return new HttpResponseMessage(returnCode);
        }
    }
}
