

using BackEnd.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Infrastructure.DataBase.ConfigShema;


public class ItemConfig : IEntityTypeConfiguration<ItemsTrabajoEntity>
{
    public void Configure(EntityTypeBuilder<ItemsTrabajoEntity> builder)
    {
        builder.ToTable("items_trabajo");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Titulo)
            .HasColumnName("titulo")
            .HasMaxLength(200);

        builder.Property(x => x.Descripcion)
            .HasColumnName("descripcion")
            .HasMaxLength(500);

        builder.Property(x => x.FechaEntrega)
            .HasColumnName("fecha_entrega");

        builder.Property(x => x.Relevancia)
            .HasColumnName("relevancia")
            .HasConversion<string>();

        builder.Property(x => x.Estado)
            .HasColumnName("estado")
            .HasConversion<string>();

        builder.Property(x => x.UsuarioId)
            .HasColumnName("usuario_id");

        builder.HasOne(x => x.Usuario)
            .WithMany(x => x.ItemsTrabajo)
            .HasForeignKey(x => x.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}