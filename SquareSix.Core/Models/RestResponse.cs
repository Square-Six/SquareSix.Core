using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SquareSix.Core.Models
{
	/// <summary>
	/// Container for data sent back from API
	/// </summary>
	public interface IRestResponse
	{
		/// <summary>
		/// HTTP response status code
		/// </summary>
		HttpStatusCode StatusCode { get; set; }

		/// <summary>
		/// Description of HTTP status returned
		/// </summary>
		string Message { get; set; }

		/// <summary>
		/// Headers returned by server with the response
		/// </summary>
		HttpResponseHeaders ResponseHeaders { get; }
	}

	/// <summary>
	/// Container for data sent back from API including deserialized data
	/// </summary>
	public interface IRestResponse<T> : IRestResponse
	{
		/// <summary>
		/// Deserialized entity data
		/// </summary>
		T Data { get; set; }
	}

	public class RestResponse : IRestResponse
	{
		public HttpStatusCode StatusCode { get; set; }

		public string Message { get; set; }

		public HttpResponseHeaders ResponseHeaders { get; set; }

		public HttpContent Content { get; set; }

		public RestResponse()
		{
		}
	}

	public class RestResponse<T> : RestResponse, IRestResponse<T>
	{
		public T Data { get; set; }

		public RestResponse()
		{
		}
	}
}
