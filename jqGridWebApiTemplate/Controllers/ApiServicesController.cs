using jqGridExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace jqGridWebApiTemplate.Controllers
{
    public class ApiServicesController : ApiController
    {
        // GET api/default
        [JQGridQueryable]
        public IQueryable<dynamic> Get()
        {
            var querydata = new[] { new { name = "value1" }, new { name = "value2" } }.AsQueryable();
            var userdata = new { key = "value" };

            Request.Properties["userdata"] = userdata;

            return querydata;
        }

        public GridModel Get_Alternative(GridSettings grid)
        {
            var querydata = new[] { new { name = "value1" }, new { name = "value2" } }.AsQueryable();
            var userdata = new { key = "value" };

            return jqGridHelper.ApplyJqGridFilters(querydata, grid, userdata);
        }

        // GET api/default/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/default
        public void Post([FromBody]string value)
        {
        }

        // PUT api/default/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/default/5
        public void Delete(int id)
        {
        }
    }
}
