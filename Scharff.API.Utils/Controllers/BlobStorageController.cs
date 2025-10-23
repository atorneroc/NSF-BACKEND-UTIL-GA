using MediatR;
using Microsoft.AspNetCore.Mvc; 
using Scharff.API.Utils.Utils.Models;
using Scharff.Application.Commands.AzureBlobStorage.DeleteFile;
using Scharff.Application.Commands.AzureBlobStorage.UploadFile;
using Scharff.Application.Queries.AzureBlobStorage.DownloadFile;
using Scharff.Application.Queries.AzureBlobStorage.GetAllFiles;
using Scharff.Domain.Entities;
using Scharff.Domain.Response.BillingCourt;
using Scharff.Domain.Response.BlobStorage;
using System.Text;
using System.Xml;

namespace Scharff.API.Utils.Controllers
{
    [ApiController]
    [Route("api/")]
    public class BlobStorageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlobStorageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(template: "BlobStorage")]
        public async Task<IActionResult> GetAllFiles()
        {
            GetAllFilesQuery request = new();
            var result = await _mediator.Send(request);

            return Ok(new CustomResponse<List<BlobStorageModel>>($"Se encontraron {result.Count} files.", result));
        }

        [HttpGet(template: "BlobStorage/{filename}")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            DownloadFileQuery request = new() { BlobFileName = filename };
            var result = await _mediator.Send(request);

            if (result != null && result.Content != null && result.ContentType != null && result.Name != null)
            {
                return File(result.Content, result.ContentType, result.Name);
            }
            else
            {
                return BadRequest("No se pudo encontrar el archivo solicitado.");
            }
        }

        [HttpPost(template: "BlobStorage")]
        public async Task<IActionResult> UploadFile([FromBody] UploadFileCommand request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpDelete(template: "BlobStorage/filename")]
        public async Task<IActionResult> DeleteFile(string filename)
        {
            DeleteFileCommand request = new() { BlobFileName = filename };
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpGet(template: "BlobStorage/v2/{containername}")]
        public async Task<IActionResult> GetAllFilesV2(string containername, string? foldername)
        {
            GetAllFilesV2Query request = new() { BlobContainerName = containername, BlobFolderName = foldername };
            var result = await _mediator.Send(request);

            return Ok(new CustomResponse<List<BlobStorageModel>>($"Se encontraron {result.Count} files.", result));
        }

        [HttpGet(template: "BlobStorage/v2/{containername}/file/{filename}")]
        public async Task<IActionResult> DownloadFileV2(string containername, string? foldername, string filename)
        {
            DownloadFileV2Query request = new() { BlobContainerName = containername, BlobFolderName = foldername, BlobFileName = filename };
            var result = await _mediator.Send(request);

            if (result != null && result.Content != null && result.ContentType != null && result.Name != null)
            {
                return File(result.Content, result.ContentType, result.Name);
            }
            else
            {
                return BadRequest("No se pudo encontrar el archivo solicitado.");
            }
        }
        [HttpGet(template: "BlobStorage/v2/content/{containername}/file/{filename}")]
        public async Task<IActionResult> ContentFileV2(string containername, string? foldername, string filename)
        {
            DownloadFileV2Query request = new() { BlobContainerName = containername, BlobFolderName = foldername, BlobFileName = filename };
            var result = await _mediator.Send(request);
            
            if (result != null && result.Content != null && result.ContentType != null && result.Name != null)
            {
                // Leer el contenido del archivo como una cadena de texto
                string fileContent;
                using (StreamReader reader = new StreamReader(result.Content))
                {
                    fileContent = await reader.ReadToEndAsync();
                }

                // Devolver el contenido del archivo como respuesta
                return Content(fileContent, "text/plain");
            }
            else
            {
                return BadRequest("No se pudo encontrar el archivo solicitado.");
            }
        }
        [HttpPost(template: "BlobStorage/V2")]
        public async Task<IActionResult> UploadFileV2([FromBody] UploadFileV2Command request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet(template: "BlobStorage/v2/base64/{containername}/file/{filename}")]
        public async Task<IActionResult> Base64FileV2(string containername, string? foldername, string filename)
        {
            DownloadFileV2Query request = new() { BlobContainerName = containername, BlobFolderName = foldername, BlobFileName = filename };
            var result = await _mediator.Send(request);

            if (result != null && result.Content != null && result.ContentType != null && result.Name != null)
            {
                // Leer el contenido del archivo como un arreglo de bytes
                byte[] fileBytes;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await result.Content.CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                string base64String = Convert.ToBase64String(fileBytes);
                string base64Image = $"data:{result.ContentType};base64,{base64String}";
                // Eliminar el prefijo "data:application/octet-stream;base64,"
                string base64WithoutPrefix = base64Image.Replace("data:application/octet-stream;base64,", "");
                var responseGetBase64 = new ResponseGetBase64
                {
                    Base64Image = base64Image
                };
                return Ok(new CustomResponse<ResponseGetBase64>($"Se encontro con el Id {filename} su base64.", responseGetBase64));

            }
            else
            {
                // Si no se encuentra el archivo, devolver un error
                return BadRequest("No se pudo encontrar el archivo solicitado.");
            }
        }

    }
}
