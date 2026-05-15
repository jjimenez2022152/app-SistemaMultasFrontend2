using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using static app_SistemaMultas_Online.Models.csConductores;


namespace app_SistemaMultasGenesis.Controllers
{
    public class ConductoresController : Controller
    {
        // GET: Conductor
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Conductores(string id_Conductor)
        {
            DataSet dsi = new DataSet();
            var url = "";
            if (id_Conductor == null)

                url = $"http://localhost/SistemaMultas/rest/api/listarConductores";
            else
                url = $"http://localhost/SistemaMultas/rest/api/listarConductoresXid?id_conductor=" + id_Conductor;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            string responseBody;
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            responseBody = objReader.ReadToEnd();
                        }
                    }
                    dsi = JsonConvert.DeserializeObject<DataSet>(responseBody);
                }
            }
            catch (Exception ex)
            {

            }

            return View(dsi);
        }

        public ActionResult newConductor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult guardar(FormCollection formCollection)
        {

            try
            {
                requestConductor insertar = new requestConductor();

                insertar.id_conductor = Convert.ToInt32(formCollection["id_conductor"]);
                insertar.nombre = formCollection["nombre"];
                insertar.dpi = formCollection["dpi"];

                var url = "http://localhost/SistemaMultas/rest/api/insertarConductor";
                var request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "application/json";

                string jsonBody = JsonConvert.SerializeObject(insertar);
                byte[] data = Encoding.UTF8.GetBytes(jsonBody);
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                using (WebResponse response = request.GetResponse())
                using (Stream strReader = response.GetResponseStream())
                using (StreamReader objReader = new StreamReader(strReader))
                {
                    string responseBody = objReader.ReadToEnd();
                }

                TempData["Success"] = "el Conductor quedo guardado exitosamente";
                return RedirectToAction("Conductores");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al guardar: " + ex.Message;
                return View("newConductor");
            }

        }
        public ActionResult actualizarConductor(string id_Conductor)
        {
            DataSet dsi = new DataSet();
            var url = "";

            if (id_Conductor == null)
                url = $"http://localhost/SistemaMultas/rest/api/listarConductores";
            else
                url = $"http://localhost/SistemaMultas/rest/api/listarConductoresXid?id_conductor=" + id_Conductor;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                using (Stream strReader = response.GetResponseStream())
                using (StreamReader objReader = new StreamReader(strReader))
                {
                    string responseBody = objReader.ReadToEnd();
                    dsi = JsonConvert.DeserializeObject<DataSet>(responseBody);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }

            return View(dsi);
        }

        public ActionResult actualizar(FormCollection formCollection)
        {
            requestConductor actualizar = new requestConductor();

            actualizar.id_conductor = Convert.ToInt32(formCollection["id_conductor"]);
            actualizar.nombre = formCollection["nombre"];
            actualizar.dpi = formCollection["dpi"];

            string json = JsonConvert.SerializeObject(actualizar);

            WebClient webClient = new WebClient();
            string url = $"http://localhost/SistemaMultas/rest/api/actualizarConductor";

            webClient.Headers["content-type"] = "application/json";

            byte[] reqString = Encoding.UTF8.GetBytes(json);
            byte[] resByte = webClient.UploadData(url, "POST", reqString);
            string resultJson = Encoding.UTF8.GetString(resByte);

            responseConductor result = JsonConvert.DeserializeObject<responseConductor>(resultJson);

            webClient.Dispose();

            if (result.respuesta == 1)
                return RedirectToAction("Conductores", "Conductores");

            return RedirectToAction("actualizarConductor", "Conductores");
        }

        public ActionResult eliminar(int id_Conductor)
        {
            WebClient webClient = new WebClient();

            requestEliminarConductor eliminar = new requestEliminarConductor();

            eliminar.id_conductor = id_Conductor;

            string json = JsonConvert.SerializeObject(eliminar);

            webClient.Headers["content-type"] = "application/json";

            byte[] reqString = Encoding.UTF8.GetBytes(json);
            byte[] resByte = webClient.UploadData("http://localhost/SistemaMultas/rest/api/eliminarConductor", "POST", reqString);

            webClient.Dispose();

            return RedirectToAction("Conductores");
        }
    }
}