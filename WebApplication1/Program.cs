using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ICaba�asUC;
using Obligatorio.BussinesLogic.CasosDeUso.Caba�asUC;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.IMantenimientosUC;
using Obligatorio.BussinesLogic.CasosDeUso.MantenimientosUC;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ITiposUC;
using Obligatorio.BussinesLogic.CasosDeUso.TiposUC;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.IUsuarioUC;
using Obligatorio.BussinesLogic.CasosDeUso.Usuario;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ISistemaUC.ICaba�aUCSistema;
using Obligatorio.BussinesLogic.CasosDeUso.SistemaUC.Caba�aUCSistema;
using Library.AccesData.Memory;
using Library.BusinessLogic.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

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

builder.Services.AddScoped<IObtenerCaba�aPorId, ObtenerCaba�aPorId>();

//Mantenimientos
builder.Services.AddScoped<IAgregarMantenimientoUC, AgregarMantenimientoUC>();
builder.Services.AddScoped<IObtenerMantenimientosEntreFechasUC, ObtenerMantenimientoEntreFechasUC>();

//Tipos
builder.Services.AddScoped<IAgregarTipoUC, AgregarTipoUC>();
builder.Services.AddScoped<IEliminarTipoUC, EliminarTipoUC>();
builder.Services.AddScoped<IModificarTipoUC, ModificarTipoUC>();
builder.Services.AddScoped<IObtenerTiposUC, ObtenerTiposUC>();
builder.Services.AddScoped< IObtenerTipoPorNombreUC, ObtenerTipoPorNombreUC > ();

//Usuarios
builder.Services.AddScoped<ILoginUC, LoginUC>();

builder.Services.AddSession();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
