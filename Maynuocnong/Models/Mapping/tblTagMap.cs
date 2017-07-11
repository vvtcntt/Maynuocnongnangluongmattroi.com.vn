using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Maynuocnong.Models.Mapping
{
    public class tblTagMap : EntityTypeConfiguration<tblTag>
    {
        public tblTagMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(300);

            this.Property(t => t.Description)
                .HasMaxLength(500);

            this.Property(t => t.Tag)
                .HasMaxLength(300);

            this.Property(t => t.Keyword)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("tblTags");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.Tag).HasColumnName("Tag");
            this.Property(t => t.Keyword).HasColumnName("Keyword");
            this.Property(t => t.Ord).HasColumnName("Ord");
            this.Property(t => t.Active).HasColumnName("Active");
            this.Property(t => t.DateCreate).HasColumnName("DateCreate");
            this.Property(t => t.idUser).HasColumnName("idUser");
        }
    }
}
