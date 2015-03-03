﻿using Microsoft.OData.Core;
using Microsoft.OData.Edm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Filters;
using System.Web.OData;
using System.Web.OData.Formatter;
using System.Web.OData.Formatter.Deserialization;
using System.Web.OData.Formatter.Serialization;

namespace jqGridExtension
{
    public class jqGridODataHelper
    {
        public static void Register(HttpConfiguration config)
        {
            var formatters = ODataMediaTypeFormatters.Create(new CustomODataSerializerProvider(), new DefaultODataDeserializerProvider());
            config.Formatters.InsertRange(0, formatters);

            //config.MessageHandlers.Insert(0, new jqGridODataHandler(config));
        }
    }

    public class jqGridODataHandler : DelegatingHandler
    {
        public jqGridODataHandler(HttpConfiguration httpConfiguration)
        {
            InnerHandler = new HttpControllerDispatcher(httpConfiguration); 
        }
        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith(
                (task) =>
                {
                    var response = task.Result;
                    IEnumerable<string> headers;
                    if (request.Headers.TryGetValues("Prefer", out headers))
                        response.Content.Headers.Add("Preference-Applied", headers);

                    return response;
                }
            );
        }
    }

    public class CustomODataDeserializerProvider : DefaultODataDeserializerProvider
    {
        private readonly CustomODataFeedDeserializer _feedSerializer;

        public CustomODataDeserializerProvider()
        {
            _feedSerializer = new CustomODataFeedDeserializer(this);
        }

        public override ODataEdmTypeDeserializer GetEdmTypeDeserializer(IEdmTypeReference edmType)
        {
            var serializer = base.GetEdmTypeDeserializer(edmType);
            var feedSerializer = serializer as ODataFeedDeserializer;
            if (feedSerializer != null)
            {
                return _feedSerializer;
            }
            return serializer;
        }
    }

    public class CustomODataFeedDeserializer : ODataFeedDeserializer
    {
        public CustomODataFeedDeserializer(ODataDeserializerProvider serializerProvider)
            : base(serializerProvider)
        {
        }

        public override IEnumerable ReadFeed(ODataFeedWithEntries feed, IEdmEntityTypeReference elementType, ODataDeserializerContext readContext)
        {
            IEnumerable feedInstance = base.ReadFeed(feed, elementType, readContext);
            return feedInstance;
        }
    }

    public class CustomODataSerializerProvider : DefaultODataSerializerProvider
    {
        private readonly CustomODataFeedSerializer _feedSerializer;

        public CustomODataSerializerProvider()
        {
            _feedSerializer = new CustomODataFeedSerializer(this);
        }

        public override ODataEdmTypeSerializer GetEdmTypeSerializer(IEdmTypeReference edmType)
        {
            var serializer = base.GetEdmTypeSerializer(edmType);
            var feedSerializer = serializer as ODataFeedSerializer;
            if (feedSerializer != null)
            {
                return _feedSerializer;
            }
            return serializer;
        }
    }

    public class CustomODataFeedSerializer : ODataFeedSerializer
    {
        public CustomODataFeedSerializer(ODataSerializerProvider serializerProvider)
            : base(serializerProvider)
        {
        }

        public override ODataFeed CreateODataFeed(IEnumerable feedInstance, IEdmCollectionTypeReference feedType, ODataSerializerContext writeContext)
        {
            ODataFeed feed = base.CreateODataFeed(feedInstance, feedType, writeContext);

            if (feedInstance != null)
            {
                int skip = 0;
                object oPageSize, oUserData;

                var options = writeContext.Request.RequestUri.ParseQueryString();
                int.TryParse(options["$skip"], out skip); 

                writeContext.Request.Properties.TryGetValue("PageSize", out oPageSize);
                writeContext.Request.Properties.TryGetValue("userdata", out oUserData);

                var iPageSize = oPageSize == null ? 0 : (int)oPageSize;
                var sUserData = oUserData as string ?? "{}";

                var records = (int)feed.Count.GetValueOrDefault(0);
                //var records = writeContext.Request.ODataProperties().TotalCount;
                var total = (int)Math.Ceiling((double)records / iPageSize);
                var page = (int)Math.Ceiling((double)skip / iPageSize) + 1;

                var value = new ODataComplexValue()
                {
                    TypeName = "jqGridExtension.GridModelAnnotate",
                    Properties = new List<ODataProperty>
                    {
                        new ODataProperty() { Name = "page", Value = page },
                        new ODataProperty() { Name = "records", Value = records },
                        new ODataProperty() { Name = "total", Value = total },
                        new ODataProperty() { Name = "userdata", Value = sUserData }
                    }
                };

                feed.InstanceAnnotations.Add(new ODataInstanceAnnotation("jqgrid.GridModelAnnotate", value));
            }

            return feed;
        }
    }

    public class JQGridODataQueryableAttribute : ActionFilterAttribute
    {
        public int PageSize { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            if (PageSize == 0)
            {
                var enableQuery = actionContext.ActionDescriptor.GetCustomAttributes<EnableQueryAttribute>().FirstOrDefault();
                if (enableQuery != null)
                    PageSize = enableQuery.PageSize;
            }

            actionContext.Request.Properties["PageSize"] = PageSize;
        }
    }

    public class GridModelAnnotate
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public string userdata { get; set; }
    }
}