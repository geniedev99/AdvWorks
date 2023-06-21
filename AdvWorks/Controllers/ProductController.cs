using AdvWorks.Models;
using AdvWorks.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvWorks.Api.Controllers
{
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetProducts")]
        public IActionResult GetProducts()
        {
            _logger.LogInformation("GetProducts started");
            try
            {
                _logger.LogInformation("GetProducts started");
                List<Product> product = _productRepository.GetProducts();
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex);
                throw;
            }

        }
        [HttpGet]
        [Route("GetProductDetails")]
        public IActionResult GetProductDetails([FromQuery] int ProductID)
        {
            try
            {
                ProductDetails productDetails = _productRepository.GetProductDetails(ProductID);

                return Ok(productDetails);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.StackTrace);
            }
        }





        [HttpPost]
        [Route("CreateProduct")]
        public IActionResult Create([FromBody] Product product)
        {
            try
            {
                _logger.LogInformation("create product started");
                bool result = _productRepository.CreateProduct(product);
                if (result)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return BadRequest(ex);
                throw;
            }


        }


    }
}