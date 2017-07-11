using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Maynuocnong.Models.Mapping
{
    public class tblCapacityToGroupProductMap : EntityTypeConfiguration<tblCapacityToGroupProduct>
    {
        public tblCapacityToGroupProductMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            // Table & Column Mappings
            this.ToTable("tblCapacityToGroupProduct");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.idCap).HasColumnName("idCap");
            this.Property(t => t.idCate).HasColumnName("idCate");
        }
    }
}
