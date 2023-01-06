using EventDemoAPI.DBContext;
using EventDemoAPI.Helper;
using EventDemoAPI.Interface;
using EventDemoAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
.CreateLogger();

logger.Debug("Start...");

var sqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContextFactory<DbEventContext>(options =>
{
    Host.CreateDefaultBuilder().UseSerilog();
    options.UseSqlServer(sqlConnectionString,
    sqlServerOptionsAction: sqlOption =>
    {
        sqlOption.EnableRetryOnFailure();
    }
    );
});
//builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventBookingHelper, EventBookingHelper>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
