using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace CodeProject.Framework.ReferenceData.EF
{
    public class ContentStaticDataDbContext : DbContext
    {
        public DbSet<Chapter>          Chapters           { get; set; }
        public DbSet<Section>          Sections           { get; set; }
        public DbSet<Subsection>       Subsections        { get; set; }
        public DbSet<PublishingStatus> PublishingStatuses { get; set; }
        public DbSet<Tag>              Tags               { get; set; }
        public DbSet<License>          Licenses           { get; set; }

        public ContentStaticDataDbContext(DbContextOptions<ContentStaticDataDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subsection>(b =>
            {
                b.Table("TaxonomySubSection");
                b.Key(ss => ss.Id);
                b.Property(ss => ss.Id).Column("TaxonomySubSectionId");
                b.Property(ss => ss.SectionId).Column("TaxonomySectionId");
            });

            modelBuilder.Entity<Section>(b =>
            {
                b.Table("TaxonomySection");
                b.Key(sect => sect.Id);
                b.Property(sect => sect.Id).Column("TaxonomySectionId");
                b.Property(sect => sect.ChapterId).Column("TaxonomyChapterId");
            });

            modelBuilder.Entity<Chapter>(b =>
            {
                b.Table("TaxonomyChapter");
                b.Key(chapt => chapt.Id);
                b.Property(chapt => chapt.Id).Column("TaxonomyChapterId");
            });

            modelBuilder.Entity<PublishingStatus>(b =>
            {
                b.Table("PublishingStatus");
                b.Key(ps => ps.Id);
                b.Property(ps => ps.Id).Column("PublishingStatusId");
            });

            modelBuilder.Entity<Tag>(b =>
            {
                b.Table("Attribute");
                b.Key(tag => tag.Id);
                b.Property(tag => tag.Id).Column("AttributeId");
                b.Property(tag => tag.Name).Column("AttributeName");
            });

            modelBuilder.Entity<License>(b =>
            {
                b.Table("License");
                b.Key(license => license.Id);
                b.Property(license => license.Id).Column("LicenseId");
                // misspelling in database
                b.Property(license => license.PatentLicense).Column("PatentLicence");
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}