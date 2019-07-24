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

        public virtual DbSet<TextspeechValidationResponseDetail> TextspeechValidationResponseDetail { get; set; }

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

            modelBuilder.Entity<TextspeechValidationResponseDetail>(entity =>
            {
                entity.HasKey(e => e.AutoId)
                    .HasName("PK_VALIDATION_RESPONSE_DETAIL");

                entity.ToTable("TEXTSPEECH_VALIDATION_RESPONSE_DETAIL");

                entity.Property(e => e.AutoId).HasColumnName("AUTO_ID");

                entity.Property(e => e.IsClear).HasColumnName("IS_CLEAR");

                entity.Property(e => e.IsMatch).HasColumnName("IS_MATCH");

                entity.Property(e => e.NoCrossTalk).HasColumnName("NO_CROSS_TALK");

                entity.Property(e => e.RefAutoid).HasColumnName("REF_AUTOID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.ValidationFlag).HasColumnName("VALIDATION_FLAG");
            });
        }
    }
}
