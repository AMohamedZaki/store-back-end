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
        private StoreContext _db;
        public ProductController(StoreContext db)
        {
            this._db = db;
        }

        // product/get
        [HttpGet]
        public async Task<IEnumerable<Products>> Get()
        {
            try
            {
                return await Task.Run(() =>
                 _db.Products.Include(item => item.ProductCategories)
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

            var product = _db.Products.Find(id);

            if (product == null)
                return BadRequest();

            return await Task.Run(() => new ObjectResult(product));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] supplierdto productdto)
        {
            try
            {
                if (productdto == null)
                    return BadRequest();
                var Product = Mapper.Map(productdto, new Products());
                _db.Products.Add(Product);
                _db.SaveChanges();
                productdto.productCategories = _db.ProductCategories
                .Select(item => Mapper.Map<ProductCategories,ProductCategorydto>(item))
                .FirstOrDefault(cat => cat.id == Product.CategoryId);
                return await Task.Run(() => new ObjectResult(productdto));
            }
            catch(Exception)
            {
                return await Task.Run(() => StatusCode(500));
            }
        }

        // Product/put/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] supplierdto productdto)
        {
            try
            {
                if (productdto == null || id <= 0)
                    return BadRequest();
                productdto.Code = "";
                var prod = _db.Products.Find(id);
                var Product = Mapper.Map<supplierdto, Products>(productdto, prod);
                _db.SaveChanges();
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
                var prod = _db.Products.Find(id);
                _db.Products.Remove(prod);
                _db.SaveChanges();
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
            var productName = _db.Products.FirstOrDefault(item => item.Name == prodName).Name;
            return await Task.Run(() => string.IsNullOrWhiteSpace(productName));
        }
    }
}
