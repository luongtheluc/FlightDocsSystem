using FlightDocsSystem.DataAccess.Responsitory;
using FlightDocsSystem.DataAccess.Responsitory.IResponsitory;
using FlightDocsSystem.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
