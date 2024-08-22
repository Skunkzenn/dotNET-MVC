using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotNetMVC.Models;

namespace dotNetMVC.Data
{
    public class dotNetMVCContext : DbContext
    {
        public dotNetMVCContext (DbContextOptions<dotNetMVCContext> options)
            : base(options)
        {
        }
        //TER ATENÇÃO E ESSA PARTE, POIS É FUNDAMENTAL PARA A MIGRATION!
        //dotNetMVC.Models não precisamos passar esse argumento pois o nome do namespace já é o dotNetMVC.
        //public DbSet<dotNetMVC.Models.Department> Department { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet <Seller> Seller { get; set; }
        public DbSet <SalesRecord> SalesRecord { get; set; }
    }
}
