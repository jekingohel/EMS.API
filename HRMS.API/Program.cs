using HRMS.API.FIlters;
using HRMS.API.Handlers;
using HRMS.API.ServiceExtensions;
using HRMS.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
//using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(x => x.SuppressModelStateInvalidFilter = true);
builder.Services.AddControllers(x => x.Filters.Add<ValidationFilter>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("CorsPolicy", build =>
{
	build.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
}));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

//builder.Host.UseSerilog((context, configuration) =>
//	configuration.ReadFrom.Configuration(context.Configuration));

var config = builder.Configuration.GetSection("ConnectionStrings");
builder.Services.Configure<AppConfigs>(config);
builder.Services.ConfigureServices();

var app = builder.Build();
app.UseExceptionHandler();
app.UseSwagger();
app.UseSwaggerUI();
//app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
