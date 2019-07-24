using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CRLCP.Models
{
    public partial class CLRCP_MASTERContext : DbContext
    {
        public CLRCP_MASTERContext()
        {
        }

        public CLRCP_MASTERContext(DbContextOptions<CLRCP_MASTERContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<DatasetSubcategoryMapping> DatasetSubcategoryMapping { get; set; }
        public virtual DbSet<DatasetSubcategoryMappingValidation> DatasetSubcategoryMappingValidation { get; set; }
        public virtual DbSet<Datasets> Datasets { get; set; }
        public virtual DbSet<DomainIdMapping> DomainIdMapping { get; set; }
        public virtual DbSet<LanguageIdMapping> LanguageIdMapping { get; set; }
        public virtual DbSet<LoginDetails> LoginDetails { get; set; }
        public virtual DbSet<QualificationIdMapping> QualificationIdMapping { get; set; }
        public virtual DbSet<SourceIdMapping> SourceIdMapping { get; set; }
        public virtual DbSet<SubCategories> SubCategories { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<UserLanguageMapping> UserLanguageMapping { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=10.208.10.142;Initial Catalog=CLRCP_MASTER;Persist Security Info=True;User ID=sa;Password=sa@Admin");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("CATEGORIES");

                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<DatasetSubcategoryMapping>(entity =>
            {
                entity.HasKey(e => e.AutoId);

                entity.ToTable("DATASET_SUBCATEGORY_MAPPING");

                entity.Property(e => e.AutoId).HasColumnName("AUTO_ID");

                entity.Property(e => e.DatasetId).HasColumnName("DATASET_ID");

                entity.Property(e => e.DestinationSubcategoryId).HasColumnName("DESTINATION_SUBCATEGORY_ID");

                entity.Property(e => e.DestinationSubcategoryId2).HasColumnName("DESTINATION_SUBCATEGORY_ID2");

                entity.Property(e => e.DestinationSubcategoryId3).HasColumnName("DESTINATION_SUBCATEGORY_ID3");

                entity.Property(e => e.SourceSubcategoryId).HasColumnName("SOURCE_SUBCATEGORY_ID");

                entity.Property(e => e.SourceSubcategoryId2).HasColumnName("SOURCE_SUBCATEGORY_ID2");

                entity.Property(e => e.SourceSubcategoryId3).HasColumnName("SOURCE_SUBCATEGORY_ID3");
            });

            modelBuilder.Entity<DatasetSubcategoryMappingValidation>(entity =>
            {
                entity.HasKey(e => e.AutoId);

                entity.ToTable("DATASET_SUBCATEGORY_MAPPING_VALIDATION");

                entity.Property(e => e.AutoId).HasColumnName("AUTO_ID");

                entity.Property(e => e.DatasetId).HasColumnName("DATASET_ID");

                entity.Property(e => e.DestinationSubcategoryId).HasColumnName("DESTINATION_SUBCATEGORY_ID");
            });

            modelBuilder.Entity<Datasets>(entity =>
            {
                entity.HasKey(e => e.DatasetId);

                entity.ToTable("DATASETS");

                entity.Property(e => e.DatasetId).HasColumnName("DATASET_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(250);

                entity.Property(e => e.IsVisible).HasColumnName("IS_VISIBLE");

                entity.Property(e => e.MaxCollectionUsers).HasColumnName("MAX_COLLECTION_USERS");

                entity.Property(e => e.MaxValidationUsers).HasColumnName("MAX_VALIDATION_USERS");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DomainIdMapping>(entity =>
            {
                entity.HasKey(e => e.DomainId);

                entity.ToTable("DOMAIN_ID_MAPPING");

                entity.Property(e => e.DomainId).HasColumnName("DOMAIN_ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("VALUE")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<LanguageIdMapping>(entity =>
            {
                entity.HasKey(e => e.LanguageId);

                entity.ToTable("LANGUAGE_ID_MAPPING");

                entity.Property(e => e.LanguageId).HasColumnName("LANGUAGE_ID");

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(50);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("VALUE")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LoginDetails>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_RegisterTable");

                entity.ToTable("LOGIN_DETAILS");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");

                entity.Property(e => e.EmailId)
                    .IsRequired()
                    .HasColumnName("EMAIL_ID")
                    .HasMaxLength(100);

                entity.Property(e => e.MobileNo)
                    .IsRequired()
                    .HasColumnName("MOBILE_NO")
                    .HasMaxLength(15);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(20);

                entity.Property(e => e.UserType).HasColumnName("USER_TYPE");
            });

            modelBuilder.Entity<QualificationIdMapping>(entity =>
            {
                entity.HasKey(e => e.QualificationId);

                entity.ToTable("QUALIFICATION_ID_MAPPING");

                entity.Property(e => e.QualificationId).HasColumnName("QUALIFICATION_ID");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("VALUE")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<SourceIdMapping>(entity =>
            {
                entity.HasKey(e => e.SourceId);

                entity.ToTable("SOURCE_ID_MAPPING");

                entity.Property(e => e.SourceId).HasColumnName("SOURCE_ID");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("VALUE")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<SubCategories>(entity =>
            {
                entity.HasKey(e => e.SubcategoryId);

                entity.ToTable("SUB_CATEGORIES");

                entity.Property(e => e.SubcategoryId).HasColumnName("SUBCATEGORY_ID");

                entity.Property(e => e.CategoryId).HasColumnName("CATEGORY_ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("DESCRIPTION")
                    .HasMaxLength(250);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.ToTable("USER_INFO");

                entity.Property(e => e.UserId)
                    .HasColumnName("USER_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Age).HasColumnName("AGE");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("GENDER")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.LangId1).HasColumnName("LANG_ID_1");

                entity.Property(e => e.LangId2).HasColumnName("LANG_ID_2");

                entity.Property(e => e.LangId3).HasColumnName("LANG_ID_3");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("NAME")
                    .HasMaxLength(250);

                entity.Property(e => e.QualificationId).HasColumnName("QUALIFICATION_ID");
            });

            modelBuilder.Entity<UserLanguageMapping>(entity =>
            {
                entity.HasKey(e => e.AutoId);

                entity.ToTable("USER_LANGUAGE_MAPPING");

                entity.Property(e => e.AutoId).HasColumnName("AUTO_ID");

                entity.Property(e => e.LanguageId).HasColumnName("LANGUAGE_ID");

                entity.Property(e => e.UserId).HasColumnName("USER_ID");
            });
        }
    }
}
