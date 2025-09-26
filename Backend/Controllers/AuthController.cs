using Firebase.Auth;
using Firebase.Auth.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Models;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        FirebaseAuthClient firebaseAuthClient;
        IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            SetFirebaseConfig();
        }

        private void SetFirebaseConfig()
        {
            var config = new FirebaseAuthConfig
            {
                ApiKey = _configuration["ApiKeyFirebase"],
                AuthDomain = _configuration["AuthDomainFirebase"],
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()

                },
            };

            firebaseAuthClient = new FirebaseAuthClient(config);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                var credentials = await firebaseAuthClient.SignInWithEmailAndPasswordAsync(login.Username, login.Password);
                return Ok(credentials.User.GetIdTokenAsync().Result);
            }catch(FirebaseAuthException ex)
            {
                return BadRequest(new { message = ex.Reason.ToString() });
            }
            
        }

        [HttpPost("resetpassword")]
        public async Task<IActionResult> ResetPassword([FromBody] LoginDTO login)
        {
            try
            {
                await firebaseAuthClient.ResetEmailPasswordAsync(login.Username);
                return Ok();
            }
            catch (FirebaseAuthException ex)
            {
                return BadRequest(new { message = ex.Reason.ToString() });
            }

        }
    }
}
