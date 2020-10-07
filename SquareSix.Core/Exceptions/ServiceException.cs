using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace SquareSix.Core.Exceptions
{
	public class ServiceAccessException : Exception
	{
		public Uri Uri { get; private set; }
		public HttpMethod HttpMethod { get; private set; }
		public HttpResponseHeaders Headers { get; private set; }
		public HttpStatusCode StatusCode { get; private set; }
		public HttpContentHeaders ContentHeaders { get; private set; }
		public HttpResponseMessage Response { get; private set; }

		protected ServiceAccessException() : base() { }

		protected ServiceAccessException(string message) : base(message) { }

		public ServiceAccessException(HttpRequestMessage request, HttpResponseMessage response, Exception innerException, string message = null) : base(CreateErrorMessage(response, message), innerException)
		{
			Uri = request?.RequestUri;
			HttpMethod = request?.Method;
			Data["Url"] = Uri?.ToString();

			if (response != null)
			{
				Headers = response.Headers;
				StatusCode = response.StatusCode;
				ContentHeaders = response.Content?.Headers;
				Response = response;
			}
		}

		private static string CreateErrorMessage(HttpResponseMessage response, string message)
		{
			if (!string.IsNullOrEmpty(message))
            {
				return message;
			}

			if (response == null)
            {
				return "request failed";
			}

			return $"request failed {nameof(response.StatusCode)}:({response.StatusCode})";
		}
	}

	public class ServerException : ServiceAccessException
	{
		public string ApiErrorResponseMessage { get; private set; }
		public string Content { get; private set; }

		public ServerException(HttpRequestMessage request, HttpResponseMessage response, HttpRequestException e) : base(request, response, e)
		{
		}
	}

	public class NetworkException : ServiceAccessException
	{
		public NetworkException(HttpRequestMessage request, HttpResponseMessage response, Exception e, string message) : base(request, response, e)
		{ }

		public NetworkException()
		{ }
	}

	public class UnauthorizedExeption : ServiceAccessException
	{
		public UnauthorizedExeption(HttpRequestMessage request, HttpResponseMessage response, HttpRequestException e) : base(request, response, e)
		{ }
	}

	public class DataContractException : ServiceAccessException
	{
		public DataContractException(HttpRequestMessage request, HttpResponseMessage response, JsonException e) : base(request, response, e, $"API Data Contract Mismatch Error: {e.Message}")
		{ }
	}

	public class NotFoundException : ServiceAccessException
	{
		public NotFoundException(HttpRequestMessage request, HttpResponseMessage response, HttpRequestException e) : base(request, response, e, $"Not Found Uri:{request.RequestUri}")
		{ }
	}

	public class RequestTimeoutException : ServiceAccessException
	{
		public RequestTimeoutException(HttpRequestMessage request, HttpResponseMessage response, Exception e) : base(request, response, e, $"Request Timeout")
		{ }
	}
}
