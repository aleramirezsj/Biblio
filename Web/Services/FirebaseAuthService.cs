using Firebase.Auth;
using Microsoft.JSInterop;
using Service.Models.Login;
using Service.Services;

namespace Web.Services
{
    public class FirebaseAuthService
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly ITokenProvider _tokenProvider;
        public event Action OnChangeLogin;
        public FirebaseUser CurrentUser { get; set; }

        public FirebaseAuthService(IJSRuntime jsRuntime, ITokenProvider tokenProvider)
        {
            _jsRuntime = jsRuntime;
            _tokenProvider = tokenProvider;
        }

        public async Task<FirebaseUser?> SignInWithEmailPassword(string email, string password, bool rememberPassword)
        {
            var user = await _jsRuntime.InvokeAsync<FirebaseUser?>("firebaseAuth.signInWithEmailPassword", email, password, rememberPassword);
            if (user != null)
            {
                CurrentUser = user;
                await SetUserToken();
                OnChangeLogin?.Invoke();
            }
            return user;
        }

        public async Task<string> createUserWithEmailAndPassword(string email, string password, string displayName)
        {
            var userId = await _jsRuntime.InvokeAsync<string>("firebaseAuth.createUserWithEmailAndPassword", email, password, displayName);
            if (userId != null)
            {
                OnChangeLogin?.Invoke();
            }
            return userId;
        }

        public async Task SignOut()
        {
            await _jsRuntime.InvokeVoidAsync("firebaseAuth.signOut");
            CurrentUser = null;
            _tokenProvider.ClearToken();
            OnChangeLogin?.Invoke();
        }

        public async Task<FirebaseUser?> GetUserFirebase()
        {
            var userFirebase = await _jsRuntime.InvokeAsync<FirebaseUser>("firebaseAuth.getUserFirebase");
            if (userFirebase != null && userFirebase.EmailVerified)
            {
                CurrentUser = userFirebase;
                return userFirebase;
            }
            else return null;
        }

        private async Task SetUserToken()
        {
            var jwtToken = await _jsRuntime.InvokeAsync<string?>("firebaseAuth.getUserToken");
            _tokenProvider.SetToken(jwtToken);
        }

        public async Task<string?> GetUserToken()
        {
            // Usa el provider para resolver y cachear el token
            return await _tokenProvider.GetTokenAsync();
        }

        public async Task<bool> IsUserAuthenticated()
        {
            var user = await GetUserFirebase();
            if (user != null)
            {
                await SetUserToken();
                OnChangeLogin?.Invoke();
            }
            return user != null;
        }

        public async Task<FirebaseUser?> LoginWithGoogle()
        {
            var userFirebase = await _jsRuntime.InvokeAsync<FirebaseUser>("firebaseAuth.loginWithGoogle");
            CurrentUser = userFirebase;
            await SetUserToken();
            OnChangeLogin?.Invoke();
            return userFirebase;
        }
        //recuperación de correo
        public async Task<bool> RecoveryPassword(string email)
        {
               return await _jsRuntime.InvokeAsync<bool>("firebaseAuth.recoveryPassword", email);
        }

    }
}
