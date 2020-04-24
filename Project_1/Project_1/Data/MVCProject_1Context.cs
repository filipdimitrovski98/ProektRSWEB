using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_1.Models;

namespace Project_1.Data
{
    public class MVCProject_1Context : DbContext
    {
        public MVCProject_1Context (DbContextOptions<MVCProject_1Context> options)
            : base(options)
        {
        }

        public DbSet<Course> Course { get; set; }

        public DbSet<Student> Student { get; set; }

        public DbSet<Teacher> Teacher { get; set; }

        public DbSet<Enrollment> Enrollment { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Enrollment>()
            .HasOne<Course>(p => p.Course)
            .WithMany(p => p.Students)
            .HasForeignKey(p => p.CourseId);
            //.HasPrincipalKey(p => p.Id);
            builder.Entity<Enrollment>()
            .HasOne<Student>(p => p.Student)
            .WithMany(p => p.Courses)
            .HasForeignKey(p => p.StudentId);
            //.HasPrincipalKey(p => p.Id);
            //builder.Entity<Course>()
            //.HasOne<Teacher>(p => p.FirstTeacher)
            //.WithMany(p => p.ourses)
            //.HasForeignKey(p => p.FirstTeacherId);
            ////.hasprincipalkey(p => p.id);
        }
    }
}
