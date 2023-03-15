using Microsoft.AspNetCore.Mvc;
using RabbitMqProdutorAPI.Model;
using RabbitMqProdutorAPI.RabbitMq;
using RabbitMqProdutorAPI.Service;
using System.Threading;

namespace RabbitMqProdutorAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductrController : ControllerBase
    {
        private readonly ILogger<ProductrController> _logger;
        private readonly IProductService _productService;
        private readonly IRabitMQProducer _rabitMQProducer;

        public ProductrController(ILogger<ProductrController> logger, IProductService productService, IRabitMQProducer rabitMQProducer)
        {
            _logger = logger;
            _productService = productService;
            _rabitMQProducer = rabitMQProducer;
        }

        [HttpPost("addproduct")]
        public Product AddProduct(Product product, CancellationToken cancellationToke)
        {

           _rabitMQProducer.SendProductMessage(product);

           return product;
        }


    }
}