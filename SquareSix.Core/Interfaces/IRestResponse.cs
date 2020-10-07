using System;
using System.Net;
using System.Net.Http.Headers;

namespace SquareSix.Core.Interfaces
{
	public interface IRestResponse
	{
		HttpStatusCode StatusCode { get; set; }
		string Message { get; set; }
		HttpResponseHeaders ResponseHeaders { get; }
	}

	public interface IRestResponse<T> : IRestResponse
	{
		T Data { get; set; }
	}
}
