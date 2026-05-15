using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;
using static app_SistemaMultas_Online.Models.csEstructuraVehiculo;

namespace app_SistemaMultas_Online.Controllers
{
    public class VehiculoController : Controller
    {
        // GET: Vehiculo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Vehiculo(string id_vehiculo)
        {
            DataSet dsi = new DataSet();
            var url = "";

            if (id_vehiculo == null)
                url = $"http://localhost/SistemaMultas/rest/api/listarVehiculos";
            else
                url = $"http://localhost/SistemaMultas/rest/api/listarVehiculosXid?id_vehiculo=" + id_vehiculo;

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
                ViewBag.Error = ex.Message;
            }

            return View(dsi);
        }

        public ActionResult newVehiculo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult guardar(FormCollection formCollection)
        {
            requestVehiculo insertar = new requestVehiculo();

            insertar.id_vehiculo = Convert.ToInt32(formCollection["id_vehiculo"].ToString());
            insertar.placa = formCollection["placa"];
            insertar.marca = formCollection["marca"];
            insertar.id_conductor = Convert.ToInt32(formCollection["id_conductor"].ToString());

            var url = $"http://localhost/SistemaMultas/rest/api/insertarVehiculo";
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            string jsonBody = JsonConvert.SerializeObject(insertar);
            byte[] data = Encoding.UTF8.GetBytes(jsonBody);

            try
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                        }
                    }
                }

                return RedirectToAction("Vehiculo");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("newVehiculo");
            }
        }

        public ActionResult actualizarVehiculo(string id_vehiculo)
        {
            DataSet dsi = new DataSet();
            var url = "";

            if (id_vehiculo == null)
                url = $"http://localhost/SistemaMultas/rest/api/listarVehiculos";
            else
                url = $"http://localhost/SistemaMultas/rest/api/listarVehiculosXid?id_vehiculo=" + id_vehiculo;

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
                ViewBag.Error = ex.Message;
            }

            return View(dsi);
        }

        public ActionResult actualizar(FormCollection formCollection)
        {
            string json, resultJson = "";
            Byte[] reqString, resByte;

            requestVehiculo actualizar = new requestVehiculo();

            actualizar.id_vehiculo = Convert.ToInt32(formCollection["id_vehiculo"]);
            actualizar.placa = formCollection["placa"];
            actualizar.marca = formCollection["marca"];
            actualizar.id_conductor = Convert.ToInt32(formCollection["id_conductor"]);

            json = JsonConvert.SerializeObject(actualizar);

            WebClient webClient = new WebClient();
            string url = $"http://localhost/SistemaMultas/rest/api/actualizarVehiculo";

            var request = (HttpWebRequest)WebRequest.Create(url);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            webClient.Headers["content-type"] = "application/json";

            reqString = Encoding.UTF8.GetBytes(json);

            resByte = webClient.UploadData(request.Address.ToString(), "POST", reqString);

            resultJson = Encoding.UTF8.GetString(resByte);

            responseVehiculo result = new responseVehiculo();

            result = JsonConvert.DeserializeObject<responseVehiculo>(resultJson);

            webClient.Dispose();

            if (result.respuesta == 1)
                return RedirectToAction("Vehiculo", "Vehiculo");

            return RedirectToAction("actualizarVehiculo", "Vehiculo", new { id = actualizar.id_vehiculo });
        }

        public ActionResult eliminar(int id_vehiculo)
        {
            string json, resultJson;
            Byte[] resByte, reqString;

            WebClient webClient = new WebClient();

            string url = $"http://localhost/SistemaMultas/rest/api/eliminarVehiculo";

            var request = (HttpWebRequest)WebRequest.Create(url);

            requestEliminarVehiculo eliminar = new requestEliminarVehiculo();

            eliminar.id_vehiculo = id_vehiculo;

            json = JsonConvert.SerializeObject(eliminar);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            webClient.Headers["content-type"] = "application/json";

            reqString = Encoding.UTF8.GetBytes(json);

            resByte = webClient.UploadData(request.Address.ToString(), "POST", reqString);

            resultJson = Encoding.UTF8.GetString(resByte);

            responseVehiculo result = new responseVehiculo();

            result = JsonConvert.DeserializeObject<responseVehiculo>(resultJson);

            webClient.Dispose();

            return RedirectToAction("Vehiculo", "Vehiculo");
        }
    }
}