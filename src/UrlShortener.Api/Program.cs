var applicationSettings = new ApplicationSettings();
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.Bind("ApplicationSettings", applicationSettings);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton(applicationSettings);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IShortUrlStorage, ShortUrlStorage>();
builder.Services.AddSingleton<IUrlShortenerService, UrlShortenerService>();

builder.Services.AddScoped<IValidator<CreateShortUrlRequest>, CreateShortUrlRequestValidator>();

builder.Services.Configure<RouteOptions>(o => o.LowercaseUrls = true);

var app = builder.Build();

app.UseSwagger(c =>
{
    c.RouteTemplate = "docs/swagger/{documentname}/swagger.json";
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/docs/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "docs/swagger";
});

app.UseExceptionHandler("/api/v1/error");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();

app.Run();
