﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OraclePrimavera.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ProjectRecord> ProjectRecords { get; set; }
        public DbSet<ProjectRecordManual> ProjectRecordManual { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Specify that the 'Attachment' property should use 'CLOB' for Oracle database
        }
    }
}