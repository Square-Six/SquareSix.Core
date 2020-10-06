using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SquareSix.Core.Exceptions;
using SquareSix.Core.Models;

namespace SquareSix.Core.Extensions
{
    public static class HttpExtensions
    {
        public static Task<RestResponse<T>> RequestAsync<T>(this HttpClient client, HttpRequestMessage message)
        {
            return client.RequestAsync<T>(message, CancellationToken.None);
        }

        public static Task<RestResponse> RequestAsync(this HttpClient client, HttpRequestMessage message)
        {
            return client.RequestAsync(message, CancellationToken.None);
        }

        public static Task<RestResponse<T>> RequestAsync<T>(this HttpClient client, HttpRequestMessage message, string xmlRootElementName)
        {
            return client.RequestAsync<T>(message, CancellationToken.None, xmlRootElementName);
        }

        public static Task<RestResponse<T>> PostFormDataAsync<T>(this HttpClient client, string requestUri, MultipartFormDataContent dataContent, string xmlRootElementName)
        {
            return client.PostFormDataAsync<T>(requestUri, dataContent, xmlRootElementName, CancellationToken.None);
        }

        public async static Task<RestResponse<T>> RequestAsync<T>(this HttpClient client, HttpRequestMessage message, CancellationToken token, string xmlRootElementName)
        {
            HttpResponseMessage response = null;
            string content = null;
            try
            {
                PrintToConsole(message);
                using (response = await client.SendAsync(message, token).ConfigureAwait(false))
                {
                    if (response.Content != null)
                    {
                        content = await response.Content.ReadAsStringAsync();
                    }
                    PrintToConsole(response, content);
                    EnsurePetcoSuccessStatusCode(response);

                    var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var resp = new RestResponse<T>
                    {
                        StatusCode = response.StatusCode,
                        Message = response.ReasonPhrase,
                        ResponseHeaders = response.Headers,
                        Content = response.Content,
                        Data = await Task.Run(() => ParseXmlContent<T>(responseString, xmlRootElementName))
                    };
                    return resp;
                }
            }
            catch (Exception e)
            {
                throw HandleRestException(message, response, e, token, content);
            }
        }

        public async static Task<RestResponse<T>> RequestAsync<T>(this HttpClient client, HttpRequestMessage message, CancellationToken token)
        {
            HttpResponseMessage response = null;
            string content = null;
            try
            {
                PrintToConsole(message);
                using (response = await client.SendAsync(message, token).ConfigureAwait(false))
                {

                    if (response.Content != null)
                    {
                        content = await response.Content.ReadAsStringAsync();
                    }
                    PrintToConsole(response, content);
                    EnsurePetcoSuccessStatusCode(response);

                    var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var resp = new RestResponse<T>
                    {
                        StatusCode = response.StatusCode,
                        Message = response.ReasonPhrase,
                        ResponseHeaders = response.Headers,
                        Content = response.Content,
                        Data = await Task.Run(() => JsonConvert.DeserializeObject<T>(responseString))
                    };
                    return resp;
                }
            }
            catch (Exception e)
            {
                throw HandleRestException(message, response, e, token, content);
            }
        }

        public async static Task<RestResponse> RequestAsync(this HttpClient client, HttpRequestMessage message, CancellationToken token)
        {
            HttpResponseMessage response = null;
            string content = null;
            try
            {
                PrintToConsole(message);
                using (response = await client.SendAsync(message, token).ConfigureAwait(false))
                {

                    if (response.Content != null)
                    {
                        content = await response.Content.ReadAsStringAsync();
                    }
                    PrintToConsole(response, content);

                    EnsurePetcoSuccessStatusCode(response);

                    var resp = new RestResponse
                    {
                        StatusCode = response.StatusCode,
                        Message = response.ReasonPhrase,
                        ResponseHeaders = response.Headers,
                        Content = response.Content,
                    };

                    return resp;
                }
            }
            catch (Exception e)
            {
                throw HandleRestException(message, response, e, token, content);
            }
        }

        public static async Task<RestResponse<T>> PostFormDataAsync<T>(this HttpClient client, string requestUri, MultipartFormDataContent dataContent, string xmlRootElementName, CancellationToken token)
        {
            HttpResponseMessage response = null;
            string content = null;
            try
            {
                using (response = await client.PostAsync(requestUri, dataContent).ConfigureAwait(false))
                {

                    if (response.Content != null)
                    {
                        content = await response.Content.ReadAsStringAsync();
                    }
                    PrintToConsole(response, content);
                    EnsurePetcoSuccessStatusCode(response);

                    var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    var resp = new RestResponse<T>
                    {
                        StatusCode = response.StatusCode,
                        Message = response.ReasonPhrase,
                        ResponseHeaders = response.Headers,
                        Content = response.Content,
                        Data = await Task.Run(() => ParseXmlContent<T>(responseString, xmlRootElementName))
                    };
                    return resp;
                }
            }
            catch (Exception e)
            {
                throw HandleRestException(new HttpRequestMessage { RequestUri = new Uri(requestUri), Content = dataContent, Method = HttpMethod.Post }, response, e, token, content);
            }
        }

        private static T ParseXmlContent<T>(string response, string rootElement)
        {
            var xRoot = new XmlRootAttribute
            {
                ElementName = rootElement,
                IsNullable = true
            };

            var serializer = new XmlSerializer(typeof(T), xRoot);
            var xDocument = XDocument.Parse(response);
            using (var reader = xDocument.Root.CreateReader())
            {
                return (T)serializer.Deserialize(reader);
            }
        }

        private static void EnsurePetcoSuccessStatusCode(HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.NotModified) return;
            response.EnsureSuccessStatusCode();
        }

        private static Exception HandleRestException(HttpRequestMessage request, HttpResponseMessage response, Exception e, CancellationToken token, string content)
        {
            if (TryWrapIfNetworkFailure(request, response, e, token, out NetworkException networkException))
                return networkException;

            //var nativeException = Mvx.Resolve<IHttpClientHandlerProvider>().UnwrapNativeException(request, response, e);

            //if (nativeException != null)
            //return nativeException;

            if (e is JsonException)
                return new DataContractException(request, response, e as JsonException);

            if (e is HttpRequestException)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Gone:
                    case HttpStatusCode.Unauthorized:
                        return new UnauthorizedExeption(request, response, e as HttpRequestException);
                    case HttpStatusCode.NotFound:
                        return new NotFoundException(request, response, e as HttpRequestException);
                    default:
                        return new ServerException(request, response, e as HttpRequestException);
                }
            }

            if (e is OperationCanceledException)
                return new RequestTimeoutException(request, response, e);

            return new ServiceAccessException(request, response, e);
        }

        private static bool TryWrapIfNetworkFailure(HttpRequestMessage request, HttpResponseMessage response, Exception e, CancellationToken token, out NetworkException networkException)
        {
            networkException = null;

            if (token.IsCancellationRequested)
                return false;

            var webException = e as WebException ?? e.InnerException as WebException;

            if (webException != null)
            {
                networkException = new NetworkException(request, response, webException, webException.Status.ToString() ?? e.Message);
                return true;
            }

            return false;
        }

        public static void PrintToConsole(HttpRequestMessage request)
        {
#if DEBUG
            var sb = new StringBuilder();
            sb.AppendLine("[SAL]: Request Start >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

            sb.AppendLine("## REQUEST");
            sb.AppendLine();
            sb.AppendLine($"URI: {request.RequestUri}");
            sb.AppendLine($"METHOD: {request.Method}");
            if (request.Content != null)
            {
                try
                {
                    var formattedString = JToken.Parse(request.Content.ReadAsStringAsync().Result).ToString(Formatting.Indented);
                    sb.AppendLine($"CONTENT: {formattedString}");
                }
                catch (Exception) { }
                sb.AppendLine($"CONTENT: {request.Content.ReadAsStringAsync().Result}");
            }
            sb.AppendLine($"HEADERS: {request.Headers}");
            System.Diagnostics.Debug.WriteLine(sb.ToString());
#endif
        }

        public static void PrintToConsole(HttpResponseMessage response, string content)
        {
#if DEBUG
            var sb = new StringBuilder();
            sb.AppendLine("## RESPONSE:");
            sb.AppendLine($"URL: {response.RequestMessage.RequestUri}");
            sb.AppendLine();
            sb.AppendLine($"StatusCode: {response.StatusCode}");

            if (!string.IsNullOrEmpty(content))
            {
                try
                {
                    var formattedString = JToken.Parse(content).ToString(Formatting.Indented);
                    sb.AppendLine($"CONTENT: {formattedString}");
                }
                catch (Exception)
                {
                    sb.AppendLine($"CONTENT: {content}");
                }
            }
            sb.AppendLine($"HEADERS: {response.Headers}");
            sb.AppendLine("[SAL]: Request End <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            System.Diagnostics.Debug.WriteLine(sb.ToString());
#endif
        }
    }
}
