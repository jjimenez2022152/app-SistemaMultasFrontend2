using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using static app_SistemaMultas_Online.Models.csEstructuraPago;

namespace app_SistemaMultas_Online.Controllers
{
    public class PagoController : Controller
    {
        // GET: Pago
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pago(string id_pago)
        {
            DataSet dsi = new DataSet();
            var url = "";

            if (id_pago == null)
                url = $"http://localhost/SistemaMultas/rest/api/listarPagos";
            else
                url = $"http://localhost/SistemaMultas/rest/api/listarPagosXid?id_pago=" + id_pago;

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

        public ActionResult newPago()
        {
            return View();
        }

        [HttpPost]
        public ActionResult guardar(FormCollection formCollection)
        {
            try
            {
                requestPago insertar = new requestPago();

                insertar.id_pago = Convert.ToInt32(formCollection["id_pago"]);
                insertar.fecha_pago = DateTime.Parse(formCollection["fecha_pago"]); // 🔥 IMPORTANTE
                insertar.monto = Convert.ToDecimal(formCollection["monto"]);
                insertar.id_infraccion = Convert.ToInt32(formCollection["id_infraccion"]);

                var url = "http://localhost/SistemaMultas/rest/api/insertarPago";
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

                TempData["Success"] = "Pago guardado exitosamente";
                return RedirectToAction("Pago");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al guardar: " + ex.Message;
                return View("newPago");
            }
        }

        public ActionResult actualizarPago(string id_pago)
        {
            DataSet dsi = new DataSet();
            var url = "";

            if (id_pago == null)
                url = $"http://localhost/SistemaMultas/rest/api/listarPagos";
            else
                url = $"http://localhost/SistemaMultas/rest/api/listarPagosXid?id_pago=" + id_pago;

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
            requestPago actualizar = new requestPago();

            actualizar.id_pago = Convert.ToInt32(formCollection["id_pago"]);
            actualizar.fecha_pago = Convert.ToDateTime(formCollection["fecha_pago"]); // 🔥 IMPORTANTE
            actualizar.monto = Convert.ToDecimal(formCollection["monto"]);
            actualizar.id_infraccion = Convert.ToInt32(formCollection["id_infraccion"]);

            string json = JsonConvert.SerializeObject(actualizar);

            WebClient webClient = new WebClient();
            string url = $"http://localhost/SistemaMultas/rest/api/actualizarPago";

            webClient.Headers["content-type"] = "application/json";

            byte[] reqString = Encoding.UTF8.GetBytes(json);
            byte[] resByte = webClient.UploadData(url, "POST", reqString);
            string resultJson = Encoding.UTF8.GetString(resByte);

            responsePago result = JsonConvert.DeserializeObject<responsePago>(resultJson);

            webClient.Dispose();

            if (result.respuesta == 1)
                return RedirectToAction("Pago");

            return RedirectToAction("actualizarPago", new { id = actualizar.id_pago });
        }

        public ActionResult eliminar(int id_pago)
        {
            WebClient webClient = new WebClient();

            requestEliminarPago eliminar = new requestEliminarPago();
            eliminar.id_pago = id_pago;

            string json = JsonConvert.SerializeObject(eliminar);

            webClient.Headers["content-type"] = "application/json";

            byte[] reqString = Encoding.UTF8.GetBytes(json);
            byte[] resByte = webClient.UploadData("http://localhost/SistemaMultas/rest/api/eliminarPago", "POST", reqString);

            webClient.Dispose();

            return RedirectToAction("Pago");
        }
    }
}