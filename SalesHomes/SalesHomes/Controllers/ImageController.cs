using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SalesHomes.Services;

namespace SalesHomes.Controllers
{
    [RoutePrefix("api/image")]
    public class ImageController : ApiController
    {
        private readonly ImageService _imageService;

        public ImageController(ImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        [Route("add-image")]
        public async Task<IHttpActionResult> AddImage()
        {
            if (!Request.Content.IsMimeMultipartContent())
                return BadRequest("La solicitud debe ser multipart/form-data.");

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            if (provider.Contents.Count < 2)
                return BadRequest("Faltan parámetros.");

            var file = provider.Contents[0];
            var imageData = await file.ReadAsByteArrayAsync();
            var clothingId = int.Parse(await provider.Contents[1].ReadAsStringAsync());

            var result = await _imageService.AddImageAsync(imageData, clothingId);
            if (!result) return BadRequest("Error al agregar la imagen.");

            return Ok("Imagen agregada correctamente.");
        }

        [HttpGet]
        [Route("get-image/{id:int}")]
        public async Task<IHttpActionResult> GetImage(int id)
        {
            var imageBase64 = await _imageService.GetImageByIdAsync(id);
            if (string.IsNullOrEmpty(imageBase64))
                return NotFound();

            return Ok(new { ImageBase64 = imageBase64 });
        }

        [HttpPut]
        [Route("update-image/{id:int}")]
        public async Task<IHttpActionResult> UpdateImage(int id)
        {
            if (!Request.Content.IsMimeMultipartContent())
                return BadRequest("La solicitud debe ser multipart/form-data.");

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            if (provider.Contents.Count == 0)
                return BadRequest("No se recibió ninguna imagen.");

            var file = provider.Contents[0];
            var imageData = await file.ReadAsByteArrayAsync();

            var result = await _imageService.UpdateImageAsync(id, imageData);
            if (!result) return BadRequest("Error al actualizar la imagen.");

            return Ok("Imagen actualizada correctamente.");
        }

        [HttpDelete]
        [Route("delete-image/{id:int}")]
        public async Task<IHttpActionResult> DeleteImage(int id)
        {
            var result = await _imageService.DeleteImageAsync(id);
            if (!result) return NotFound();

            return Ok("Imagen eliminada correctamente.");
        }
    }
}