using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SquareSix.Core.Interfaces;

namespace SquareSix.Core
{
    public static class SquareSixService
    {
        public static void Init()
        {
            // Register for alert service
            SimpleIOC.Container.Register<ISquaredAlertService>(new AlertService());

            // Register for Rest Services
            SimpleIOC.Container.Register<ISquaredRestService>(new SquaredRestService());

            // Configure Json serialization
            ConfigureJson();
        }

        private static void ConfigureJson()
        {
            JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
            {
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = new List<JsonConverter> { new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() } },
                };
                return settings;
            });
        }
    }
}
