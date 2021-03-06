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
    public class CustomerController : Controller
    {
        private StoreContext _db;
        public CustomerController(StoreContext db)
        {
            _db = db;
        }

        // Customer/get
        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {
            try
            {
                return await Task.Run(() =>
                 _db.Customers.ToList());
            }
            catch (Exception ex)
            {
                var x = ex;
                return null;
            }
        }

        // Customer/1
        [HttpGet("{id}")]
        public async Task<ActionResult> getById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var customer = _db.Customers.Find(id);

            if (customer == null)
                return BadRequest();

            return await Task.Run(() => new ObjectResult(customer));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Customerdto customerdto)
        {
            try
            {
                if (customerdto == null)
                    return BadRequest();
                var customer = Mapper.Map(customerdto, new Customer());
                _db.Customers.Add(customer);
                _db.SaveChanges();
                return await Task.Run(() => new ObjectResult(customer));
            }
            catch
            {
                return await Task.Run(() => StatusCode(500));
            }
        }

        // Customer/put/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Customerdto customerdto)
        {
            try
            {
                if (customerdto == null || id <= 0)
                    return BadRequest();
                var customerdb = _db.Customers.Find(id);
                var customer = Mapper.Map<Customerdto, Customer>(customerdto, customerdb);
                _db.SaveChanges();
                return await Task.Run(() => new ObjectResult(customer));
            }
            catch (Exception)
            {
                return await Task.Run(() => StatusCode(500));
            }
        }

        // Customer/Delete/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();
                var customerdb = _db.Customers.Find(id);
                _db.Customers.Remove(customerdb);
                _db.SaveChanges();
                return await Task.Run(() => new ObjectResult(customerdb));
            }
            catch
            {
                return await Task.Run(() => StatusCode(500));
            }
        }
    }
}
