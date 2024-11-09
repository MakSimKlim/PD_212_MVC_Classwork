using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PD_212_MVC_Classwork.Models;

namespace PD_212_MVC_Data
{
    public class AcademyContext : DbContext
    {
        public AcademyContext (DbContextOptions<AcademyContext> options)
            : base(options)
        {
        }

        public DbSet<PD_212_MVC_Classwork.Models.Teacher> Teachers { get; set; } = default!;
        public DbSet<PD_212_MVC_Classwork.Models.Student> Students { get; set; } = default!;
        public DbSet<PD_212_MVC_Classwork.Models.Group> Groups { get; set; } = default!;
    }
}
