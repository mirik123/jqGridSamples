
using jqGridExtension;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.OData.Batch;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Formatter;


namespace WebApplication2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
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
            builder.ComplexType<GridModelAnnotate>();

            var edmmodel = builder.GetEdmModel();

            config.MapODataServiceRoute(
                    routeName: "odata",
                    routePrefix: "odata",
                    model: edmmodel,
                    defaultHandler: new jqGridODataHandler(GlobalConfiguration.Configuration)
            );
           
            var list = ODataMediaTypeFormatters.Create();
            config.Formatters.InsertRange(0, list);
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            jqGridODataHelper.Register(config);
        }
    }
}
