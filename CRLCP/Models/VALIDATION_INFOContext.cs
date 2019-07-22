using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRLCP.Models
{
    public partial class VALIDATION_INFOContext : DbContext
    {
        public VALIDATION_INFOContext()
        {
        }

        public VALIDATION_INFOContext(DbContextOptions<VALIDATION_INFOContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ValidationResponseDetail> ValidationResponseDetail { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=10.208.10.142;Initial Catalog=VALIDATION_INFO;User ID=sa;Password=sa@Admin");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ValidationResponseDetail>(entity =>
            {
                entity.HasKey(e => e.AutoId);

                entity.ToTable("VALIDATION_RESPONSE_DETAIL");

                entity.Property(e => e.AutoId).HasColumnName("AUTO_ID");

                entity.Property(e => e.RefAutoid).HasColumnName("REF_AUTOID");

                entity.Property(e => e.SubcategoryId).HasColumnName("SUBCATEGORY_ID");

                entity.Property(e => e.ValidationDetail).HasColumnName("VALIDATION_DETAIL");

                entity.Property(e => e.ValidationFlag).HasColumnName("VALIDATION_FLAG");
            });
        }
    }
}
