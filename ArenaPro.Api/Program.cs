using ArenaPro.Api.Filters;
using ArenaPro.Infra.Data;
using ArenaPro.CrossCutting;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Fatal)
    .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Fatal)
    .WriteTo.Seq(builder.Configuration["SeqUrl"]!)
    .ReadFrom.Configuration(context.Configuration)
    .Enrich.FromLogContext());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Arena Pro", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<CustomExceptionFilter>();

builder.Services.AddAuthorization();
builder.Services
    .AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Arena Pro v1");
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.MapGroup("/Auth").MapIdentityApi<IdentityUser>();

app.Run();
