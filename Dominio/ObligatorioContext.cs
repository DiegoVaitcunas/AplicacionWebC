using Library.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Library.AccesData
{
    public class ObligatorioContext : DbContext
    {
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Mantenimiento> mantenimientos { get; set; }
        public DbSet<Cabaña> cabañas { get; set; }
        public DbSet<Tipo> tipos { get; set; }
        public DbSet<Configuracion> Configuraciones { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string cadena = @"Server=(localdb)\MSSQLLocalDB;Database=Obligatorio;Integrated Security=true;ENCRYPT=False";
            optionsBuilder.UseSqlServer(cadena).EnableDetailedErrors();
        }
    }
}

