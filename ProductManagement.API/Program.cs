using Microsoft.EntityFrameworkCore;
using NLog;

using ProductManagement.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy("AllowAnyHost", policy => policy
			.WithOrigins("http://localhost:3000", "http://localhost", "http://product_management_app", "http://product_management_app:3000", "http://70.153.8.83:3000", "http://70.153.8.83")
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials()));

builder.Configuration
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", optional: true)
	.AddEnvironmentVariables();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductManagementDbContext>(option =>
{
	string? serverDbConnectionString = builder.Configuration.GetConnectionString("DbProjectManagementConnection");
	option.UseNpgsql(serverDbConnectionString);
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

LogManager.Setup().LoadConfigurationFromFile("nlog.config", true);
builder.Services.AddSingleton<NLog.ILogger>(LogManager.GetCurrentClassLogger());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAnyHost");
app.UseAuthorization();

app.MapControllers();

app.Run();
