using ArenaPro.Api.Filters;
using ArenaPro.CrossCutting;
using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .WriteTo.Graylog(new GraylogSinkOptions
    {
        HostnameOrAddress = builder.Configuration["Graylog"],
        Port = int.Parse(builder.Configuration["GraylogPort"]!),
        TransportType = TransportType.Udp
    })
    .ReadFrom.Configuration(context.Configuration)
    .Enrich.FromLogContext());


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<CustomExceptionFilter>();
builder.Services.AddMvc(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
