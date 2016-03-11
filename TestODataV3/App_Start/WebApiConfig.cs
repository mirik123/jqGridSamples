
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Formatter;

namespace WebApplication2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<ClientModel>("ODClient").EntityType.HasKey(x => x.id);
            builder.ComplexType<ClientStatus>();
            builder.ComplexType<ClientType>();

            var edmmodel = builder.GetEdmModel();

            config.Routes.MapODataRoute(
                    routeName: "odata",
                    routePrefix: "odata",
                    model: edmmodel
            );

            var list = ODataMediaTypeFormatters.Create();
            config.Formatters.InsertRange(0, list);
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            //config.Formatters.XmlFormatter.UseXmlSerializer = true;
        }
    }
}
