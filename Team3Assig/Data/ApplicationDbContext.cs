using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Team3Assig.Models;

namespace Team3Assig.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(new { StudentId = 1, Name = "Name1", Birthdate = new DateTime(2001, 07, 20), EmailAddress = "example@email.com" });
            modelBuilder.Entity<Student>().HasData(new { StudentId = 2, Name = "Name2", Birthdate = new DateTime(1980, 02, 28), EmailAddress = "example2@email.com" });
            modelBuilder.Entity<Student>().HasData(new { StudentId = 3, Name = "Name3", Birthdate = new DateTime(1988, 12, 30), EmailAddress = "example3@email.com" });
            modelBuilder.Entity<Diploma>().HasData(new Diploma { DiplomaId = 1, Thesis = "Computer Science", Abstract = "Big description of the Thesis", Completeness = true, Supervisor = "Borys" });
            modelBuilder.Entity<Diploma>().HasData(new Diploma { DiplomaId = 2, Thesis = "Computer Engineering", Abstract = "Big description of the Thesis", Completeness = true, Supervisor = "Borys" });
            modelBuilder.Entity<Diploma>().HasData(new Diploma { DiplomaId = 3, Thesis = "Computer Architecture", Abstract = "Big description of the Thesis", Completeness = true, Supervisor = "Borys" });
            base.OnModelCreating(modelBuilder);

        }


        public DbSet<Team3Assig.Models.Diploma> Diploma { get; set; }
        public DbSet<Team3Assig.Models.Student> Student { get; set; }
    }
}
