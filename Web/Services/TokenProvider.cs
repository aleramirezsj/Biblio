using System.Threading;
using System.Threading.Tasks;

namespace Web.Services
{
    /// <summary>
    /// Token provider scoped que almacena el JWT en memoria para el usuario/conexión actual.
    /// No depende de FirebaseAuthService para evitar dependencias circulares.
    /// </summary>
    public class TokenProvider : ITokenProvider
    {
        private string? _token;

        public ValueTask<string?> GetTokenAsync(CancellationToken cancellationToken = default)
            => ValueTask.FromResult(_token);

        public void SetToken(string? token) => _token = token;

        public void ClearToken() => _token = null;
    }
}
