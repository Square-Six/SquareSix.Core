using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace SquareSix.Core
{
    public static class SquareSixCore
    {
        public static void Init()
        {
            // Register for alert service
            SimpleIOC.Container.Register<IAlertService>(new AlertService());

            // Register for Rest Services
            SimpleIOC.Container.Register<IRestService>(new RestService());

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
