using MicroservicesDocker.Model;
using MicroservicesDocker.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroservicesDocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ProductController(IProductRepository productRepository, IHostingEnvironment hostingEnvironment)
        {
            _productRepository = productRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Product> objProduct = new List<Product>();

            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            var filePath = Content(webRootPath + contentRootPath);
            // XDocument doc = XDocument.Load("D:\\Jaydeep\\Projects\\MicroservicesDocker\\Products.xml");
            XDocument doc = XDocument.Load(filePath.Content + "/Products.xml");
            foreach (XElement element in doc.Descendants("Products")
                .Descendants("prod"))
            {
                Product prod = new Product();
                prod.Id = Convert.ToInt32(element.Element("Id").Value);
                prod.Name = element.Element("Name").Value;
                prod.Description = element.Element("Description").Value;
                prod.Price = Convert.ToInt32(element.Element("Price").Value);
                prod.CategoryId = Convert.ToInt32(element.Element("CategoryId").Value);
                objProduct.Add(prod);
            }
            

            return new OkObjectResult(objProduct);
        }

        //[HttpGet("{id}", Name = "Get")]
        //public IActionResult Get(int id)
        //{
        //    var product = _productRepository.GetProductById(id);
        //    return new OkObjectResult(product);
        //}

        //[HttpPost]
        //public IActionResult Post([FromBody] Product product)
        //{
        //    using (var scope = new TransactionScope())
        //    {
        //        _productRepository.InsertProduct(product);
        //        scope.Complete();
        //        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        //    }
        //}

        //[HttpPut]
        //public IActionResult Put([FromBody] Product product)
        //{
        //    if (product != null)
        //    {
        //        using (var scope = new TransactionScope())
        //        {
        //            _productRepository.UpdateProduct(product);
        //            scope.Complete();
        //            return new OkResult();
        //        }
        //    }
        //    return new NoContentResult();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    _productRepository.DeleteProduct(id);
        //    return new OkResult();
        //}
    }
}
