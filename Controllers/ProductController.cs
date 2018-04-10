using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using Store.Model;

namespace store_back_end.api
{
    [Route("[controller]/[action]/")]
    [EnableCors("AllowAnyOrigin")]
    [AllowAnonymous]
    public class ProductController : Controller
    {
        private StoreContext db;
        public ProductController()
        {
            db = new StoreContext();
        }

        [HttpGet]
        public async Task<IEnumerable<Products>> Get()
        {
            var data = db.Products.ToList();
            return await Task.Run(() => data);
        }
    }
}
