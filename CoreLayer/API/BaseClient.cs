using CoreLayer.API.Models;
using RestSharp;
using RestSharp.Serializers.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreLayer.API
{
    public class BaseClient
    {
        private readonly IRestClient _client;

        public BaseClient(string endpoint)
        {
            var serializerOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true
            };
            _client = new RestClient(
                options: new() { BaseUrl = new(endpoint) },
                configureSerialization: s => s.UseSystemTextJson(serializerOptions));
        }

        public async Task<RestResponse<List<User>>> GetUsersAsync()
        {
            var request = new RestRequest($"/users", Method.Get);

            var response = await _client.ExecuteAsync<List<User>>(request);

            return response;
        }

        public async Task<RestResponse<User>> CreateUserAsync(User user)
        {
            var request = new RestRequest($"/users", Method.Post)
                .AddJsonBody(user);

            var response = await _client.ExecuteAsync<User>(request);

            return response;
        }

        public async Task<RestResponse> GetUsersNotExistingAsync()
        {
            var request = new RestRequest($"/invalidendpoint", Method.Get);

            var response = await _client.GetAsync(request);

            return response;
        }
    }
}
