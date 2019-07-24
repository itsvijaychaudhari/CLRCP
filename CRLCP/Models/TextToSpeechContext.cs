using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRLCP.Models
{
    public partial class TextToSpeechContext : DbContext
    {
        public TextToSpeechContext()
        {
        }

        public TextToSpeechContext(DbContextOptions<TextToSpeechContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TextSpeech> TextSpeech { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=10.208.10.142;Initial Catalog=TextToSpeech;Persist Security Info=True;User ID=sa;Password=sa@Admin");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<TextSpeech>(entity =>
            {
                entity.HasKey(e => e.AutoId)
                    .HasName("PK_Text_Speech");

                entity.Property(e => e.AutoId).HasColumnName("AUTO_ID");

                entity.Property(e => e.AddedOn)
                    .HasColumnName("ADDED_ON")
                    .HasColumnType("datetime");

                entity.Property(e => e.Age).HasColumnName("AGE");

                entity.Property(e => e.DataId).HasColumnName("DATA_ID");

                entity.Property(e => e.DatasetId).HasColumnName("DATASET_ID");

                entity.Property(e => e.DomainId).HasColumnName("DOMAIN_ID");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("GENDER")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.IsAddedInDataset).HasColumnName("IS_ADDED_IN_DATASET");

                entity.Property(e => e.IsValid).HasColumnName("IS_VALID");

                entity.Property(e => e.LangId).HasColumnName("LANG_ID");

                entity.Property(e => e.OutputData)
                    .IsRequired()
                    .HasColumnName("OUTPUT_DATA");

                entity.Property(e => e.TotalValidationUsersCount).HasColumnName("TOTAL_VALIDATION_USERS_COUNT");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.VoteCount).HasColumnName("VOTE_COUNT");
            });
        }
    }
}
