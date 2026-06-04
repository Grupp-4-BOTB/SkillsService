using SkillsService.Application;
using SkillsService.Infrastructure;
using SkillsService.Presentation.API.Endpoints;
using SkillsService.Presentation.API.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiKeyOption>(builder.Configuration.GetSection(ApiKeyOption.SectionName));
builder.Services.AddScoped<ApiKeyEndpointFilter>();

builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "text/plain";
        var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();

        if (error != null)
        {
            await context.Response.WriteAsync(error.Error.Message);
        }
    });
});

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.UseHttpsRedirection();
app.MapSkillsEndpoints();

app.Run();

