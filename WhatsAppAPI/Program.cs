using Fathy.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WhatsAppAPI.Models;
using WhatsAppAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<MetaSettings>(builder.Configuration.GetSection(nameof(MetaSettings)));

builder.Services.AddSingleton<IWhatsAppService, WhatsAppService>();

var openApiInfo = new OpenApiInfo
{
    Title = "WhatsAppAPI",
    Version = "v1"
};

builder.Services.AddSwaggerService(openApiInfo);

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint($"/swagger/{openApiInfo.Version}/swagger.json", openApiInfo.Title);
});

app.MapControllers();

app.Run();
