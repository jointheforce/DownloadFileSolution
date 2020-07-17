using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DownloadFile.Controllers
{
    public class TestController : Controller
    {
        [HttpGet("api/user")]
        public IActionResult Geta()
        {
            return Ok(new { Name = "Nick" });
        }

        // --
        //[HttpGet("api/file/{id}")]
        //public async Task<HttpResponseMessage> GetFile(int id)
        //{
        //    var root = @"C:\Users\DEVing\Desktop\";
        //    var fileName = "doc.pdf";
        //    var filePath = root + fileName;
        //    //MemoryStream memoryStream = new MemoryStream();
        //    var localFile = System.IO.File.ReadAllBytes(filePath);
        //    MemoryStream memoryStream = new MemoryStream(localFile);
        //    HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        //    {
        //        Content = new StreamContent(localFile)
        //    };
        //    var ext = Path.GetExtension(filePath).ToLowerInvariant();
        //    var contentType = GetMimeTypes()[ext];
        //    responseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

        //    return responseMessage;
        //}
        // --

        //[HttpGet("api/getfile/{id}")]
        //public HttpResponseMessage GetAFile(int id)
        //{
        //    var root = @"C:\Users\DEVing\Desktop\";
        //    var fileName = "doc.pdf";
        //    var filePath = root + fileName;
        //    var localFile = System.IO.File.ReadAllBytes(filePath);
        //    var stream = new MemoryStream(localFile);
        //    var result = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        //    {
        //        Content = new ByteArrayContent(stream.ToArray())
        //    };
        //    result.Content.Headers.ContentDisposition =
        //        new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
        //        {
        //            FileName = "Doc.pdf"
        //        };
        //    result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

        //    return result;
        //}
        [HttpGet("api/filealternative/{id}")]
        public IActionResult GetFileAlternative(int id)
        {
            var root = @"C:\Users\DEVing\Desktop\";
            var fileName = "doc.pdf";
            var filePath = root + fileName;
            return new PhysicalFileResult(filePath, "application/pdf");
        }
        [HttpGet("api/file/{id}")]
        public IActionResult GetFile(int id)
        {
            var root = @"C:\Users\DEVing\Desktop\";
            var fileName = "doc.pdf";
            var filePath = root + fileName;
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(stream, "application/pdf");
            
            ////MemoryStream memoryStream = new MemoryStream();
            //var localFile = System.IO.File.ReadAllBytes(filePath);
            //var byteArray = new ByteArrayContent(localFile);
            //var memoryStream = new MemoryStream(localFile);
            //HttpResponseMessage responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            //responseMessage.Content = byteArray;
            //responseMessage.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            //responseMessage.Content.Headers.ContentDisposition.FileName = fileName;
            //responseMessage.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            ////responseMessage.Content = new StreamContent(new FileStream(filePath, FileMode.Open, FileAccess.Read));
            //return responseMessage;
            ////responseMessage.Content = new StreamContent();

        }
        [HttpGet("api/file")]
        public async Task<IActionResult> DownloadFile()
        {
            try
            {//var path = @"C:\Users\DEVing\Desktop\index.jpg";
                var path = @"C:\Users\DEVing\Desktop\doc.pdf";
                //HttpResponseMessage result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                //var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
                //result.Content = new StreamContent(stream);
                //result.Content.Headers.ContentType =
                //    new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                var ext = Path.GetExtension(path).ToLowerInvariant();
                return File(memory, GetMimeTypes()[ext], Path.GetFileName(path));
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".jpg", "image/jpeg"},
                {".pdf", "application/pdf"}
            };
        }
    }
}
