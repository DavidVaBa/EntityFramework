using DemoMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMVC.Context
{
    public class EFDemoContext : DbContext
    {
        public EFDemoContext(DbContextOptions<EFDemoContext> options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
    }
}
