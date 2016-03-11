using jqGridExtension;
using System.Collections;
using System.Linq;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Extensions;

namespace WebApplication2.odata
{
    public class ODClientController : ODataController
    {
        DevEntities dbcontext = EFRepository.Create();

        [JQGridODataQueryable]    
        [HttpGet]       
		[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, PageSize = 25)]
        public IQueryable<ClientModel> Get() 
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

            Request.Properties["userdata"] = new { key = "value" };

            return model;
        }

        [HttpGet]
        public ClientModel Get([FromODataUri] int key)
        {
            return Get().FirstOrDefault(x => x.id == key);
        }

        [HttpPost]
        public IHttpActionResult Post(Client model)
        {
            return Ok();
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
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            return Ok();
        }
    }
}
