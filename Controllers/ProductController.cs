using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Store.Model;
using Store.dto;
using AutoMapper;

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
        [HttpGet(Name = "get")]
        public async Task<IEnumerable<Products>> Get()
        {
            var data = db.Products.ToList();
            return await Task.Run(() => data);
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
                return await Task.Run(() => new ObjectResult(Product));
            }
            catch
            {
                return await Task.Run(() => StatusCode(500));
            }
        }

        // Product/put/1
        [HttpPut("{id}",Name = "Put")]
        public async Task<ActionResult> Put(int id, [FromBody] Productdto productdto)
        {
            try
            {
                if (productdto == null || id <= 0)
                    return BadRequest();

                var prod = db.Products.Find(id);                    
                var Product = Mapper.Map(productdto, prod);
                db.SaveChanges();
                return await Task.Run(() => new ObjectResult(Product));
            }
            catch
            {
                return await Task.Run(() => StatusCode(500));
            }
        }

        // Product/Delete/1
        [HttpDelete("{id}",Name = "Delete")]
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
    }
}
