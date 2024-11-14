using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyAuthEmp.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MyAuthEmp.Services
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Salary>Salaries { get; set; }  
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity <Department>().HasKey(d => d.Id);
            modelBuilder.Entity<Employee>()
           .HasOne(e => e.Department)
           .WithOne(d => d.Employee)
           .HasForeignKey<Department>(d => d.EmployeeId);
            modelBuilder.Entity<Salary>().HasKey(e => e.Id);
            modelBuilder.Entity<Employee>()
       .HasMany(e => e.Salaries)
       .WithOne(s => s.Employee)
       .HasForeignKey(s => s.EmployeeId)
       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
