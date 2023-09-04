using Library.AccesData.Memory;
using Library.BusinessLogic.Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Obligatorio.BussinesLogic.CasosDeUso.Caba�asUC;
using Obligatorio.BussinesLogic.CasosDeUso.MantenimientosUC;
using Obligatorio.BussinesLogic.CasosDeUso.SistemaUC.Caba�aUCSistema;
using Obligatorio.BussinesLogic.CasosDeUso.TiposUC;
using Obligatorio.BussinesLogic.CasosDeUso.Usuario;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ICaba�asUC;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.IMantenimientosUC;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ISistemaUC.ICaba�aUCSistema;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ITiposUC;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.IUsuarioUC;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ISistemaUC.IUsuarioUCSistema;
using Obligatorio.BussinesLogic.CasosDeUso.SistemaUC.UsuarioUCSistema;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using Obligatorio.LogicaDeAplicacion.CasosDeUso.UsuarioLogeadoUC.Caba�asUC;
using Obligatorio.LogicaDeAplicacion.InterfacesCasosDeUso.IUsuarioLogeadoUC.ICaba�asUC;
using Obligatorio.LogicaDeAplicacion.InterfacesCasosDeUso.IUsuarioLogeadoUC.IMantenimientosUC;
using Obligatorio.LogicaDeAplicacion.CasosDeUso.UsuarioLogeadoUC.MantenimientosUC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var rutaArchivo = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WebApi.xml");
builder.Services.AddSwaggerGen(opciones =>
{
    //Se agrega la opcion de autenticarse en Swagger
    opciones.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
    {
        Description = "Autorizacion estandar mediante esquema Bearer",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    opciones.OperationFilter<SecurityRequirementsOperationFilter>();

    opciones.IncludeXmlComments(rutaArchivo);
    opciones.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Documentaci�n de Obligatorio.Api",
        Description = "Aqui se encuentran todos los endpoint activos para utilizar los servicios del proyecto obligatorio",
        Contact = new OpenApiContact
        {
            Email = "ignaciopataro@gmail.com"
        },
        Version = "v1"
    });
});

// Configurar de la autenticacion
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opciones =>
{
    opciones.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:SecretTokenKey").Value!)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICabanaRepository, SQLCabanaRepository>();
builder.Services.AddScoped<ITipoRepository, SQLTipoRepository>();
builder.Services.AddScoped<IMantenimientoRepository, SQLMantenimientoRepostory>();
builder.Services.AddScoped<IUsuarioRepository, SQLUsuarioRespository>();

//Caba�a
builder.Services.AddScoped<IObtenerCaba�aPorNombreUC, ObtenerCaba�aPorNombreUC>();
builder.Services.AddScoped<IEliminarCaba�aUC, EliminarCaba�aUC>();
builder.Services.AddScoped<IObtenerCaba�asHabilitadasUC, ObtenerCaba�asHabilitadasUC>();
builder.Services.AddScoped<IObtenerCaba�asPorCapacidadUC, ObtenerCaba�asPorCapacidadUC>();
builder.Services.AddScoped<IObtenerCaba�asPorTipoUC, ObtenerCaba�asPorTipoUC>();
builder.Services.AddScoped<IObtenerCaba�asUC, ObtenerCaba�asUC>();
builder.Services.AddScoped<IRegistrarCaba�aUC, RegistrarCaba�aUC>();
builder.Services.AddScoped<IObtenerCaba�aPorMontoyTipoUC, ObtenerCaba�aPorMontoyTipo>();

builder.Services.AddScoped<IObtenerCaba�aPorId, ObtenerCaba�aPorId>();

//Mantenimientos
builder.Services.AddScoped<IAgregarMantenimientoUC, AgregarMantenimientoUC>();
builder.Services.AddScoped<IObtenerMantenimientosEntreFechasUC, ObtenerMantenimientoEntreFechasUC>();
builder.Services.AddScoped<IGetMantenimientosEntreCapacidadesUC, GetMantenimientosEntreCapacidadesUC>();

//Tipos
builder.Services.AddScoped<IAgregarTipoUC, AgregarTipoUC>();
builder.Services.AddScoped<IEliminarTipoUC, EliminarTipoUC>();
builder.Services.AddScoped<IModificarTipoUC, ModificarTipoUC>();
builder.Services.AddScoped<IObtenerTiposUC, ObtenerTiposUC>();
builder.Services.AddScoped<IObtenerTipoPorNombreUC, ObtenerTipoPorNombreUC>();

//Usuarios
builder.Services.AddScoped<ILoginUC, LoginUC>();
builder.Services.AddScoped<IObtenerUsuario, ObtenerUsuarioUC>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization(opciones =>
{
    opciones.DefaultPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


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