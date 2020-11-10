using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MvcClient47.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
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


        [Authorize]
        public async Task<ActionResult> Claims()
        {
            ViewBag.Message = "Claims";

            var result = await Request.GetOwinContext().Authentication.AuthenticateAsync("Cookies");
            string token = result.Properties.Dictionary["access_token"];

            //var user = User as ClaimsPrincipal;
            //var token = user.FindFirst("access_token");

            if (token != null)
            {
                ViewData["access_token"] = token;
            }

            return View();
        }

        [Authorize]
        public async Task<ActionResult> CallApi()
        {
            var result = await Request.GetOwinContext().Authentication.AuthenticateAsync("Cookies");
            string token = result.Properties.Dictionary["access_token"];

            var client = new HttpClient();
            //client.SetBearerToken(token);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var result2 = await client.GetStringAsync("https://localhost:6001/identity");
            ViewBag.Json = JArray.Parse(result2.ToString());

            return View();
        }


        public ActionResult Signout()
        {
            Request.GetOwinContext().Authentication.SignOut();
            return Redirect("/");
        }

        public void SignoutCleanup(string sid)
        {
            var cp = (ClaimsPrincipal) User;
            var sidClaim = cp.FindFirst("sid");
            if (sidClaim != null && sidClaim.Value == sid)
            {
                Request.GetOwinContext().Authentication.SignOut("Cookies");
            }
        }

    }
}