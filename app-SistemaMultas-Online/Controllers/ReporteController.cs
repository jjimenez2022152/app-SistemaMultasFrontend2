using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace app_SistemaMultas_Online.Controllers
{
    public class ReporteController : Controller
    {

        public ActionResult SolvenciaVehicular(int id_vehiculo)
        {
            DataSet dsi = new DataSet();

            string url = "http://localhost/SistemaMultas/rest/api/solvenciaVehicular?id_vehiculo=" + id_vehiculo;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            using (WebResponse response = request.GetResponse())
            using (Stream strReader = response.GetResponseStream())
            using (StreamReader objReader = new StreamReader(strReader))
            {
                string responseBody = objReader.ReadToEnd();
                dsi = JsonConvert.DeserializeObject<DataSet>(responseBody);
            }

            MemoryStream stream = new MemoryStream();

            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);

            PdfWriter.GetInstance(pdfDoc, stream).CloseStream = false;

            pdfDoc.Open();

            Paragraph titulo = new Paragraph("SOLVENCIA VEHICULAR");
            titulo.Alignment = Element.ALIGN_CENTER;

            pdfDoc.Add(titulo);

            pdfDoc.Add(new Paragraph(" "));

            if (dsi.Tables.Count > 0 && dsi.Tables[0].Rows.Count > 0)
            {
                PdfPTable tabla = new PdfPTable(8);

                tabla.WidthPercentage = 100;

                tabla.AddCell("Placa");
                tabla.AddCell("Marca");
                tabla.AddCell("Conductor");
                tabla.AddCell("ID Infracción");
                tabla.AddCell("Fecha");
                tabla.AddCell("Sanción");
                tabla.AddCell("Monto");
                tabla.AddCell("Estado");

                foreach (DataRow row in dsi.Tables[0].Rows)
                {
                    tabla.AddCell(row["placa"].ToString());
                    tabla.AddCell(row["marca"].ToString());
                    tabla.AddCell(row["conductor"].ToString());
                    tabla.AddCell(row["id_infraccion"].ToString());
                    tabla.AddCell(Convert.ToDateTime(row["fecha"]).ToString("dd/MM/yyyy"));
                    tabla.AddCell(row["sancion"].ToString());
                    tabla.AddCell(row["monto"].ToString());
                    tabla.AddCell(row["estado"].ToString());
                }

                pdfDoc.Add(tabla);
            }
            else
            {
                Paragraph solvencia = new Paragraph("El vehículo no posee infracciones pendientes.");
                solvencia.Alignment = Element.ALIGN_CENTER;

                pdfDoc.Add(solvencia);
            }

            pdfDoc.Close();

            stream.Position = 0;

            return File(stream.ToArray(),
                        "application/pdf",
                        "SolvenciaVehicular.pdf");
        }


        // REPORTE AGENTES

        public ActionResult ReporteAgentes()
        {
            DataSet dsi = new DataSet();

            string url = "http://localhost/SistemaMultas/rest/api/listarAgentes";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            using (WebResponse response = request.GetResponse())
            using (Stream strReader = response.GetResponseStream())
            using (StreamReader objReader = new StreamReader(strReader))
            {
                string responseBody = objReader.ReadToEnd();
                dsi = JsonConvert.DeserializeObject<DataSet>(responseBody);
            }

            MemoryStream stream = new MemoryStream();

            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);

            PdfWriter.GetInstance(pdfDoc, stream).CloseStream = false;

            pdfDoc.Open();

            Paragraph titulo = new Paragraph("Reporte de Agentes");
            titulo.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(titulo);

            pdfDoc.Add(new Paragraph(" "));

            PdfPTable tabla = new PdfPTable(2);

            tabla.WidthPercentage = 100;

            tabla.AddCell("ID Agente");
            tabla.AddCell("Nombre");

            foreach (DataRow row in dsi.Tables[0].Rows)
            {
                tabla.AddCell(row["id_agente"].ToString());
                tabla.AddCell(row["nombre"].ToString());
            }

            pdfDoc.Add(tabla);

            pdfDoc.Close();

            stream.Position = 0;

            return File(stream.ToArray(),
                        "application/pdf",
                        "ReporteAgentes.pdf");
        }



        // REPORTE CONDUCTORES

        public ActionResult ReporteConductores()
        {
            DataSet dsi = new DataSet();

            string url = "http://localhost/SistemaMultas/rest/api/listarConductores";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            using (WebResponse response = request.GetResponse())
            using (Stream strReader = response.GetResponseStream())
            using (StreamReader objReader = new StreamReader(strReader))
            {
                string responseBody = objReader.ReadToEnd();
                dsi = JsonConvert.DeserializeObject<DataSet>(responseBody);
            }

            MemoryStream stream = new MemoryStream();

            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);

            PdfWriter.GetInstance(pdfDoc, stream).CloseStream = false;

            pdfDoc.Open();

            Paragraph titulo = new Paragraph("Reporte de Conductores");
            titulo.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(titulo);

            pdfDoc.Add(new Paragraph(" "));

            PdfPTable tabla = new PdfPTable(3);

            tabla.WidthPercentage = 100;

            tabla.AddCell("ID Conductor");
            tabla.AddCell("Nombre");
            tabla.AddCell("DPI");

            foreach (DataRow row in dsi.Tables[0].Rows)
            {
                tabla.AddCell(row["id_conductor"].ToString());
                tabla.AddCell(row["nombre"].ToString());
                tabla.AddCell(row["dpi"].ToString());
            }

            pdfDoc.Add(tabla);

            pdfDoc.Close();

            stream.Position = 0;

            return File(stream.ToArray(),
                        "application/pdf",
                        "ReporteConductores.pdf");
        }



        // REPORTE VEHICULOS
        public ActionResult ReporteVehiculos()
        {
            DataSet dsi = new DataSet();

            string url = "http://localhost/SistemaMultas/rest/api/listarVehiculos";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            using (WebResponse response = request.GetResponse())
            using (Stream strReader = response.GetResponseStream())
            using (StreamReader objReader = new StreamReader(strReader))
            {
                string responseBody = objReader.ReadToEnd();
                dsi = JsonConvert.DeserializeObject<DataSet>(responseBody);
            }

            MemoryStream stream = new MemoryStream();

            Document pdfDoc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);

            PdfWriter.GetInstance(pdfDoc, stream).CloseStream = false;

            pdfDoc.Open();

            Paragraph titulo = new Paragraph("Reporte de Vehículos");
            titulo.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(titulo);

            pdfDoc.Add(new Paragraph(" "));

            PdfPTable tabla = new PdfPTable(5);

            tabla.WidthPercentage = 100;

            tabla.AddCell("ID Vehículo");
            tabla.AddCell("Placa");
            tabla.AddCell("Marca");
            tabla.AddCell("ID Conductor");

            foreach (DataRow row in dsi.Tables[0].Rows)
            {
                tabla.AddCell(row["id_vehiculo"].ToString());
                tabla.AddCell(row["placa"].ToString());
                tabla.AddCell(row["marca"].ToString());
                tabla.AddCell(row["id_conductor"].ToString());
            }

            pdfDoc.Add(tabla);

            pdfDoc.Close();

            stream.Position = 0;

            return File(stream.ToArray(),
                        "application/pdf",
                        "ReporteVehiculos.pdf");
        }



        // REPORTE Sanciones
        public ActionResult ReporteSanciones()
        {
            DataSet dsi = new DataSet();

            string url = "http://localhost/SistemaMultas/rest/api/listarSanciones";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            using (WebResponse response = request.GetResponse())
            using (Stream strReader = response.GetResponseStream())
            using (StreamReader objReader = new StreamReader(strReader))
            {
                string responseBody = objReader.ReadToEnd();
                dsi = JsonConvert.DeserializeObject<DataSet>(responseBody);
            }

            MemoryStream stream = new MemoryStream();

            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);

            PdfWriter.GetInstance(pdfDoc, stream).CloseStream = false;

            pdfDoc.Open();

            Paragraph titulo = new Paragraph("Reporte de Sanciones");
            titulo.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(titulo);

            pdfDoc.Add(new Paragraph(" "));

            PdfPTable tabla = new PdfPTable(3);

            tabla.WidthPercentage = 100;

            tabla.AddCell("ID Sanción");
            tabla.AddCell("Descripción");
            tabla.AddCell("Monto");

            foreach (DataRow row in dsi.Tables[0].Rows)
            {
                tabla.AddCell(row["id_sancion"].ToString());
                tabla.AddCell(row["descripcion"].ToString());
                tabla.AddCell(row["monto"].ToString());
            }

            pdfDoc.Add(tabla);

            pdfDoc.Close();

            stream.Position = 0;

            return File(stream.ToArray(),
                        "application/pdf",
                        "ReporteSanciones.pdf");
        }



        // REPORTE Infracciones
        public ActionResult ReporteInfracciones()
        {
            DataSet dsi = new DataSet();

            string url = "http://localhost/SistemaMultas/rest/api/listarInfracciones";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            using (WebResponse response = request.GetResponse())
            using (Stream strReader = response.GetResponseStream())
            using (StreamReader objReader = new StreamReader(strReader))
            {
                string responseBody = objReader.ReadToEnd();
                dsi = JsonConvert.DeserializeObject<DataSet>(responseBody);
            }

            MemoryStream stream = new MemoryStream();

            Document pdfDoc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);

            PdfWriter.GetInstance(pdfDoc, stream).CloseStream = false;

            pdfDoc.Open();

            Paragraph titulo = new Paragraph("Reporte de Infracciones");
            titulo.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(titulo);

            pdfDoc.Add(new Paragraph(" "));

            PdfPTable tabla = new PdfPTable(7);

            tabla.WidthPercentage = 100;

            tabla.AddCell("ID");
            tabla.AddCell("Fecha");
            tabla.AddCell("Lugar");
            tabla.AddCell("Vehículo");
            tabla.AddCell("Agente");
            tabla.AddCell("Sanción");
            tabla.AddCell("Estado");

            foreach (DataRow row in dsi.Tables[0].Rows)
            {
                tabla.AddCell(row["id_infraccion"].ToString());
                tabla.AddCell(row["fecha"].ToString());
                tabla.AddCell(row["lugar"].ToString());
                tabla.AddCell(row["id_vehiculo"].ToString());
                tabla.AddCell(row["id_agente"].ToString());
                tabla.AddCell(row["id_sancion"].ToString());
                tabla.AddCell(row["id_estado"].ToString());
            }

            pdfDoc.Add(tabla);

            pdfDoc.Close();

            stream.Position = 0;

            return File(stream.ToArray(),
                        "application/pdf",
                        "ReporteInfracciones.pdf");
        }


        // REPORTE PAGOS
        public ActionResult ReportePagos()
        {
            DataSet dsi = new DataSet();

            string url = "http://localhost/SistemaMultas/rest/api/listarPagos";

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            using (WebResponse response = request.GetResponse())
            using (Stream strReader = response.GetResponseStream())
            using (StreamReader objReader = new StreamReader(strReader))
            {
                string responseBody = objReader.ReadToEnd();
                dsi = JsonConvert.DeserializeObject<DataSet>(responseBody);
            }

            MemoryStream stream = new MemoryStream();

            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);

            PdfWriter.GetInstance(pdfDoc, stream).CloseStream = false;

            pdfDoc.Open();

            Paragraph titulo = new Paragraph("Reporte de Pagos");
            titulo.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(titulo);

            pdfDoc.Add(new Paragraph(" "));

            PdfPTable tabla = new PdfPTable(4);

            tabla.WidthPercentage = 100;

            tabla.AddCell("ID Pago");
            tabla.AddCell("Fecha Pago");
            tabla.AddCell("Monto");
            tabla.AddCell("ID Infracción");

            foreach (DataRow row in dsi.Tables[0].Rows)
            {
                tabla.AddCell(row["id_pago"].ToString());
                tabla.AddCell(row["fecha_pago"].ToString());
                tabla.AddCell(row["monto"].ToString());
                tabla.AddCell(row["id_infraccion"].ToString());
            }

            pdfDoc.Add(tabla);

            pdfDoc.Close();

            stream.Position = 0;

            return File(stream.ToArray(),
                        "application/pdf",
                        "ReportePagos.pdf");
        }
    }


}