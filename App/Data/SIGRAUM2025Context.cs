using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SIGRAUM2025.Models;

namespace SIGRAUM2025.Data
{
    public class SIGRAUM2025Context : DbContext
    {
        public SIGRAUM2025Context (DbContextOptions<SIGRAUM2025Context> options)
            : base(options)
        {
        }

        public DbSet<SIGRAUM2025.Models.Atleta> Atleta { get; set; } = default!;
        public DbSet<SIGRAUM2025.Models.RECINTO> RECINTO { get; set; } = default!;
        public DbSet<SIGRAUM2025.Models.Facultad> Facultad { get; set; } = default!;
        public DbSet<SIGRAUM2025.Models.ParticipacionEvento> ParticipacionEvento { get; set; } = default!;
    }
}
