using FinDashboard.API.Data;
using FinDashboard.API.Repository.IRepository;
using FinDashboard.API.Repository;
using Microsoft.EntityFrameworkCore;
using FinDashboard.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FinDashboard.API.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyHeader() 
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers() //enables controllers that process req like get post put delete
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;//ignore or skip the properties of class which points to each other class
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull; //it will ingonre propr having null values
    });

builder.Services.AddEndpointsApiExplorer(); //collect or document all the api endpoints all together
builder.Services.AddSwaggerGen();//add swagger

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;// if req is there without authorize then tell authorize first to avail it
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var token = context.Request.Headers["Authorization"];
                
            if (!string.IsNullOrEmpty(token))
            {
                context.Token = token.ToString()?.Split(" ")[1];
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminPolicy", policy =>
                    policy.RequireRole("Admin"))
    .AddPolicy("UserPolicy", policy =>
                    policy.RequireRole("User"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IHoldingRepository, HoldingRepository>();
builder.Services.AddScoped<IPortfolioPerformanceHistoryRepository, PortfolioPerformanceHistoryRepository>();
builder.Services.AddScoped<IStockPriceHistoryRepository, StockPriceHistoryRepository>();
builder.Services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();
builder.Services.AddHostedService<StockDataUpdater>();
builder.Services.AddDbContext<FinDashboardDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("FinDashboardConnectionString")));
builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("FinDashboardAuthConnectionString")));
builder.Services.AddHttpClient<FinHubService>();
builder.Services.AddScoped<TokenGenerator>();
builder.Services.AddSingleton<MqttService>();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {// makes sure all the api requires jwt token for access 
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
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";  // api documentation contains xml comments 
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);//generates xml file tht contains xml comments  
    options.IncludeXmlComments(xmlPath); // tells swagger to include xml comments 

});

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var mqttService = app.Services.GetRequiredService<MqttService>();
Task.Run(async () =>
{
    await mqttService.ConnectAsync();
}).GetAwaiter().GetResult();


app.Run();
