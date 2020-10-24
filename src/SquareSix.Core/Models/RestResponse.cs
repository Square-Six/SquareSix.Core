using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SquareSix.Core
{
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
