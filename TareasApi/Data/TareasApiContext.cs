using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TareasApi.Models;

namespace TareasApi.Data
{
    public class TareasApiContext : DbContext
    {
        public TareasApiContext (DbContextOptions<TareasApiContext> options)
            : base(options)
        {
        }

        public DbSet<TareasApi.Models.Tarea> Tarea { get; set; } = default!;
    }
}
