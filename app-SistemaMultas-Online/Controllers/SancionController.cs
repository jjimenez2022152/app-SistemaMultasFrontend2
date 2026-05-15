using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using static app_SistemaMultas_Online.Models.csEstructuraSancion;

namespace App_SistemaMultas_Online2.Controllers
{
    public class SancionController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sancion(string id_sancion)
        {
            DataSet dsi = new DataSet();
            var url = "";

            if (id_sancion == null)
                url = $"http://localhost/SistemaMultas/rest/api/listarSanciones";
            else
                url = $"http://localhost/SistemaMultas/rest/api/listarSancionesXid?id_sancion=" + id_sancion;

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
            catch (Exception ex) { }

            return View(dsi);
        }

        public ActionResult newSancion()
        {
            return View();
        }

        public ActionResult guardar(FormCollection formCollection)
        {
            try
            {
                requestSancion insertar = new requestSancion();

                insertar.id_sancion = Convert.ToInt32(formCollection["id_sancion"]);
                insertar.descripcion = formCollection["descripcion"];
                insertar.monto = Convert.ToDecimal(formCollection["monto"]);

                var url = "http://localhost/SistemaMultas/rest/api/insertarSancion";
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

                TempData["Success"] = "Sanción guardada exitosamente";
                return RedirectToAction("Sancion");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al guardar: " + ex.Message;
                return View("newSancion");
            }
        }

        public ActionResult actualizarSancion(string id_sancion)
        {
            DataSet dsi = new DataSet();
            var url = "";

            if (id_sancion == null)
                url = $"http://localhost/SistemaMultas/rest/api/listarSanciones";
            else
                url = $"http://localhost/SistemaMultas/rest/api/listarSancionesXid?id_sancion=" + id_sancion;

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
            requestSancion actualizar = new requestSancion();

            actualizar.id_sancion = Convert.ToInt32(formCollection["id_sancion"]);
            actualizar.descripcion = formCollection["descripcion"];
            actualizar.monto = Convert.ToDecimal(formCollection["monto"]);

            string json = JsonConvert.SerializeObject(actualizar);

            WebClient webClient = new WebClient();
            string url = $"http://localhost/SistemaMultas/rest/api/actualizarSancion";

            webClient.Headers["content-type"] = "application/json";

            byte[] reqString = Encoding.UTF8.GetBytes(json);
            byte[] resByte = webClient.UploadData(url, "POST", reqString);
            string resultJson = Encoding.UTF8.GetString(resByte);

            responseSancion result = JsonConvert.DeserializeObject<responseSancion>(resultJson);

            webClient.Dispose();

            if (result.respuesta == 1)
                return RedirectToAction("Sancion");

            return RedirectToAction("actualizarSancion", new { id = actualizar.id_sancion });
        }

        public ActionResult eliminar(int id_sancion)
        {
            WebClient webClient = new WebClient();

            requestEliminarSancion eliminar = new requestEliminarSancion();
            eliminar.id_sancion = id_sancion;

            string json = JsonConvert.SerializeObject(eliminar);

            webClient.Headers["content-type"] = "application/json";

            byte[] reqString = Encoding.UTF8.GetBytes(json);
            webClient.UploadData("http://localhost/SistemaMultas/rest/api/eliminarSancion", "POST", reqString);

            webClient.Dispose();

            return RedirectToAction("Sancion");
        }
    }
}