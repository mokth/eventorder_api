using EventDemoAPI.DBContext;
using EventDemoAPI.Helper;
using EventDemoAPI.Interface;
using EventDemoAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// serilog preparation
var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
.CreateLogger();

logger.Debug("Start...");

//read connection string from appsetting.json
var sqlConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//register database context
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

//register serilog services
builder.Logging.AddSerilog(logger);

//register repositoty and helper service
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IEventBookingHelper, EventBookingHelper>();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register Automapper service
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
