using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RetailSite.Products.Images.Api.Controllers
{
    [Route("api/productimages")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        [HttpGet("{name}")]
        public async Task<IActionResult> GetProductImage(string name, bool returnFault = false)
        {
            // if returnFault is true, wait 500ms and
            // return an Internal Server Error
            if (returnFault)
            {
                await Task.Delay(500);
                return new StatusCodeResult(500);
            }

            // generate a "product image" (byte array) between 2 and 10MB
            var random = new Random();
            int fakeBytes = random.Next(2097152, 10485760);            
            byte[] fakeImage = new byte[fakeBytes];
            random.NextBytes(fakeImage);

            return Ok(new { Name = name, Content = fakeImage});
        }
    }
}
