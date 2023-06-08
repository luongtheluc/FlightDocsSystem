using System.Text;
using FlightDocsSystem.DataAccess.Data;
using FlightDocsSystem.DataAccess.Responsitory;
using FlightDocsSystem.DataAccess.Responsitory.IResponsitory;
using FlightDocsSystem.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "standard authorization header using the bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();


}
);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            builder.Configuration.GetSection("AppSettings:Token").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
builder.Services.AddDbContext<FlightDocsSystemContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IAircraftResponsitory, AircraftResponsitory>();
builder.Services.AddScoped<IAircraftTypeResponsitory, AircraftTypesResponsitory>();
builder.Services.AddScoped<IAirportResponsitory, AirportResponsitory>();
builder.Services.AddScoped<IDocumentResponsitory, DocumentResponsitory>();
builder.Services.AddScoped<IFlightResponsitory, FlightResponsitory>();
builder.Services.AddScoped<IFlightDocumentTypeResponsitory, FlightDocumentTypeResponsitory>();
builder.Services.AddScoped<IPassengerResponsitory, PassengerResponsitory>();
builder.Services.AddScoped<IRoleResponsitory, RoleResponsitory>();
builder.Services.AddScoped<IAuthResponsitory, AuthResponsitory>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
