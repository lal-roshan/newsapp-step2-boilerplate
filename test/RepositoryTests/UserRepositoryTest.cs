using News_WebApp.Repository;
using Xunit;
namespace Test.RepositoryTests
{
    [TestCaseOrderer("Test.PriorityOrderer", "test")]
    [Collection("NewsDb Fixture")]
    public class UserRepositoryTest
    {
        private readonly UserRepository userRepository;

        public UserRepositoryTest(DatabaseFixture fixture)
        {
            userRepository = new UserRepository(fixture.context);
        }

        [Fact]
        public void IsAuthenticatedShouldSuccess()
        {
            string userId = "Jack";
            string password = "password@123";

            var actual =userRepository.IsAuthenticated(userId, password);
            Assert.True(actual);
        }

        [Fact]
        public void IsAuthenticatedShouldFail()
        {
            string userId = "Jack";
            string password = "password";

            var actual = userRepository.IsAuthenticated(userId, password);
            Assert.False(actual);
        }
    }
}
