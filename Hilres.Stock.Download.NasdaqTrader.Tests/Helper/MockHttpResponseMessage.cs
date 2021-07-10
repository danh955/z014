// <copyright file="MockHttpResponseMessage.cs" company="None">
// Free and open source code.
// </copyright>
namespace Hilres.Stock.Download.NasdaqTrader.Tests.Helper
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Mock of a HttpMessageHandler class.
    /// </summary>
    public class MockHttpResponseMessage : HttpMessageHandler
    {
        private readonly IDictionary<string, HttpResponseMessage> messages;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockHttpResponseMessage"/> class.
        /// </summary>
        /// <param name="messages">A dictionary of URI and HttpResponseMessage.</param>
        public MockHttpResponseMessage(IDictionary<string, HttpResponseMessage> messages)
        {
            this.messages = messages;
        }

        /// <inheritdoc/>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string uri = request.RequestUri.ToString();
            var response = this.messages.ContainsKey(uri)
                ? this.messages[uri] ?? new HttpResponseMessage(HttpStatusCode.NoContent)
                : new HttpResponseMessage(HttpStatusCode.NotFound);

            response.RequestMessage = request;
            return Task.FromResult(response);
        }
    }
}