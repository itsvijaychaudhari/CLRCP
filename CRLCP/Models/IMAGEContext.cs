using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRLCP.Models
{
    public partial class IMAGEContext : DbContext
    {
        public IMAGEContext()
        {
        }

        public IMAGEContext(DbContextOptions<IMAGEContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Images> Images { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=10.208.10.142;Initial Catalog=IMAGE;Persist Security Info=True;User ID=sa;Password=sa@Admin");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Images>(entity =>
            {
                entity.HasKey(e => e.DataId)
                    .HasName("PK_GENERAL_IMAGE");

                entity.Property(e => e.DataId).HasColumnName("DATA_ID");

                entity.Property(e => e.AddedOn)
                    .HasColumnName("ADDED_ON")
                    .HasColumnType("datetime");

                entity.Property(e => e.AdditionalInfo).HasColumnName("ADDITIONAL_INFO");

                entity.Property(e => e.DatasetId).HasColumnName("DATASET_ID");

                entity.Property(e => e.DomainId).HasColumnName("DOMAIN_ID");

                entity.Property(e => e.Image)
                    .HasColumnName("IMAGE")
                    .HasColumnType("image");

                entity.Property(e => e.LangId).HasColumnName("LANG_ID");

                entity.Property(e => e.SourceId).HasColumnName("SOURCE_ID");
            });
        }
    }
}
