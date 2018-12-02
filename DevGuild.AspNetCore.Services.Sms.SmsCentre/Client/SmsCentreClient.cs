using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DevGuild.AspNetCore.Services.Sms.SmsCentre.Client
{
    /// <summary>
    /// Represents sms centre client.
    /// </summary>
    public sealed class SmsCentreClient : IDisposable
    {
        private readonly HttpClient client;
        private readonly String username;
        private readonly String password;

        /// <summary>
        /// Initializes a new instance of the <see cref="SmsCentreClient"/> class.
        /// </summary>
        /// <param name="host">The host.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        public SmsCentreClient(String host, String username, String password)
        {
            this.username = username;
            this.password = password;
            this.client = new HttpClient();
            this.client.BaseAddress = new Uri(host);
        }

        /// <summary>
        /// Asynchronously sends the sms message.
        /// </summary>
        /// <param name="request">The sending request.</param>
        /// <returns>A task that represents the operation.</returns>
        /// <exception cref="SmsCentreException">Server reported failure.</exception>
        /// <exception cref="InvalidOperationException">Unable to process server response</exception>
        public async Task<SmsCentreSendResponse> SendAsync(SmsCentreSendRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException($"{nameof(request)} is null", nameof(request));
            }

            if (request.Phones == null || request.Phones.Length == 0)
            {
                throw new ArgumentException("No phone provided", nameof(request));
            }

            if (String.IsNullOrEmpty(request.Message))
            {
                throw new ArgumentException($"Message is null or empty", nameof(request));
            }

            var parameters = new Dictionary<String, String>();
            parameters["login"] = this.username;
            parameters["psw"] = this.password;
            parameters["phones"] = String.Join(",", request.Phones);
            parameters["mes"] = request.Message;

            if (request.Identifier != null)
            {
                parameters["id"] = request.Identifier;
            }

            if (request.Sender != null)
            {
                parameters["sender"] = request.Sender;
            }

            parameters["charset"] = "utf-8";
            parameters["fmt"] = "3";

            var response = await this.client.PostAsync($"send.php", new FormUrlEncodedContent(parameters));
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var jsonResponse = JObject.Parse(content);
            if (jsonResponse["error_code"] is JValue errorCode && jsonResponse["error"] is JValue error)
            {
                var errorCodeValue = errorCode.Value<Int32>();
                var errorValue = error.Value<String>();

                throw new SmsCentreException(errorCodeValue, errorValue);
            }

            if (jsonResponse["id"] is JValue id && jsonResponse["cnt"] is JValue count)
            {
                return new SmsCentreSendResponse(id.Value<String>(), count.Value<Int32>());
            }

            throw new InvalidOperationException("Unable to process server response");
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.client?.Dispose();
        }
    }
}
