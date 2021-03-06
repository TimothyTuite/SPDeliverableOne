﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SeniorProjectPreReq
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                 name: "API",
                 url: "API/{action}/{id}/{year}",
                 defaults: new { controller = "API", action = "Index", id = UrlParameter.Optional , year = UrlParameter.Optional }
             );
            routes.MapRoute(
                  name: "DeleteAccount",
                  url: "Account/{action}/{email}/",
                  defaults: new { controller = "Account", action = "Delete", email = ""}
             );
            routes.MapRoute(
                  name: "EditMetricForYear",
                  url: "principal/{action}/{year}/{MetricID}",
                  defaults: new { controller = "Principal", action = "EditMetricForYear", year = "", MetricID="" }
             );
            routes.MapRoute(
                 name: "EditYear",
                 url: "principal/{action}/{year}",
                 defaults: new { controller = "Principal", action = "AddPrograms", year = "" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
           


        }
    }
}
