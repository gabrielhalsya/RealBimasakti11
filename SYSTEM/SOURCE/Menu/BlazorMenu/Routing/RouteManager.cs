﻿using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace BlazorMenu.Routing
{
    public class RouteManager
    {
        public Route[] Routes { get; private set; }

        public void Initialize()
        {
            var pageComponentTypes = Assembly.GetExecutingAssembly()
                                             .ExportedTypes
                                             .Where(t => t.IsSubclassOf(typeof(ComponentBase))
                                                         && t.Namespace.Contains(".Pages"));

            var routesList = new List<Route>();
            foreach (var pageType in pageComponentTypes)
            {
                var newRoute = new Route
                {
                    UriSegments = pageType.FullName.Substring(pageType.FullName.IndexOf("Pages") + 6).Split('.'),
                    Handler = pageType
                };

                routesList.Add(newRoute);
            }

            Routes = routesList.ToArray();
        }

        public MatchResult Match(string[] segments)
        {
            if (segments.Length == 0)
            {
                var indexRoute = Routes.SingleOrDefault(x => x.Handler.FullName.ToLower().EndsWith("home"));
                return MatchResult.Match(indexRoute);
            }

            foreach (var route in Routes)
            {
                var matchResult = route.Match(segments);

                if (matchResult.IsMatch)
                {
                    return matchResult;
                }
            }

            return MatchResult.NoMatch();
        }
    }
}
