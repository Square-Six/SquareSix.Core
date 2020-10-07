using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SquareSix.Core.Extensions
{
	public static class RequestUtils
	{
		private const char PATH_DELIMITER = '/';
		private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
		{
			ContractResolver = new CamelCasePropertyNamesContractResolver(),
		};

		public static void AddJsonContent(this HttpRequestMessage message, object data)
		{
			if (data == null)
            {
				return;
			}

			var json = JsonConvert.SerializeObject(data, _settings);
			message.Content = new StringContent(json, Encoding.UTF8, "application/json");
		}

		public static UriBuilder AppendQueryParameter(this UriBuilder builder, string key, string value)
		{
			var queryToAppend = $"{key}={value}";

			if (builder.Query != null && builder.Query.Length > 1)
            {
				builder.Query = builder.Query.Substring(1) + "&" + queryToAppend;
			}
			else
            {
				builder.Query = queryToAppend;
			}

			return builder;
		}

		public static UriBuilder AppendUrlSegment(this UriBuilder builder, string path)
		{
			builder.Path = Combine(builder.Path, path);
			return builder;
		}

		public static string Combine(string path, string relative)
		{
			if (relative == null)
            {
				relative = string.Empty;
			}

			if (path == null)
            {
				path = string.Empty;
			}

			if (relative.Length == 0 && path.Length == 0)
            {
				return string.Empty;
			}

			if (relative.Length == 0)
            {
				return path;
			}

			if (path.Length == 0)
            {
				return relative;
			}

			path = path.Replace('\\', PATH_DELIMITER);
			relative = relative.Replace('\\', PATH_DELIMITER);

			return path.TrimEnd(PATH_DELIMITER) + PATH_DELIMITER + relative.TrimStart(PATH_DELIMITER);
		}
	}
}
