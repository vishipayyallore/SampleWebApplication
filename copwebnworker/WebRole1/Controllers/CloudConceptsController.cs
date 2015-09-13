using System;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using WebRole1.Services;

namespace WebRole1.Controllers
{
    public class CloudConceptsController : Controller
    {
        #region Action methods

        public ActionResult Author()
        {
            return View();
        }

        public ActionResult Course()
        {
            return View();
        }

        public ActionResult BackOff()
        {
            return View();
        }

        #endregion

        #region API methods

        public ActionResult AuthorSearch(string firstName, string lastName)
        {
            CloudSampleEventSource.Log.SearchCalled(firstName, lastName);

            //new AuthorService().InsertSeedData();

            var result = new AuthorService().GetAuthors(firstName, lastName)
                .Select(f => new
                {
                    Firstname = f.Firstname,
                    Lastname = f.Lastname,
                    Twitter = f.PartitionKey,
                    Phone = f.Phone,
                    Email = f.Email,
                })
                .ToArray();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CourseSearch(string courseName)
        {
            var result = new CourseService().GetCourses(courseName);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Mail(string to, string message, string thandle)
        {
            string status = "SENT";
            if (!Mailer.SendEmail(to, message)) status = "ERROR";
            new Services.EmailService().Insert(to, message, thandle, status);
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SiteContent(string url) 
        {
            HttpClient httpClient = new HttpClient();
            string htmlString = string.Empty;

            Run.TightLoop(5, () =>
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (!response.IsSuccessStatusCode) throw new ApplicationException("Could not connect");
                htmlString = response.Content.ReadAsStringAsync().Result;
            });

            Run.WithDefault(10, 2, () =>
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (!response.IsSuccessStatusCode) throw new ApplicationException("Could not connect");
                htmlString = response.Content.ReadAsStringAsync().Result;
            });

            Run.WithRandomInterval(10, 1, 5, () =>
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (!response.IsSuccessStatusCode) throw new ApplicationException("Could not connect");
                htmlString = response.Content.ReadAsStringAsync().Result;
            });

            Run.WithProgressBackOff(10, 1, 10, () =>
            {
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                if (!response.IsSuccessStatusCode) throw new ApplicationException("Could not connect");
                htmlString = response.Content.ReadAsStringAsync().Result;
            });

            return Json(htmlString, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}