
using jqGridExtension;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace WebApplication2.api
{
    public class ApiServicesController : ApiController
    {
        DevEntities dbcontext = EFRepository.Create();
        
        [HttpGet]
        public HttpResponseMessage GetSelectData(string table, bool empty = false, int status = 0)
        {
            IEnumerable<string> model = null;
            string format = "<option value=\"{0}\" data-bkcolor=\"{2}\" style=\"background-color:{2}\">{1}</option>";

            switch(table)
            {
                case "ClientStatus":
                    model = dbcontext.ClientStatus.ToList().Select(x => string.Format(format, x.Id, x.descr, x.color));
                    break;
                case "ClientType":
                    model = dbcontext.ClientType.ToList().Select(x => string.Format(format, x.Id, x.descr, "transparent"));
                    break;
            }

            if (empty)
                model = new string[] { string.Format(format, "-1", "", "transparent") }.Union(model);

            string content = "<select>" + model.Aggregate("", (x, y) => x + y) + "</select>";

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(content, Encoding.UTF8, "text/html")
            };
        }

        [HttpGet]
        public GridModel Get1(GridSettings grid)
        {
            var model = dbcontext.Client
                    .Join(dbcontext.ClientStatus, a => a.status, b => b.Id, (a, b) => new { client = a, status = b })
                    .Join(dbcontext.ClientType, a => a.client.cltype, b => b.Id, (a, b) => new { client = a.client, status = a.status, type = b })
                    .Select(x => new ClientModel()
                    {
                        id = x.client.Id,
                        status = x.status,
                        cltype = x.type,
                        firstname = x.client.firstname,
                        lastname = x.client.lastname,
                        phone_cell = x.client.phone_cell,
                        ssn = x.client.ssn,
                        addr_city = x.client.addr_city,
                        addr_street = x.client.addr_street,
                        addr_home = x.client.addr_home
                    });

            return jqGridHelper.ApplyJqGridFilters(model, grid, new { });
        }

        [HttpGet]
        [JQGridQueryable]
        public IQueryable<ClientModel> Get2()
        {
            var model = dbcontext.Client
                    .Join(dbcontext.ClientStatus, a => a.status, b => b.Id, (a, b) => new { client = a, status = b })
                    .Join(dbcontext.ClientType, a => a.client.cltype, b => b.Id, (a, b) => new { client = a.client, status = a.status, type = b })
                    .Select(x => new ClientModel()
                    {
                        id = x.client.Id,
                        status = x.status,
                        cltype = x.type,
                        firstname = x.client.firstname,
                        lastname = x.client.lastname,
                        phone_cell = x.client.phone_cell,
                        ssn = x.client.ssn,
                        addr_city = x.client.addr_city,
                        addr_street = x.client.addr_street,
                        addr_home = x.client.addr_home
                    });

            Request.Properties["userdata"] = new { };

            return model;
        }
    }
}
