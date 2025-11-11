using CurrieTechnologies.Razor.SweetAlert2;
using Service.Interfaces;
using Service.Services;
using Web.Components;
using Web.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();
builder.Configuration.AddConfiguration(configuration);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
//configuro la inyección de dependencias de IConfiguration en InstitutoAppService
builder.Services.AddScoped<IInstitutoAppService, InstitutoAppService>();
//builder.Services.AddSingleton<IConfiguration>(builder.Configuration);


// caching memory   
builder.Services.AddMemoryCache();
// Auth service que usa el provider
builder.Services.AddScoped<FirebaseAuthService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGeminiService, GeminiService>();

builder.Services.AddScoped(typeof(IGenericService<>),
    typeof(GenericService<>));
//libroService
builder.Services.AddScoped<ILibroService, LibroService>();
//prestamoService
builder.Services.AddScoped<IPrestamoService, PrestamoService>();
//usuarioService
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<CarreraService>();
builder.Services.AddHttpClient();

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
