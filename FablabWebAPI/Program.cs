using Google.GenAI;
using FablabWebAPI.Datos;
using FablabWebAPI.Entities;
using FablabWebAPI.Services;
using FablabWebAPI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//SERVICIOS

//Servicios personalizados
builder.Services.AddTransient<IServicioUsuarios,ServicioUsuarios>();
builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenarArchivosDeImagenes>();

//Uso de Gemini
builder.Services.AddTransient<IChatContexto, ChatContexto>();

//FluentValidations Validadores de entidades
builder.Services.AddScoped<IValidator<Noticias>, NoticiasValidator>();
builder.Services.AddScoped<IValidator<Inventario>, InventarioValidator>();
builder.Services.AddScoped<IValidator<Usuario>, UsuarioValidator>();


//Servicio identity con configuracion de entidades
builder.Services.AddIdentityCore<Usuario>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


//Manejador de usuarios para registrar

builder.Services.AddScoped<UserManager<Usuario>>(); //Maneja   
builder.Services.AddScoped<SignInManager<Usuario>>(); //Registra
builder.Services.AddHttpContextAccessor();





//Configuracion JWT

builder.Services.AddAuthentication().AddJwtBearer(opts =>
{
    opts.MapInboundClaims = false; //Para utilizar propios textos de claims

    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["llavejwt"]!)),
        ClockSkew = TimeSpan.Zero,
    };  
});



//Automapper
builder.Services.AddAutoMapper(typeof(Program));


//CORS
builder.Services.AddCors( options =>
    {
        options.AddPolicy("OrigenDeAdminFablab", builder =>
        {
            builder.WithOrigins(["http://localhost:4200", "http://localhost:51323", "http://10.0.2.2:3000"]) //TODO: Colocar en una parametro de configuracion
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

//using (var scope = app.Services.CreateScope())
//{
//    var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    if (dbcontext.Database.IsRelational())
//    {
//        dbcontext.Database.Migrate();
//    }
//}





//Area de Middlewares

app.UseRouting();

app.UseCors("OrigenDeAdminFablab");

app.UseAuthentication();
app.UseAuthorization();

//MapearControladores
app.MapControllers();

app.Run();
