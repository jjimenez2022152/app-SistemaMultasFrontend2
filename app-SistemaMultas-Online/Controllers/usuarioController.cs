using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using static app_SistemaMultas_Online.Models.csEstructuraUsuario;

namespace app_SistemaMultas_Online.Controllers
{
    public class usuarioController : Controller
    {
        // GET: usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuario(string id_Usuario)
        {
            DataSet dsi = new DataSet();
            var url = "";
            if (id_Usuario == null)
                url = $"http://localhost/SistemaMultas/rest/api/listarUsuarios";
            else
                url = $"http://localhost/SistemaMultas/rest/api/listarUsuariosXid?id_usuario=" + id_Usuario;


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

        public ActionResult newUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult guardar(FormCollection formCollection)
        {
            requestUsuario insertar = new requestUsuario();

            insertar.id_usuario = Convert.ToInt32(formCollection["id_usuario"].ToString());
            insertar.username = formCollection["username"];
            insertar.password = formCollection["password"];
            insertar.id_agente = Convert.ToInt32(formCollection["id_agente"].ToString());

            var url = $"http://localhost/SistemaMultas/rest/api/insertarUsuario";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            string jsonBody = JsonConvert.SerializeObject(insertar);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonBody);

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

                return RedirectToAction("Usuario");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("newUsuario");
            }
        }

        public ActionResult actualizarUsuario(string id_Usuario)
        {
            DataSet dsi = new DataSet();
            var url = "";
            if (id_Usuario == null)
                url = $"http://localhost/SistemaMultas/rest/api/listarUsuarios";
            else
                url = $"http://localhost/SistemaMultas/rest/api/listarUsuariosXid?id_usuario=" + id_Usuario;


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

            requestUsuario actualizar = new requestUsuario();
            actualizar.id_usuario = Convert.ToInt32(formCollection["id_usuario"]);
            actualizar.username = formCollection["username"];
            actualizar.password = formCollection["password"];
            actualizar.id_agente = Convert.ToInt32(formCollection["id_agente"]);

            json = JsonConvert.SerializeObject(actualizar);

            WebClient webClient = new WebClient();
            string url = $"http://localhost/SistemaMultas/rest/api/actualizarUsuario";
            var request = (HttpWebRequest)WebRequest.Create(url);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            webClient.Headers["content-type"] = "application/json";
            reqString = Encoding.UTF8.GetBytes(json);
            resByte = webClient.UploadData(request.Address.ToString(), "POST", reqString);
            resultJson = Encoding.UTF8.GetString(resByte);

            responseUsuario result = new responseUsuario();
            result = JsonConvert.DeserializeObject<responseUsuario>(resultJson);
            webClient.Dispose();

            if (result.respuesta == 1)
                return RedirectToAction("Usuario", "Usuario");

            return RedirectToAction("actualizarUsuario", "Usuario", new { id = actualizar.id_usuario });
        }

        public ActionResult eliminar(int id_usuario)
        {
            string json, resultJson;
            Byte[] resByte, reqString;

            WebClient webClient = new WebClient();
            string url = $"http://localhost/SistemaMultas/rest/api/eliminarUsuario";
            var request = (HttpWebRequest)WebRequest.Create(url);

            requestEliminarUsuario eliminar = new requestEliminarUsuario();
            eliminar.id_usuario = id_usuario;

            json = JsonConvert.SerializeObject(eliminar);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            webClient.Headers["content-type"] = "application/json";

            reqString = Encoding.UTF8.GetBytes(json);
            resByte = webClient.UploadData(request.Address.ToString(), "POST", reqString);
            resultJson = Encoding.UTF8.GetString(resByte);

            responseUsuario result = new responseUsuario();
            result = JsonConvert.DeserializeObject<responseUsuario>(resultJson);
            webClient.Dispose();

            return RedirectToAction("Usuario", "usuario");
        }

    }

    
}