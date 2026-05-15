using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using app_SistemaMultas_Online.Models;
using static app_SistemaMultas_Online.Models.csEstructuraLogin;

namespace app_SistemaMultas_Online.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult validar(FormCollection formCollection)
        {
            try
            {
                requestLogin login = new requestLogin();

                login.username = formCollection["username"];
                login.password = formCollection["password"];

                var url = "http://localhost/SistemaMultas/rest/api/loginUsuario";

                var request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "application/json";

                string jsonBody = JsonConvert.SerializeObject(login);

                byte[] data = Encoding.UTF8.GetBytes(jsonBody);

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                responseLogin result;

                using (WebResponse response = request.GetResponse())
                using (Stream strReader = response.GetResponseStream())
                using (StreamReader objReader = new StreamReader(strReader))
                {
                    string responseBody = objReader.ReadToEnd();

                    result = JsonConvert
                        .DeserializeObject<responseLogin>(responseBody);
                }

                if (result.respuesta == 1)
                {
                    Session["usuario"] = login.username;

                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Error = result.descripcion_respuesta;

                return View("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return View("Index");
            }
        }

        public ActionResult logout()
        {
            Session.Clear();

            return RedirectToAction("Index");
        }
    }
}