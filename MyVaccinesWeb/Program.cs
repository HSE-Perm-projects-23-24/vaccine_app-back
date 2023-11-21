global using MyVaccinesWeb.Models;
global using Microsoft.EntityFrameworkCore;
global using System.Text.RegularExpressions;
using MyVaccinesWeb.Services.CountriesService;
using MyVaccinesWeb.Services.PatientTypesService;
using MyVaccinesWeb.Services.ProceduresService;
using MyVaccinesWeb.Services.VaccineService;
using MyVaccinesWeb.Services.VaccineTypesService;
using MyVaccinesWeb.Services.VaccineMakersService;
using MyVaccinesWeb.Services.DoneProceduresService;
using MyVaccinesWeb.Services.UserService;
using MyVaccinesWeb.Services.RegistrationService;
using MyVaccinesWeb.Services.AdminsService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProceduresContext>();
builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IPatientTypesService, PatientTypesService>();
builder.Services.AddScoped<IVaccineTypesService, VaccineTypesService>();
builder.Services.AddScoped<IVaccineMakersService, VaccineMakersService>();
builder.Services.AddScoped<IProceduresService, ProceduresService>();
builder.Services.AddScoped<IVaccineService, VaccineService>();
builder.Services.AddScoped<IProceduresService, ProceduresService>();
builder.Services.AddScoped<IDoneProceduresService, DoneProceduresService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRegistrationService, RegistrationService>();
builder.Services.AddScoped<IAdminsService, AdminsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
