namespace Web.Services
{
    public interface ITokenProvider
    {
        ValueTask<string?> GetTokenAsync(CancellationToken cancellationToken = default);
        void SetToken(string? token);
        void ClearToken();
    }
}
