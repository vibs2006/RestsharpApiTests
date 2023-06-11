using RestSharp;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using GraphQLProductApp.Data;
using Xunit.Abstractions;
using Newtonsoft.Json;
using NuGet.Frameworks;
using RestSharpMain.Test.Models;

namespace RestSharpMain.Test
{
    public class AuthenticationTests : BaseHelper
    {
        public AuthenticationTests(ITestOutputHelper outputHelper) : base(outputHelper)
        {
            _logger = _output.BuildLoggerFor<AuthenticationTests>();
        }

        string _token = string.Empty;

        [Fact]
        public async Task GetTokenFromAPI_Returns_ValidToken()
        {
            //Arrange
            var request = new RestRequest("api/Authenticate/Login");
            request.AddJsonBody(new  
                                { 
                                    username = "KK",
                                    password = "123456"
                                });

            //Act
            var response = await RestClientInstance.PostAsync<LoginResponse>(request);

            //Assert

            response.Should().NotBeNull();
            response?.Token.Should().NotBeNullOrWhiteSpace();
            _output.WriteLine("Token Received is");
            _output.WriteLine(response?.Token);
            _token = response?.Token ?? throw new NullReferenceException(response?.Token);
            Assert.NotNull(response);
        }


        [Fact]
        public async Task GetAllProducts_Tests_Authenticated_Returns_ValidDataset()
        {
            var client = RestClientInstance;

            //Arrange
            var request = new RestRequest("api/Authenticate/Login");
            request.AddJsonBody(new
            {
                username = "KK",
                password = "123456"
            });

            //Act
            var responseToken = await RestClientInstance.PostAsync<LoginResponse>(request);

            _token = responseToken?.Token ?? throw new NullReferenceException($"{responseToken?.Token}");

            _output.WriteLine($"Token received is {_token}");

            

            var authRequest = new RestRequest("Product/GetProducts");
            //authRequest.AddHeader("Authorization", "Bearer "+_token);

            AddOrUpdateAuthenticationTokenHeader(authRequest, _token);


            var productResponse = await RestClientInstance.GetAsync<List<Product>>(authRequest);

            _output.LogObject(productResponse);

            

            //var responseToken = await client.GetAsync<List<Product>>(authRequest);
        }
    }
}