using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Helpers;

//using System.Web.Helpers;


namespace copwebapplication.Services
{
    public class CourseService 
    {
        const string serviceUrl = "https://ussouthcentral.services.azureml.net/workspaces/edc40912a2404ab7824efef410be95b0/services/6878af12e0c64d56b29c683c6982cc09/execute?api-version=2.0";
        const string apiKey = "Icqx7Jo6jDwsUgF7jBuFgRRiYiB1XkErvyt70SOQ8MKgDX2QEFo9bBs/L+UFLng0Z1kggqBTZtibd/JDyk/C5g=="; 

        public CourseService()
        {
        }

        public IEnumerable<string> GetCourses(string courseName)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {
                    Inputs = new Dictionary<string, StringTable>() 
                    { 
                        { 
                            "input1", 
                            new StringTable() 
                            {
                                ColumnNames = new string[] {"CourseName"},
                                Values = new string[,] {  { courseName } }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                
                client.BaseAddress = new Uri(serviceUrl);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                List<string> courses = new List<string>();

                try
                {
                    HttpResponseMessage response = client.PostAsJsonAsync("", scoreRequest).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = response.Content.ReadAsStringAsync().Result;
                        dynamic courseResult = Json.Decode(jsonString);
                        foreach (var course in courseResult.Results.output2.value.Values[0])
                        {
                            courses.Add(Convert.ToString(course));
                        }
                    }
                }
                catch (Exception ex)
                {
                    courses.Add(ex.Message);
                    courses.Add(ex.StackTrace);
                }

                return courses;
            }
        }

        class StringTable
        {
            public string[] ColumnNames { get; set; }

            public string[,] Values { get; set; }
        }
    }

}
