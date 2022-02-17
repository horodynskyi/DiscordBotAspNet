using DiscordBotWebApi.Bot;
using DiscordBotWebApi.Options;
using Infrastructure.Commands;
using Infrastructure.Services;
using Microsoft.OpenApi.Models;
using Options.Shikimory;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .Configure<DanbooruOptions>(builder.Configuration.GetSection(DanbooruOptions.Title))
    .Configure<ShikimoryOptions>(builder.Configuration.GetSection(ShikimoryOptions.Title))
    .Configure<DiscordOptions>(builder.Configuration.GetSection(DiscordOptions.Title))
    .Configure<ShikimoryClientOptions>(builder.Configuration.GetSection(ShikimoryClientOptions.Title));

var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

builder.Services.AddTransient<CommandsHandler>();
builder.Services.AddTransient<AdminService>();
builder.Services.AddTransient<ShikimoryService>();
builder.Services.AddTransient<CommandServices>();
builder.Services.AddTransient<SetupSlashCommands>();
builder.Services.AddClient(configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Discord bot API",
        Description = "",
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
