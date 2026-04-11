using System.Threading;
using System.Linq;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using MediaBrowser.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediaBrowser.Common.Net;

namespace Jellyfin.Plugin.TMDbPlus.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("/plugin/tmdbplus")]
    public class ApiController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiController"/> class.
        /// </summary>
        /// <param name="httpClientFactory">The <see cref="IHttpClientFactory"/>.</param>
        public ApiController(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }


        /// <summary>
        /// 代理访问图片.
        /// </summary>
        [Route("proxy/image")]
        [HttpGet]
        public async Task<Stream> ProxyImage(string url)
        {

            if (string.IsNullOrEmpty(url))
            {
                throw new ResourceNotFoundException();
            }

            HttpResponseMessage response;
            var httpClient = GetHttpClient();
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, url))
            {
                response = await httpClient.SendAsync(requestMessage);
            }
            var stream = await response.Content.ReadAsStreamAsync();

            Response.StatusCode = (int)response.StatusCode;
            if (response.Content.Headers.ContentType != null)
            {
                Response.ContentType = response.Content.Headers.ContentType.ToString();
            }
            Response.ContentLength = response.Content.Headers.ContentLength;

            foreach (var header in response.Headers)
            {
                Response.Headers.Add(header.Key, header.Value.First());
            }

            return stream;
        }

        private HttpClient GetHttpClient()
        {
            var client = _httpClientFactory.CreateClient(NamedClient.Default);
            return client;
        }
    }
}
