using EmployeeAttendancePortalApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Allow CORS from any origin (development/testing). Remove or restrict in production.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllDev", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure EF Core with SQL Server (uses DefaultConnection from appsettings.json)
builder.Services.AddDbContext<AttendanceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Use scoped repository backed by EF Core
builder.Services.AddScoped<EmployeeAttendancePortalApi.Repositories.IEmployeeAttendanceRepository, EmployeeAttendancePortalApi.Repositories.EmployeeAttendanceRepository>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Keep OpenApi and also add Swagger (Swashbuckle) so classic Swagger UI is available at /swagger
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply any pending EF Core migrations at startup so the hosted DB schema matches the model.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AttendanceDbContext>();
    try
    {
        db.Database.Migrate();
    }
    catch
    {
        // If migrations cannot be applied (permissions, network), ignore so the app can still start.
        // Consider logging this error in production.
    }
}

// Configure the HTTP request pipeline.
// Map OpenAPI UI so the built-in OpenAPI page is available.
app.MapOpenApi();

// Enable CORS. Use the policy configured above.
app.UseCors("AllowAllDev");

// Enable classic Swagger middleware and UI (Swashbuckle) at /swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee Attendance API V1");
    // Serve the Swagger UI at /swagger
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
