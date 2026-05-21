using CoreLayer.API;
using CoreLayer.API.Models;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace TestLayer
{
    public class APITests : BaseTest
    {
        private readonly BaseClient _apiClient = new("https://jsonplaceholder.typicode.com");

        public APITests() : base()
        {
            SetLogLevel("DEBUG");
        }

        [Theory]
        [Trait("Category", "API")]
        [InlineData(new object[] { "id", "name", "username", "email", "address", "phone", "website", "company" })]
        public async Task Task1ValidateThatListOfUsersWithCorrectFieldsIsReturnedSuccesfully(params string[] expectedFields)
        {
            try
            {
                var response = await _apiClient.GetUsersAsync();
                var UserList = JArray.Parse(response.Content!);
                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK && ResponseNoErrorMessage(response), $"API call was not successful. Status code: {response.StatusCode}");
                AssertUsersInListContainsExpectedFields(expectedFields, UserList);
                Log.Info("Task1 List of users returned data successfully.");
            }
            catch (Exception)
            {
                Log.Warn("API Test Task1 failed unexpectedly, check logs for details.");
                throw;
            }
        }

        private static void AssertUsersInListContainsExpectedFields(string[] expectedFields, JArray JsonArray)
        {
            foreach (var user in JsonArray)
            {
                foreach (var field in expectedFields)
                {
                    Assert.True(user[field] != null, $"User does not contain expected field: {field}");
                }
            }
        }

        [Theory]
        [Trait("Category", "API")]
        [InlineData("application/json; charset=utf-8")]
        public async Task Task2ValidateResponseHeaderForAListOfUsers(string expectedContentType)
        {
            try
            {
                var response = await _apiClient.GetUsersAsync();

                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK && ResponseNoErrorMessage(response), $"API call was not successful. Status code: {response.StatusCode}, Error message: {response.ErrorMessage}");
                Assert.True(response.ContentHeaders.Any(h => h.Name.Equals("Content-Type", StringComparison.OrdinalIgnoreCase)), "Response does not contain expected Content-Type header.");
                Assert.True(response.ContentHeaders.Any(h => h.Value.ToString().Contains(expectedContentType)), $"Response does not contain expected Content-Type header with value: {expectedContentType}");
                Log.Info("Task2 Response header for list of users validated successfully.");
            }
            catch (Exception)
            {
                Log.Warn("API Test Task2 failed unexpectedly, check logs for details.");
                throw;
            }
        }
        [Theory]
        [InlineData(10)]
        [Trait("Category", "API")]
        public async Task Task3ValidateThatListOfUsersFieldsContainValidInformation(int expectedCount)
        {
            try
            {
                var response = await _apiClient.GetUsersAsync();
                var duplicateIds = response.Data!
                .GroupBy(u => u.Id)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK && ResponseNoErrorMessage(response), $"API call was not successful. Status code: {response.StatusCode}");
                Assert.True(response.Data!.Count == expectedCount, $"Expected {expectedCount} users, but got {response.Data.Count}.");
                Assert.True(duplicateIds.Count == 0, $"Duplicate user IDs found: {string.Join(", ", duplicateIds)}");
                AssertUsersContainValidInformation(response);
                Log.Info("Task3 List of users fields contain valid information validated successfully.");
            }
            catch (Exception)
            {
                Log.Warn("API Test Task3 failed unexpectedly, check logs for details.");
                throw;
            }
        }

        [Theory]
        [Trait("Category", "API")]
        [InlineData("Johnson", "Mister")]
        public async Task Task4ValidateThatUserCanBeCreatedSuccessfully(string name, string username)
        {
            try
            {
                var newUser = new User
                {
                    Name = name,
                    Username = username,
                };
                var response = await _apiClient.CreateUserAsync(newUser);

                Assert.True(response.StatusCode == System.Net.HttpStatusCode.Created && ResponseNoErrorMessage(response), $"API call was not successful. Status code: {response.StatusCode}, Error message: {response.ErrorMessage}");
                Assert.True(!string.IsNullOrEmpty(response.Data.ToString()), "Response data is empty or null.");
                Assert.True(response.Data.Id.ToString().Any(), "Response does not contain a user ID.");
            }
            catch (Exception)
            {
                Log.Warn("API Test Task4 failed unexpectedly, check logs for details.");
                throw;
            }
        }

        [Fact]
        [Trait("Category", "API")]
        public async Task Task5ValidateThatUserIsNotifiedIfResourceDoesNotExist()
        {
            try
            {
                var response = await _apiClient.GetUsersNotExistingAsync();

                Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound && ResponseNoErrorMessage(response), $"Expected status code 404 Not Found, but got {response.StatusCode}");
                Log.Info("Task5 User is notified when resource does not exist validated successfully.");
            }
            catch (Exception)
            {
                Log.Warn("API Test Task5 failed unexpectedly, check logs for details.");
                throw;
            }
        }
        private static bool ResponseNoErrorMessage(RestResponse response)
        {
            return string.IsNullOrEmpty(response.ErrorMessage);
        }

        private static void AssertUsersContainValidInformation(RestResponse<List<User>> response)
        {
            response.Data?.ForEach(user =>
            {
                Assert.False(string.IsNullOrEmpty(user.Name), "User name should not be null or empty.");
                Assert.False(string.IsNullOrEmpty(user.Username), "User username should not be null or empty.");
                Assert.False(user.Company == null, "User company should not be null.");
                Assert.False(string.IsNullOrEmpty(user.Company.Name), "User company name should not be null or empty.");
            });
        }
    }
}
