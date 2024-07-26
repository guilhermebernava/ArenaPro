using ArenaPro.Api.Filters;
using ArenaPro.CrossCutting;
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
