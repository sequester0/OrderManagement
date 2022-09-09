using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OrderManagement.API.Middlewares;
using OrderManagement.BusinessEngine;
using OrderManagement.Common.Contracts;
using OrderManagement.Common.Helpers;
using OrderManagement.Data.Models;
using Serilog;
using System.Text;

//Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));
Serilog.Debugging.SelfLog.Enable(Console.Error);
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    //.WriteTo.MongoDB(databaseUrl: "mongodb+srv://keremtest:keremtest123@cluster0.nj54wpn.mongodb.net/OrderManagement",
    //                collectionName: "log",
    //                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose
    //                )
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EGITIM_TESTContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient, ServiceLifetime.Transient);

builder.Host.UseSerilog();

builder.Services.AddCors();
// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("OrderManagementApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Input a valid token to access this API"
    });

    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "OrderManagementApiBearerAuth"
                }
            }, new List<string>()
        }
    });
});

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IUserBusinessEngine, UserBusinessEngine>();
builder.Services.AddScoped<IBasketBusinessEngine, BasketBusinessEngine>();
builder.Services.AddScoped<IProductBusinessEngine, ProductBusinessEngine>();
builder.Services.AddScoped<IBrandBusinessEngine, BrandBusinessEngine>();
builder.Services.AddScoped<IOrderBusinessEngine, OrderBusinessEngine>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

//app.MapControllers();
app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();