﻿/*
 Copyright 2014, Mark Taling
 
 Licensed under the Apache License, Version 2.0 (the "License");
 you may not use this file except in compliance with the License.
 You may obtain a copy of the License at
 
 http://www.apache.org/licenses/LICENSE-2.0
 
 Unless required by applicable law or agreed to in writing, software
 distributed under the License is distributed on an "AS IS" BASIS,
 WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 See the License for the specific language governing permissions and
 limitations under the License.
*/

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Http;
using Augurk.Api.Filters;
using Augurk.Api.Managers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;

namespace Augurk.Api
{
    /// <summary>
    /// OWIN startup class that configures the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Called when the supplied <paramref name="app"/> is to be configured.
        /// </summary>
        /// <param name="app">An <see cref="IAppBuilder"/> instance that represents the application.</param>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Configuration is used by infrastructure so must be done this way.")]
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Called by infrastructure so must be done this way.")]
        public void Configuration(IAppBuilder app)
        {
            // Build the configuration for Web API
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            // Ensure no browser-side caching is used
            config.Filters.Add(new NoCacheHeaderFilter());

            // Make JSON the default format
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
            // Use camel-casing for property names
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // Serialize enums using their string value
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            // Be flexible with additional data, for backwards compatability purposes
            config.Formatters.JsonFormatter.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;

            // Share the formatter settings with the manager
            FeatureManager.JsonSerializerSettings = config.Formatters.JsonFormatter.SerializerSettings;

            // Make sure that Web API is enabled for our application
            app.UseWebApi(config);
        }
    }
}