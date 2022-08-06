using ADProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADProject.Data
{
    public class EmployeesDBContext: DbContext
    {
        public EmployeesDBContext(DbContextOptions<EmployeesDBContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Refreshtoken> refreshtokens { get; set; }
    }
}
