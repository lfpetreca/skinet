using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public string GetProducts()
        {
            return "this will be implemented";
        }

        [HttpGet("{id}")]
        public string GetProduct(int id)
        {
            return "BY 3 ID this will be implemented by id";
        }
    }
}