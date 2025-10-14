using CurrieTechnologies.Razor.SweetAlert2;
using Service.Interfaces;
using Service.Services;
using Web.Components;
using Web.Handlers;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Token provider scoped (per-request/per-connection)
builder.Services.AddScoped<ITokenProvider, TokenProvider>();

// Auth service que usa el provider
builder.Services.AddScoped<FirebaseAuthService>();

// Handler que adjunta el token Bearer a cada request hacia el backend
builder.Services.AddTransient<AuthenticationHandler>();

// HttpClient nombrado con BaseAddress + AuthenticationHandler
builder.Services.AddHttpClient("BackendApi", client =>
{
    client.BaseAddress = new Uri(Service.Properties.Resources.UrlApi);
}).AddHttpMessageHandler<AuthenticationHandler>();

// Exponer el HttpClient nombrado como HttpClient por defecto para DI
builder.Services.AddTransient<HttpClient>(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("BackendApi"));

// HttpClient por defecto (nombre vacío) con BaseAddress + AuthenticationHandler
builder.Services.AddHttpClient(string.Empty, client => { client.BaseAddress = new Uri(Service.Properties.Resources.UrlApi); }).AddHttpMessageHandler<AuthenticationHandler>();
// Registrar el mapeo abierto: IGenericService<T> -> GenericService<T>
// GenericService<T> recibirá el HttpClient anterior (con handler y base address)
builder.Services.AddScoped(typeof(IGenericService<>),
    typeof(GenericService<>));

// Servicios concretos también pueden convivir
builder.Services.AddHttpClient<IUsuarioService, UsuarioService>(client =>
{
    client.BaseAddress = new Uri(Service.Properties.Resources.UrlApi);
}).AddHttpMessageHandler<AuthenticationHandler>();

builder.Services.AddHttpClient<ILibroService, LibroService>(client =>
{
    client.BaseAddress = new Uri(Service.Properties.Resources.UrlApi);
}).AddHttpMessageHandler<AuthenticationHandler>();

builder.Services.AddSweetAlert2();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
