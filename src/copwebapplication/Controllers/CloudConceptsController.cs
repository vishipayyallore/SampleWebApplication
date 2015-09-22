using System.Linq;
using copwebapplication.Services;
using Microsoft.AspNet.Mvc;

namespace copwebapplication.Controllers
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

        public ActionResult EmailHistory()
        {
            return View();
        }

        #endregion

        #region API methods

        public ActionResult AuthorSearch(string firstName, string lastName)
        {
            //CloudSampleEventSource.Log.SearchCalled(firstName, lastName);
            //new AuthorService().InsertSeedData();

            var result = new AuthorService().GetAuthors(firstName, lastName)
                .Select(f => new
                {
                    Firstname = f.Firstname,
                    Lastname = f.Lastname,
                    Twitter = f.RowKey,
                    Phone = f.Phone,
                    Email = f.Email,
                })
                .ToArray();

            return Json(result);
        }

        public ActionResult CourseSearch(string courseName)
        {
            var result = new CourseService().GetCourses(courseName);
            return Json(result);
        }

        public ActionResult EmailHistorySearch(string fromDate, string toDate)
        {
            var result = new EmailService().GetEmails(fromDate, toDate)
                .Select(f => new
                {
                    RequestDateTime = f.Timestamp,
                    Status = f.Status,
                    To = f.To,
                    Message = f.Message
                })
                .ToArray();

            return Json(result);
        }

        public ActionResult Mail(string to, string message, string thandle)
        {
            string status = "SENT";
            if (!Mailer.SendEmail(to, message)) status = "ERROR";
            new EmailService().Insert(to, message, thandle, status);
            return Json(status);
        }

        public ActionResult Image(string handle)
        {
            return File(new MediaService().GetImageStream(handle).ToArray(), "image/jpeg");
        }

        #endregion
    }
}