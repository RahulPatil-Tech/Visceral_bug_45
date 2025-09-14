using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Viseralbug.Data;
using Viseralbug.Repositories;
using Viseralbug.Services;
using Viseralbug.Models;
using Viseralbug.Validators;
using Viseralbug.Middleware;
using FluentValidation;
using Serilog;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/viseralbug-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Configure services
builder.Services.AddControllers();

// Configure FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();

// Configure database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.SecretKey ?? "default-key")),
            ValidateIssuer = true,
            ValidIssuer = jwtSettings?.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings?.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
        policy.WithOrigins(allowedOrigins ?? new[] { "http://localhost:3000" })
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

// Configure file upload settings
builder.Services.Configure<FileUploadSettings>(builder.Configuration.GetSection("FileUpload"));

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IBugRepository, BugRepository>();
builder.Services.AddScoped<IBugLogRepository, BugLogRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskLogRepository, TaskLogRepository>();

// Register services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<BugService>();
builder.Services.AddScoped<BugLogService>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<TaskLogService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<FileUploadService>();
builder.Services.AddSingleton<MailService>();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "Viseralbug API", 
        Version = "v1",
        Description = "API for Viseralbug Bug Tracking System",
        Contact = new OpenApiContact
        {
            Name = "Viseralbug Team",
            Email = "support@viseralbug.com"
        }
    });
    
    // Configure JWT authentication in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Viseralbug API v1");
        c.RoutePrefix = "swagger";
    });
}

// Use global exception handling middleware
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowFrontend");

// Serve static files for uploads
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Ensure database is created and seed default admin user
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    try
    {
        // Drop and recreate the database to ensure correct schema
        Log.Information("Dropping existing database...");
        context.Database.EnsureDeleted();
        
        Log.Information("Creating new database with correct schema...");
        context.Database.EnsureCreated();
        
        Log.Information("Database created successfully");
        
        // Seed default admin user
        await SeedDefaultAdminUser(context);
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Error during database setup");
        Console.WriteLine($"Database setup error: {ex.Message}");
    }
}

try
{
    Log.Information("Starting Viseralbug application");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

// Method to seed default admin user
static async Task SeedDefaultAdminUser(ApplicationDbContext context)
{
    try
    {
        // Check if admin user already exists
        var existingAdmin = await context.Users.FirstOrDefaultAsync(u => u.Username == "admin@gmail.com");
        
        if (existingAdmin == null)
        {
            // Create default admin user
            var adminUser = new User
            {
                Username = "admin@gmail.com",
                Email = "admin@gmail.com",
                Password = PasswordHasher.HashPassword("Admin@123"),
                Role = "Admin",
                Name = "System Administrator",
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            
            context.Users.Add(adminUser);
            await context.SaveChangesAsync();
            
            Log.Information("Default admin user created successfully");
            Log.Information("Admin credentials - Username: admin@gmail.com, Password: Admin@123");
            Console.WriteLine("=== ADMIN CREDENTIALS ===");
            Console.WriteLine("Username: admin@gmail.com");
            Console.WriteLine("Password: Admin@123");
            Console.WriteLine("========================");
        }
        else
        {
            Log.Information("Default admin user already exists");
            Console.WriteLine("=== ADMIN CREDENTIALS (EXISTING) ===");
            Console.WriteLine("Username: admin@gmail.com");
            Console.WriteLine("Password: Admin@123");
            Console.WriteLine("================================");
        }
    }
    catch (Exception ex)
    {
        Log.Error(ex, "Error creating default admin user");
        Console.WriteLine($"Error creating admin user: {ex.Message}");
    }
}
