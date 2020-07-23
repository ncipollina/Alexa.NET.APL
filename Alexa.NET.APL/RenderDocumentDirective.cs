﻿using System;
using System.Collections.Generic;
using System.Text;
using Alexa.NET.APL;
using Alexa.NET.Response.APL;
using Alexa.NET.Response.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Alexa.NET.Response
{
    public class RenderDocumentDirective : IDirective
    {
        public const string APLDirectiveType = "Alexa.Presentation.APL.RenderDocument";
        public const string APLTDirectiveType = "Alexa.Presentation.APLT.RenderDocument";
        private static readonly object directiveadd = new object();

        public static void AddSupport()
        {
            lock (directiveadd)
            {
                if (!DirectiveConverter.TypeFactories.ContainsKey(APLDirectiveType))
                {
                    DirectiveConverter.TypeFactories.Add(APLDirectiveType, () => new RenderDocumentDirective());
                }

                if (!DirectiveConverter.TypeFactories.ContainsKey(APLTDirectiveType))
                {
                    DirectiveConverter.TypeFactories.Add(APLTDirectiveType, () => new RenderDocumentDirective());
                }
            }
        }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type => Document is APLTDocument ? APLTDirectiveType : APLDirectiveType;

        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }

        [JsonProperty("targetProfile", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(StringEnumConverter))]
        public APLTProfile? TargetProfile { get; set; }

        [JsonProperty("document")]
        public APLDocumentReference Document { get; set; }

        [JsonProperty("datasources", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, APLDataSource> DataSources { get; set; }
    }
}
