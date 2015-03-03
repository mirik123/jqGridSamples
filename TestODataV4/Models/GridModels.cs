using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebApplication2
{
    public class ClientModel
    {
        public int id { get; set; }
        public ClientStatus status { get; set; }
        public ClientType cltype { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string phone_cell { get; set; }
        public string ssn { get; set; }
        public string addr_city { get; set; }
        public string addr_street { get; set; }
        public string addr_home { get; set; }
        public bool request_exists { get; set; }
        public bool check_exists { get; set; }
    }

    public static class EFRepository
    {
        public static DevEntities Create()
        {
            var context = new DevEntities();
            context.Database.CommandTimeout = 10000;
            context.Configuration.LazyLoadingEnabled = false;
            context.Configuration.ProxyCreationEnabled = false;

            context.Database.Log = (str) =>
            {
                if (!string.IsNullOrWhiteSpace(str) && (str.StartsWith("select", StringComparison.CurrentCultureIgnoreCase) || str.StartsWith("-- Completed")))
                {
                    var traceframe = new System.Diagnostics.StackTrace(true).GetFrames().Skip(1).FirstOrDefault(x => !string.IsNullOrEmpty(x.GetFileName()));
                    var tracedata = traceframe != null ? traceframe.ToString().Replace("C:\\...\\WebApplication2\\", "") : "";

                    Debug.WriteLine("DatabaseLog: " + tracedata + ", str: " + str, "Database.Log");
                }
            };

            return context;
        }
    }
}