using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Store.data.Model;
using Store.data.dto;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace store_back_end.api
{
    [Route("[controller]/[action]")]
    [EnableCors("AllowAnyOrigin")]
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private StoreContext db;
        public ProductController()
        {
            db = new StoreContext();
        }

        // product/get
        [HttpGet]
        public async Task<IEnumerable<Products>> Get()
        {
            try
            {
                return await Task.Run(() =>
                 db.Products.Include(item => item.ProductCategories)
                 .Select(product => new Products
                 {
                     Name = product.Name,
                     Price = product.Price,
                     Cost = product.Cost,
                     Description = product.Description,
                     CategoryId = product.CategoryId,
                     Code = product.Code,
                     id = product.id,
                     ProductCategories = new ProductCategories
                     {
                         id = product.ProductCategories.id,
                         Name = product.ProductCategories.Name
                     }
                 })
                .ToList());
            }
            catch (Exception ex)
            {
                var x = ex;
                return null;
            }
        }

        // Product/1
        [HttpGet("{id}")]
        public async Task<ActionResult> getById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var product = db.Products.Find(id);

            if (product == null)
                return BadRequest();

            return await Task.Run(() => new ObjectResult(product));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Productdto productdto)
        {
            try
            {
                if (productdto == null)
                    return BadRequest();
                var Product = Mapper.Map(productdto, new Products());
                db.Products.Add(Product);
                db.SaveChanges();
                Product.ProductCategories = db.ProductCategories.FirstOrDefault(cat => cat.id == Product.CategoryId);
                return await Task.Run(() => new ObjectResult(Product));
            }
            catch(Exception)
            {
                return await Task.Run(() => StatusCode(500));
            }
        }

        // Product/put/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Productdto productdto)
        {
            try
            {
                if (productdto == null || id <= 0)
                    return BadRequest();
                productdto.Code = "";
                var prod = db.Products.Find(id);
                var Product = Mapper.Map<Productdto, Products>(productdto, prod);
                db.SaveChanges();
                return await Task.Run(() => new ObjectResult(Product));
            }
            catch (Exception)
            {
                return await Task.Run(() => StatusCode(500));
            }
        }

        // Product/Delete/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();
                var prod = db.Products.Find(id);
                db.Products.Remove(prod);
                db.SaveChanges();
                return await Task.Run(() => new ObjectResult(prod));
            }
            catch
            {
                return await Task.Run(() => StatusCode(500));
            }
        }

        // For Asynchronous Validations
        [HttpPost(Name = "chackProductName")]
        public async Task<bool> chackProductName(string prodName)
        {
            var productName = db.Products.FirstOrDefault(item => item.Name == prodName).Name;
            return await Task.Run(() => string.IsNullOrWhiteSpace(productName));
        }
    }
}
