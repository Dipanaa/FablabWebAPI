




using FablabWebAPI.Datos;
using FablabWebAPI.Entities;
using FablabWebAPI.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Servicios

//FluentValidations

builder.Services.AddScoped<IValidator<Noticias>, NoticiasValidator>();



//Automapper
builder.Services.AddAutoMapper(typeof(Program));



builder.Services.AddCors( options =>
    {
        options.AddPolicy("OrigenDeAdminFablab", builder =>
        {
            builder.WithOrigins(["http://localhost:4200", "http://localhost:59318"]) //TODO: Colocar en una parametro de configuracion
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
        });

    }
);

//Controllers
builder.Services.AddControllers().AddJsonOptions(opciones => opciones.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<ApplicationDbContext>(opciones => opciones.UseSqlServer("name=DefaultConnection"));



var app = builder.Build();


//Area de Middlewares

app.UseRouting();

app.UseCors("OrigenDeAdminFablab");

//MapearControladores
app.MapControllers();

app.Run();
