using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ICabañasUC;
using Obligatorio.BussinesLogic.CasosDeUso.CabañasUC;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.IMantenimientosUC;
using Obligatorio.BussinesLogic.CasosDeUso.MantenimientosUC;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ITiposUC;
using Obligatorio.BussinesLogic.CasosDeUso.TiposUC;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.IUsuarioUC;
using Obligatorio.BussinesLogic.CasosDeUso.Usuario;
using Obligatorio.BussinesLogic.InterfacesCasosDeUso.ISistemaUC.ICabañaUCSistema;
using Obligatorio.BussinesLogic.CasosDeUso.SistemaUC.CabañaUCSistema;
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

//Cabaña
builder.Services.AddScoped<IObtenerCabañaPorNombreUC, ObtenerCabañaPorNombreUC>();
builder.Services.AddScoped<IEliminarCabañaUC, EliminarCabañaUC>();
builder.Services.AddScoped<IObtenerCabañasHabilitadasUC, ObtenerCabañasHabilitadasUC>();
builder.Services.AddScoped<IObtenerCabañasPorCapacidadUC, ObtenerCabañasPorCapacidadUC>();
builder.Services.AddScoped<IObtenerCabañasPorTipoUC, ObtenerCabañasPorTipoUC>();
builder.Services.AddScoped<IObtenerCabañasUC, ObtenerCabañasUC>();
builder.Services.AddScoped<IRegistrarCabañaUC, RegistrarCabañaUC>();

builder.Services.AddScoped<IObtenerCabañaPorId, ObtenerCabañaPorId>();

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
