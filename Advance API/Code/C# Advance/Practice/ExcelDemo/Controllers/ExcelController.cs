using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExcelDemo.Controllers
{
    /// <summary>
    /// Controller to handle Excel file upload and download functionality.
    /// </summary>
    [RoutePrefix("api/excel")]
    public class ExcelController : ApiController
    {
        /// <summary>
        /// Endpoint to upload an Excel file.
        /// Processes the uploaded file and extracts its data.
        /// </summary>
        /// <returns>
        /// Returns a success message with the extracted data or an error message.
        /// </returns>
        [HttpPost]
        [Route("upload")]
        public async Task<IHttpActionResult> UploadExcel()
        {
            // Check if the request contains multipart form data.
            if (!Request.Content.IsMimeMultipartContent("form-data"))
                return BadRequest("Unsupported media type.");

            // Initialize a memory stream provider to handle uploaded content.
            MultipartMemoryStreamProvider objMultipartMemoryStreamProvider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(objMultipartMemoryStreamProvider);

            // Process each uploaded file.
            foreach (HttpContent objHttpContent in objMultipartMemoryStreamProvider.Contents)
            {
                // Get the file name and read the file's content into a byte array.
                string fileName = objHttpContent.Headers.ContentDisposition.FileName.Trim('\"');
                byte[] fileBytes = await objHttpContent.ReadAsByteArrayAsync();

                using (MemoryStream objMemoryStream = new MemoryStream(fileBytes))
                {
                    using (ExcelPackage objExcelPackage = new ExcelPackage(objMemoryStream))
                    {
                        // Access the first worksheet in the Excel file.
                        ExcelWorksheet objExcelWorksheet = objExcelPackage.Workbook.Worksheets[0];
                        int rows = objExcelWorksheet.Dimension.Rows; // Get the total number of rows.
                        int columns = objExcelWorksheet.Dimension.Columns; // Get the total number of columns.

                        // Extract data from the worksheet into a list of string arrays.
                        List<string[]> lstWorkSheetData = new List<string[]>();
                        for (int i = 1; i <= rows; i++)
                        {
                            List<string> lstRow = new List<string>();
                            for (int j = 1; j <= columns; j++)
                            {
                                lstRow.Add(objExcelWorksheet.Cells[i, j].Text);
                            }
                            lstWorkSheetData.Add(lstRow.ToArray());
                        }

                        // Return the extracted data to the client.
                        return Ok(new { Message = "File uploaded successfully.", Data = lstWorkSheetData });
                    }
                }
            }

            // Return an error if no files were processed.
            return BadRequest("No files to process.");
        }

        /// <summary>
        /// Endpoint to download a sample Excel file.
        /// Generates an Excel file with sample data and sends it to the client.
        /// </summary>
        /// <returns>
        /// Returns an HTTP response containing the generated Excel file.
        /// </returns>
        [HttpGet]
        [Route("download")]
        public HttpResponseMessage DownloadExcel()
        {
            using (ExcelPackage objExcelPackage = new ExcelPackage())
            {
                // Create a new worksheet in the Excel package and populate it with sample data.
                ExcelWorksheet objExcelWorksheet = objExcelPackage.Workbook.Worksheets.Add("Sample Data");
                objExcelWorksheet.Cells[1, 1].Value = "ID";
                objExcelWorksheet.Cells[1, 2].Value = "Name";
                objExcelWorksheet.Cells[1, 3].Value = "Age";

                objExcelWorksheet.Cells[2, 1].Value = 1;
                objExcelWorksheet.Cells[2, 2].Value = "Yash Khohkar";
                objExcelWorksheet.Cells[2, 3].Value = 20;

                objExcelWorksheet.Cells[3, 1].Value = 2;
                objExcelWorksheet.Cells[3, 2].Value = "Maulik Bhatt";
                objExcelWorksheet.Cells[3, 3].Value = 20;

                // Convert the Excel package to a byte array and create a memory stream.
                MemoryStream objMemoryStream = new MemoryStream(objExcelPackage.GetAsByteArray());

                // Create an HTTP response containing the generated Excel file.
                HttpResponseMessage objHttpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ByteArrayContent(objMemoryStream.ToArray())
                };
                objHttpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "SampleData.xlsx"
                };
                objHttpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

                // Return the response to the client.
                return objHttpResponseMessage;
            }
        }
    }
}