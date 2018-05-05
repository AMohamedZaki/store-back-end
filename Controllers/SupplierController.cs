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
    public class SupplierController : Controller
    {
        private StoreContext _db;
        public SupplierController(StoreContext db)
        {
            this._db = db;
        }

        // product/get
        [HttpGet]
        public async Task<IEnumerable<Suppliers>> Get()
        {
            try
            {
                return await Task.Run(() => _db.Suppliers.ToList());
            }
            catch (Exception ex)
            {
                var x = ex;
                return null;
            }
        }

        // Suppliers/1
        [HttpGet("{id}")]
        public async Task<ActionResult> getById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var suppliers = _db.Suppliers.Find(id);

            if (suppliers == null)
                return BadRequest();

            return await Task.Run(() => new ObjectResult(suppliers));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Suppliersdto supplierdto)
        {
            try
            {
                if (supplierdto == null)
                    return BadRequest();
                var Supplier = Mapper.Map(supplierdto, new Suppliers());
                _db.Suppliers.Add(Supplier);
                _db.SaveChanges();
                return await Task.Run(() => new ObjectResult(supplierdto));
            }
            catch(Exception)
            {
                return await Task.Run(() => StatusCode(500));
            }
        }

        // Product/put/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Suppliersdto supplierDto)
        {
            try
            {
                if (supplierDto == null || id <= 0)
                    return BadRequest();
                var sup = _db.Suppliers.Find(id);
                var Supplier = Mapper.Map(supplierDto, sup);
                _db.SaveChanges();
                return await Task.Run(() => new ObjectResult(Supplier));
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
                var suppp = _db.Suppliers.Find(id);
                _db.Suppliers.Remove(suppp);
                _db.SaveChanges();
                return await Task.Run(() => new ObjectResult(suppp));
            }
            catch
            {
                return await Task.Run(() => StatusCode(500));
            }
        }
    }
}
