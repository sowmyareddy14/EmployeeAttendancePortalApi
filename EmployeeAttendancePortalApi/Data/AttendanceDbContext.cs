using EmployeeAttendancePortalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAttendancePortalApi.Data
{
    public class AttendanceDbContext : DbContext
    {
        public AttendanceDbContext(DbContextOptions<AttendanceDbContext> options) : base(options)
        {
        }

        public DbSet<EmployeeAttendance> EmployeeAttendances => Set<EmployeeAttendance>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Map DateOnly to SQL date
            modelBuilder.Entity<EmployeeAttendance>().Property(e => e.Date)
                .HasConversion(
                    d => d.ToDateTime(System.TimeOnly.MinValue),
                    dt => DateOnly.FromDateTime(dt))
                .HasColumnType("date")
                .HasColumnName("AttendanceDate");

        }
    }
}
