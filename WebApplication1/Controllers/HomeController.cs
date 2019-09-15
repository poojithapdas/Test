using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {

        public async Task<ActionResult> Index()
        {

            List<CoursesViewModel> lmd = new List<CoursesViewModel>();
            string url = "https://www2.bellevuecollege.edu/data/api/v1/courses/engl";
            using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                System.Net.Http.HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var ds = JsonConvert.DeserializeObject<DataSet>(data);

                    foreach (DataRow dr in ds.Tables[0].Rows) // loop for adding add from dataset to list<modeldata>  
                    {
                        lmd.Add(new CoursesViewModel
                        {
                            Title = dr["Title"].ToString(), // adding data from dataset row in to list<modeldata>  
                            Credits = Convert.ToInt32(dr["Credits"]),
                            Description = dr["Description"].ToString(),
                        });

                        

                    }
                }
                return View(lmd);
            }




        }
    }
}