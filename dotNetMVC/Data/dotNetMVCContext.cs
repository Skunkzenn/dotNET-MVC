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

        public DbSet<dotNetMVC.Models.Department> Department { get; set; }
    }
}
