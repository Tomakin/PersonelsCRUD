using Microsoft.EntityFrameworkCore;
using Project.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Data.Concrete.DAL
{
    public class EFProjectContext : DbContext
    {
        public EFProjectContext(DbContextOptions<EFProjectContext> options)
            : base(options) { }


    public DbSet<Personel> Personels { get; set; }
    }

}
