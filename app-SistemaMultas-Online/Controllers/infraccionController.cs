using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Net;
using System.Text;
using System.Web.Mvc;
using static app_SistemaMultas_Online.Models.csEstructuraInfraccion;

namespace app_SistemaMultas_Online.Controllers
{
    public class InfraccionController : Controller
    {
        // GET: Infraccion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Infraccion(string id_infraccion)
        {
            DataSet dsi = new DataSet();
            var url = "";

            if (id_infraccion == null)
                url = $"http://localhost/SistemaMultas/rest/api/listarInfracciones";
            else
                url = $"http://localhost/SistemaMultas/rest/api/listarInfraccionesXid?id_infraccion=" + id_infraccion;

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

        public ActionResult newInfraccion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult guardar(FormCollection formCollection)
        {

            try
            {
                requestInfraccion insertar = new requestInfraccion();

                insertar.id_infraccion = Convert.ToInt32(formCollection["id_infraccion"]);
                insertar.fecha = DateTime.Parse(formCollection["fecha"]); ; // 🔥 IMPORTANTE
                insertar.lugar = formCollection["lugar"];
                insertar.id_vehiculo = Convert.ToInt32(formCollection["id_vehiculo"]);
                insertar.id_agente = Convert.ToInt32(formCollection["id_agente"]);
                insertar.id_sancion = Convert.ToInt32(formCollection["id_sancion"]);
                insertar.id_estado = Convert.ToInt32(formCollection["id_estado"]);

                var url = "http://localhost/SistemaMultas/rest/api/insertarInfraccion";
                var request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "application/json";

                string jsonBody = JsonConvert.SerializeObject(insertar);
                byte[] data = Encoding.UTF8.GetBytes(jsonBody);

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

                TempData["Success"] = "Infracción guardada exitosamente";
                return RedirectToAction("Infraccion");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al guardar: " + ex.Message;
                return View("newInfraccion");
            }

        }

        public ActionResult actualizarInfraccion(string id_infraccion)
        {
            DataSet dsi = new DataSet();
            var url = "";

            if (id_infraccion == null)
                url = $"http://localhost/SistemaMultas/rest/api/listarInfracciones";
            else
                url = $"http://localhost/SistemaMultas/rest/api/listarInfraccionesXid?id_infraccion=" + id_infraccion;

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
            requestInfraccion actualizar = new requestInfraccion();

            actualizar.id_infraccion = Convert.ToInt32(formCollection["id_infraccion"]);
            actualizar.fecha = Convert.ToDateTime(formCollection["fecha"]); // 🔥 IMPORTANTE
            actualizar.lugar = formCollection["lugar"];
            actualizar.id_vehiculo = Convert.ToInt32(formCollection["id_vehiculo"]);
            actualizar.id_agente = Convert.ToInt32(formCollection["id_agente"]);
            actualizar.id_sancion = Convert.ToInt32(formCollection["id_sancion"]);
            actualizar.id_estado = Convert.ToInt32(formCollection["id_estado"]);

            string json = JsonConvert.SerializeObject(actualizar);

            WebClient webClient = new WebClient();
            string url = $"http://localhost/SistemaMultas/rest/api/actualizarInfraccion";

            webClient.Headers["content-type"] = "application/json";

            byte[] reqString = Encoding.UTF8.GetBytes(json);
            byte[] resByte = webClient.UploadData(url, "POST", reqString);
            string resultJson = Encoding.UTF8.GetString(resByte);

            responseInfraccion result = JsonConvert.DeserializeObject<responseInfraccion>(resultJson);

            webClient.Dispose();

            if (result.respuesta == 1)
                return RedirectToAction("Infraccion");

            return RedirectToAction("actualizarInfraccion", new { id = actualizar.id_infraccion });
        }

        public ActionResult eliminar(int id_infraccion)
        {
            WebClient webClient = new WebClient();

            requestEliminarInfraccion eliminar = new requestEliminarInfraccion();
            eliminar.id_infraccion = id_infraccion;

            string json = JsonConvert.SerializeObject(eliminar);

            webClient.Headers["content-type"] = "application/json";

            byte[] reqString = Encoding.UTF8.GetBytes(json);
            byte[] resByte = webClient.UploadData("http://localhost/SistemaMultas/rest/api/eliminarInfraccion", "POST", reqString);

            webClient.Dispose();

            return RedirectToAction("Infraccion");
        }
    }
}