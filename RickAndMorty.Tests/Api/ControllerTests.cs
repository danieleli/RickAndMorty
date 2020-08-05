namespace RickAndMorty.Tests.Api
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.TestHost;
    using RickAndMorty.Utils;
    using Xunit;
    using static System.FormattableString;

    public abstract class ControllerTests : IClassFixture<SetupFixture>
    {
        protected ControllerTests(SetupFixture setupFixture)
        {
            this.SetupFixture = setupFixture;
        }

        public SetupFixture SetupFixture { get; }

        protected async Task SendAndAssertSuccessAsync(
            HttpMethod method,
            string path,
            bool authenticated = true,
            object? model = null)
        {
            using var response = await this.Send(
                method,
                path,
                model: model);

            AssertResponseIsSuccessful(response);
        }

        protected async Task SendAndAssertSuccessAsync<T>(
            HttpMethod method,
            string path,
            Action<T> assertModel,
            bool authenticated = true,
            object? model = null)
            where T : class
        {
            using var response = await this.Send(
                method,
                path,
                model);

            AssertResponseIsSuccessful(response);

            await AssertModel(response, assertModel);
        }

        protected async Task SendAndAssertFailureAsync(
            HttpMethod method,
            string path,
            HttpStatusCode statusCode,
            object? model = null)
        {
            using var response = await this.Send(
                method,
                path,
                model: model);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(statusCode, response.StatusCode);
        }

        private static async Task AssertModel<T>(HttpResponseMessage response, Action<T> assertModel)
            where T : class
        {
            var json = await response.Content.ReadAsStringAsync();
            T? model = (json ?? string.Empty).FromJson<T>(throwOnFailure: true);

            if (model is null)
            {
                throw new HttpRequestException("Could not deserialize model from json.");
            }

            assertModel(model);
        }

        private static void AssertResponseIsSuccessful(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Http Request failed with status code {response.StatusCode} and message: '{response.ReasonPhrase}'.");
            }
        }

        public async Task<HttpResponseMessage> Send(
            HttpMethod method,
            string path,
            object? model = null)
        {
            RequestBuilder requestBuilder = this.SetupFixture.Setup.TestServer.CreateRequest(Invariant($"/api/{path}"));

            if (model != null)
            {
                requestBuilder.And(
                    m => m.Content = new StringContent(model.ToJson(), Encoding.UTF8, "application/json"));
            }

            return await requestBuilder.SendAsync(method.ToString());
        }
    }
}