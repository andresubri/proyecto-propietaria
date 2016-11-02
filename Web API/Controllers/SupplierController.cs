using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CuentasPorPagar.Views.CRUD;
using Parse;

namespace Web_API.Controllers
{
    [RoutePrefix("api/supplier")]

    public class SupplierController : ApiController
    {
        private Supplier supplier;

        // GET: api/Supplier
        public async Task<IEnumerable<CuentasPorPagar.Models.Supplier>> Get()
        {
            return await new ParseQuery<CuentasPorPagar.Models.Supplier>().FindAsync();
        }

        // GET: api/Supplier/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Supplier
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Supplier/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Supplier/5
        public void Delete(int id)
        {
        }
    }
}
