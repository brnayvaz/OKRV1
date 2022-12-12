using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Okr.Web.Enum;
using Okr.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

namespace Okr.Web.Controllers
{
    public class HomeController : Controller 
    {
       
        public HomeController() { }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PublishPage()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Index(UserModel userModel)
        {
            object userToken;
            using (var client = new HttpClient())
            {
                

                var myContent = JsonConvert.SerializeObject(userModel);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.BaseAddress = new Uri("https://localhost:5050/api/login");
                var postTask = client.PostAsync("", byteContent);
                postTask.Wait();

                userToken = postTask.Result.Content.ReadAsStringAsync().Result;
                if (userToken != null)
                {
                    TempData["userToken"] = userToken;
                    return RedirectToAction("Index");
                }
            }
            



            return View("Index");
        }

        public ActionResult AdminPage()
        {
           
            return View();
        }

        public ActionResult WritePage()
        {

            return View();
        }

        public ActionResult ReadPage()
        {

            return View();
        }

        public ActionResult FreePage()
        {

            return View();
        }

        public ActionResult MockPage()
        {

            return View();
        }

        public ActionResult ConsumerPage()
        {

            return View();
        }

        public ActionResult ParallelPage()
        {

            return View();
        }

        [HttpGet]
        public ActionResult GetAdminPage(string token)
        {
            object adminPageResult;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:5050/api/adminPage");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var postTask = client.GetAsync("");
                postTask.Wait();

                var result = postTask.Result;

                adminPageResult = result.Content.ReadAsStringAsync().Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["adminPage"] = adminPageResult;
                }
                else
                {
                    TempData["adminPage"] = result.StatusCode;
                }
            }




            return View("AdminPage");
        }

        [HttpGet]
        public ActionResult GetMockPage()
        {
            object mockPageResult;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:5050/api/mockData/httpClientMiddleWare");
                var postTask = client.GetAsync("");
                postTask.Wait();

                var result = postTask.Result;

                mockPageResult = result.Content.ReadAsStringAsync().Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["mockPage"] = mockPageResult;
                }
                else
                {
                    TempData["mockPage"] = result.StatusCode;
                }
            }




            return View("MockPage");
        }

        [HttpGet]
        public ActionResult GetWritePage(string token)
        {
            object writePageResult;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:5050/api/writePage");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var postTask = client.GetAsync("");
                postTask.Wait();

                var result = postTask.Result;

                writePageResult = result.Content.ReadAsStringAsync().Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["writePage"] = writePageResult;
                }
                else
                {
                    TempData["writePage"] = result.StatusCode;
                }
            }




            return View("writePage");
        }

        [HttpGet]
        public ActionResult GetReadPage(string token)
        {
            object readPageResult;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:5050/api/readPage");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var postTask = client.GetAsync("");
                postTask.Wait();

                var result = postTask.Result;

                readPageResult = result.Content.ReadAsStringAsync().Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["readPage"] = readPageResult;
                }
                else
                {
                    TempData["readPage"] = result.StatusCode;
                }
            }




            return View("ReadPage");
        }

        [HttpGet]
        public ActionResult GetFreePage(string token)
        {
            object freePageResult;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:5050/api/freePage");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var postTask = client.GetAsync("");
                postTask.Wait();

                var result = postTask.Result;

                freePageResult = result.Content.ReadAsStringAsync().Result;
                if (result.IsSuccessStatusCode)
                {
                    TempData["freePage"] = freePageResult;
                }
                else
                {
                    TempData["freePage"] = result.StatusCode;
                }
            }




            return View("FreePage");
        }

        [HttpPost]
        public ActionResult PostPublishMessage(UserBusModel model)
        {
            object result;
            using (var client = new HttpClient())
            {


                var myContent = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.BaseAddress = new Uri("https://localhost:5050/api/publishMessage");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.Token);
                var postTask = client.PostAsync("", byteContent);
                postTask.Wait();

                result = postTask.Result.Content.ReadAsStringAsync().Result;
                if (result != null)
                {
                    TempData["publishMessage"] = result;
                    return RedirectToAction("PublishPage");
                }
            }




            return View("PublishPage");
        }

        [HttpGet]
        public ActionResult GetConsumer(ConsumerModel model)
        {
            object result;
            using (var client = new HttpClient())
            {


                var myContent = JsonConvert.SerializeObject(model.QueueName);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.BaseAddress = new Uri("https://localhost:5050/api/consumer");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.Token);
                var postTask = client.PostAsync("", byteContent);
                postTask.Wait();

                result = postTask.Result.Content.ReadAsStringAsync().Result;
                if (result != null)
                {
                    TempData["consumer"] = JsonConvert.DeserializeObject<List<UserBusModel>>(result.ToString()); 
                    return RedirectToAction("ConsumerPage");
                }
            }




            return View("ConsumerPage");
        }

        [HttpGet]
        public ActionResult GetParallelPage(string token)
        {
            Stopwatch stopWatch = new Stopwatch();
            TimeSpan ts;
            string elapsedTime = "";


            List<int> listem = new List<int> { 1, 2, 3 };

            object result;

            stopWatch.Start();

            Parallel.ForEach(listem, k => {

                using (var client = new HttpClient())
                {
                    var myContent = JsonConvert.SerializeObject(k);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    client.BaseAddress = new Uri("https://localhost:5050/api/parallelTask");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var postTask = client.PostAsync("", byteContent);
                    postTask.Wait();

                    result = postTask.Result.Content.ReadAsStringAsync().Result;
                }

            });

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            TempData["parallelPageTime"] = elapsedTime;


            stopWatch.Start();

            foreach (int i in listem)
                {
                using (var client = new HttpClient())
                {
                    var myContent = JsonConvert.SerializeObject(i);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    client.BaseAddress = new Uri("https://localhost:5050/api/parallelTask");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var postTask = client.PostAsync("", byteContent);
                    postTask.Wait();

                    result = postTask.Result.Content.ReadAsStringAsync().Result;
                }
            }
            

            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

            TempData["UnParalleledPageTime"] = elapsedTime;


            return View("ParallelPage");
        }

    }
}