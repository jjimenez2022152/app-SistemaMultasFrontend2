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
using static app_SistemaMultas_Online.Models.csEstructuraAgente;


namespace App_SistemaMultas_Online2.Controllers
{
    public class AgenteController : Controller
    {

        // GET: Conductores
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Agente(string id_agente)
        {
            DataSet dsi = new DataSet();
            var url = "";

            if (id_agente == null)
                url = $"http://localhost/SistemaMultas/rest/api/listarAgentes";

            else
                url = $"http://localhost/SistemaMultas/rest/api/listarAgentesXid?id_agente=" + id_agente;

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

        public ActionResult newAgente()
        {
            return View();
        }

        public ActionResult guardar(FormCollection formCollection)
        {

            try
            {
                requestAgente insertar = new requestAgente();

                insertar.id_agente = Convert.ToInt32(formCollection["id_agente"]);
                insertar.nombre = formCollection["nombre"];


                var url = "http://localhost/SistemaMultas/rest/api/insertarAgente";
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

                TempData["Success"] = "Agente guardada exitosamente";
                return RedirectToAction("Agente");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al guardar: " + ex.Message;
                return View("newAgente");
            }

        }

        public ActionResult actualizarAgente(string id_agente)
        {
            DataSet dsi = new DataSet();
            var url = "";

            if (id_agente == null)
                url = $"http://localhost/SistemaMultas/rest/api/listarAgentes";
            else
                url = $"http://localhost/SistemaMultas/rest/api/listarAgentesXid?id_agente=" + id_agente;

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
            requestAgente actualizar = new requestAgente();

            actualizar.id_agente = Convert.ToInt32(formCollection["id_agente"]);
            actualizar.nombre = formCollection["nombre"];

            string json = JsonConvert.SerializeObject(actualizar);

            WebClient webClient = new WebClient();
            string url = $"http://localhost/SistemaMultas/rest/api/actualizarAgente";

            webClient.Headers["content-type"] = "application/json";

            byte[] reqString = Encoding.UTF8.GetBytes(json);
            byte[] resByte = webClient.UploadData(url, "POST", reqString);
            string resultJson = Encoding.UTF8.GetString(resByte);

            responseAgente result = JsonConvert.DeserializeObject<responseAgente>(resultJson);

            webClient.Dispose();

            if (result.respuesta == 1)
                return RedirectToAction("Agente");

            return RedirectToAction("actualizarAgente", new { id = actualizar.id_agente });
        }

        public ActionResult eliminar(int id_agente)
        {
            WebClient webClient = new WebClient();

            requestEliminarAgente eliminar = new requestEliminarAgente();
            eliminar.id_agente = id_agente;

            string json = JsonConvert.SerializeObject(eliminar);

            webClient.Headers["content-type"] = "application/json";

            byte[] reqString = Encoding.UTF8.GetBytes(json);
            byte[] resByte = webClient.UploadData("http://localhost/SistemaMultas/rest/api/eliminarAgente", "POST", reqString);

            webClient.Dispose();

            return RedirectToAction("Agente");
        }
    }
}