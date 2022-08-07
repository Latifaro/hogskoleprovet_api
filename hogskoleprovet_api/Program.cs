using hogskoleprovet_api;
using hogskoleprovet_api.Model;
using hogskoleprovet_api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<QuestionStoreDatabaseSettings>(builder.Configuration.GetSection("QuestionStoreDatabase"));
builder.Services.AddTransient<QuestionService>();
//JwtAuth
builder.Services.Configure<JwtAuthentication>(builder.Configuration.GetSection("JwtAuthentication"));
builder.Services.AddSingleton<IPostConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

class ConfigureJwtBearerOptions : IPostConfigureOptions<JwtBearerOptions>
{
    private readonly IOptions<JwtAuthentication> _jwtAuthentication;

    public ConfigureJwtBearerOptions(IOptions<JwtAuthentication> jwtAuthentication)
    {
        _jwtAuthentication = jwtAuthentication ?? throw new System.ArgumentNullException(nameof(jwtAuthentication));
    }

    public void PostConfigure(string name, JwtBearerOptions options)
    {
        var jwtAuthentication = _jwtAuthentication.Value;

        options.ClaimsIssuer = jwtAuthentication.ValidIssuer;
        options.IncludeErrorDetails = true;
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateActor = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtAuthentication.ValidIssuer,
            ValidAudience = jwtAuthentication.ValidAudience,
            IssuerSigningKey = jwtAuthentication.SymmetricSecurityKey,
            NameClaimType = ClaimTypes.NameIdentifier
        };
    }
}
