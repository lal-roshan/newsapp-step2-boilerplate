using News_WebApp;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace Test.ControllerTests
{
    [Collection("News Fixture")]
    public class ControllersIntegrationTest
    {
        private readonly HttpClient _client;
        public ControllersIntegrationTest(NewsWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact, TestPriority(1)]
        public async Task LandingPageShouldDisplayLoginForm()
        {
            // Act
            var response = await _client.GetAsync("/login");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Match userIdInput = Regex.Match(responseString, "<input .+ type=\"text\"");
            Assert.True(userIdInput.Success);
            Match passwordInput = Regex.Match(responseString, "<input .+ type=\"password\"");
            Assert.True(passwordInput.Success);
        }

        [Fact, TestPriority(2)]
        public async Task InvalidUserLoginShouldDisplayError()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/login");
            var formModel = new Dictionary<string, string>
                {
                    { "UserId", "Jack" },
                    { "Password", "password"}
                };

            postRequest.Content = new FormUrlEncodedContent(formModel);

            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Match userIdInput = Regex.Match(responseString, "<input .+ type=\"text\"");
            Assert.True(userIdInput.Success);
            Match passwordInput = Regex.Match(responseString, "<input .+ type=\"password\"");
            Assert.True(passwordInput.Success);
            Assert.Contains("Invalid User Id or Password", responseString);
        }

        [Fact, TestPriority(3)]
        public async Task UserLoginShouldRedirectToNewsIndex()
        {
            var postRequest = new HttpRequestMessage(HttpMethod.Post, "/login");
            var formModel = new Dictionary<string, string>
                {
                    { "UserId", "Jack" },
                    { "Password", "password@123"}
                };

            postRequest.Content = new FormUrlEncodedContent(formModel);
            var response = await _client.SendAsync(postRequest);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            MatchCollection groups = Regex.Matches(responseString, $"<div class=\"form-group\">");
            Assert.Equal(5, groups.Count);
            MatchCollection inputs = Regex.Matches(responseString, "<input .+ type=\"text\"");
            Assert.Equal(2, inputs.Count);
            Assert.Contains("form", responseString);
            Assert.Contains("Title", responseString);
            Assert.Contains("Content", responseString);
            Assert.Contains("PublishedAt", responseString);
            Assert.Contains("<label class=\"control-label\">Select an image to upload</label>", responseString);
            Assert.Contains("input type=\"file\"", responseString);
            Assert.Contains("table", responseString);
            Assert.Contains("IT industry in 2020", responseString);
        }
    }
}
