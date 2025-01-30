using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OraclePrimavera.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ProjectRecord> ProjectRecords { get; set; }
        public DbSet<ProjectRecordManual> ProjectRecordManual { get; set; }
        public DbSet<ProjectRecordFile> ProjectRecordFile { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProjectRecord>()
           .Property(e => e.Attachment)
           .HasColumnType("CLOB");

            modelBuilder.Entity<ProjectRecordManual>()
           .Property(e => e.Attachment)
           .HasColumnType("CLOB");

            modelBuilder.Entity<ProjectRecordFile>()
         .Property(e => e.Base64File)
         .HasColumnType("CLOB");
        }
    }
}