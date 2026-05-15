using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using static app_SistemaMultas_Online.Models.csEstructuraEstadoInfraccion;

namespace app_SistemaMultas_Online.Controllers
{
    public class EstadoInfraccionController : Controller
    {
        // GET: EstadoInfraccion
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EstadoInfraccion(string id_estado)
        {
            DataSet dsi = new DataSet();
            var url = "";

            if (id_estado == null)
                url = $"http://localhost/SistemaMultas/rest/api/listarEstadosInfraccion";
            else
                url = $"http://localhost/SistemaMultas/rest/api/listarEstadosInfraccionXid?id_estado=" + id_estado;

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

        public ActionResult newEstadoInfraccion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult guardar(FormCollection formCollection)
        {
            try
            {
                requestEstadoInfraccion insertar = new requestEstadoInfraccion();

                insertar.id_estado = Convert.ToInt32(formCollection["id_estado"]);
                insertar.descripcion = formCollection["descripcion"];

                var url = "http://localhost/SistemaMultas/rest/api/insertarEstadoInfraccion";

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

                TempData["Success"] = "Estado de infracción guardado exitosamente";

                return RedirectToAction("EstadoInfraccion");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al guardar: " + ex.Message;
                return View("newEstadoInfraccion");
            }
        }

        public ActionResult actualizarEstadoInfraccion(string id_estado)
        {
            DataSet dsi = new DataSet();
            var url = "";

            if (id_estado == null)
                url = $"http://localhost/SistemaMultas/rest/api/listarEstadosInfraccion";
            else
                url = $"http://localhost/SistemaMultas/rest/api/listarEstadosInfraccionXid?id_estado=" + id_estado;

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
            requestEstadoInfraccion actualizar = new requestEstadoInfraccion();

            actualizar.id_estado = Convert.ToInt32(formCollection["id_estado"]);
            actualizar.descripcion = formCollection["descripcion"];

            string json = JsonConvert.SerializeObject(actualizar);

            WebClient webClient = new WebClient();

            string url = $"http://localhost/SistemaMultas/rest/api/actualizarEstadoInfraccion";

            webClient.Headers["content-type"] = "application/json";

            byte[] reqString = Encoding.UTF8.GetBytes(json);

            byte[] resByte = webClient.UploadData(url, "POST", reqString);

            string resultJson = Encoding.UTF8.GetString(resByte);

            responseEstadoInfraccion result =
                JsonConvert.DeserializeObject<responseEstadoInfraccion>(resultJson);

            webClient.Dispose();

            if (result.respuesta == 1)
                return RedirectToAction("EstadoInfraccion");

            return RedirectToAction("actualizarEstadoInfraccion",
                new { id = actualizar.id_estado });
        }

        public ActionResult eliminar(int id_estado)
        {
            WebClient webClient = new WebClient();

            requestEliminarEstadoInfraccion eliminar =
                new requestEliminarEstadoInfraccion();

            eliminar.id_estado = id_estado;

            string json = JsonConvert.SerializeObject(eliminar);

            webClient.Headers["content-type"] = "application/json";

            byte[] reqString = Encoding.UTF8.GetBytes(json);

            byte[] resByte = webClient.UploadData(
                "http://localhost/SistemaMultas/rest/api/eliminarEstadoInfraccion",
                "POST",
                reqString
            );

            webClient.Dispose();

            return RedirectToAction("EstadoInfraccion");
        }
    }
}