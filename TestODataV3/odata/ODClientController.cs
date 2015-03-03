using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;

namespace WebApplication2.odata
{
    public class ODClientController : ODataController
    {
        DevEntities dbcontext = EFRepository.Create();

        [HttpGet]
        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All, PageSize = 25)]
        public IQueryable<ClientModel> Get() 
        {
            //dbcontext.Seed(dbcontext);
            
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

            return model;
        }

        [HttpGet]
        [Queryable(AllowedQueryOptions = AllowedQueryOptions.All, PageSize = 25)]
        public PageResult<ClientModel> Get_alternative(ODataQueryOptions<ClientModel> options)
        {
            var settings = new ODataQuerySettings() { PageSize = 25 };
            var settings2 = new ODataValidationSettings() { AllowedFunctions = AllowedFunctions.AllMathFunctions, AllowedQueryOptions = AllowedQueryOptions.All };

            IQueryable model = dbcontext.Client
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

            options.Validate(settings2);
            if (options.Filter != null)
                model = options.ApplyTo(model, settings);

            //if (options.Filter != null)
            //    model = options.Filter.ApplyTo(model, settings);
            //if (options.OrderBy != null) 
            //    model = options.OrderBy.ApplyTo(model, settings);
            //if (options.Skip != null)
            //    model = options.Skip.ApplyTo(model, settings);
            //if (options.Top != null)
            //    model = options.Top.ApplyTo(model, settings);

            //if (options.RawValues.InlineCount == "allpages")
            //    count = results.Count();

            return new PageResult<ClientModel>(model as IEnumerable<ClientModel>, Request.GetNextPageLink(), Request.GetInlineCount());
        }

        [HttpGet]
        public ClientModel Get([FromODataUri] int key)
        {
            return Get().FirstOrDefault(x => x.id == key);
        }

        [HttpPost]
        public HttpResponseMessage Post(Client model)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [AcceptVerbs("PATCH", "MERGE")]
        public Client Patch([FromODataUri] int key, Delta<Client> model)
        {
            var itm = dbcontext.Client.FirstOrDefault(x => x.Id == key);
            model.Patch(itm);

            return itm;
        }

        [HttpPut]
        public Client Put([FromODataUri]int key, Delta<Client> model)
        {
            var itm = dbcontext.Client.FirstOrDefault(x => x.Id == key);
            model.Put(itm);
            
            return itm;
        }

        [HttpDelete]
        public HttpResponseMessage Delete([FromODataUri] int key)
        {
           return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
