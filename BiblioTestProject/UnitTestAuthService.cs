using Microsoft.Extensions.Configuration;
using Service.DTOs;
using Service.Models;
using Service.Services;

namespace BiblioTestProject
{
    public class UnitTestAuthService
    {
        private async Task LoginTest()
        {
            var serviceAuth = new AuthService();
            var token = await serviceAuth.Login(new LoginDTO { Username = "aleramirezsj@gmail.com", Password = "234567" });
            Console.WriteLine($">>>>>>>>>>>>>>>>>>>>>>>>>>Token: {token}");
        }

        [Fact]
        public async Task Reset_Password_Works_Correctly()
        {
            // Arrange
            await LoginTest();
            var serviceAuth = new AuthService();
            var loginDto = new LoginDTO
            {
                Username = "aleramirezsj@gmail.com",
                Password = "no hace falta en este proceso"
            };
            // Act
            var result = await serviceAuth.ResetPassword(loginDto);
            // Assert
            Assert.True(result);

        }
    }
}
