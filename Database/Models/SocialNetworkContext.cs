using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Database.Models
{
    public partial class SocialNetworkContext : IdentityDbContext
    {
        public SocialNetworkContext()
        {
        }

        public SocialNetworkContext(DbContextOptions<SocialNetworkContext> options)
            : base(options)
        {
        }

        public virtual DbSet<SubTablaComentarios> SubTablaComentarios { get; set; }
        public virtual DbSet<TablaAmigo> TablaAmigo { get; set; }
        public virtual DbSet<TablaComentarios> TablaComentarios { get; set; }
        public virtual DbSet<TablaPublicaciones> TablaPublicaciones { get; set; }
        public virtual DbSet<TablaUsuario> TablaUsuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-DK0MCFF;Database=SocialNetwork;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
                
            modelBuilder.Entity<SubTablaComentarios>(entity =>
            {
                entity.HasKey(e => e.IdSubComentario)
                    .HasName("pk_subTablaComentarios");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserComentario)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdComentarioNavigation)
                    .WithMany(p => p.SubTablaComentarios)
                    .HasForeignKey(d => d.IdComentario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TablaPublicaciones_subTablaComentarios");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.SubTablaComentarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TablaUsuario_subTablaComentarios");
            });

            modelBuilder.Entity<TablaAmigo>(entity =>
            {
                entity.HasKey(e => new { e.UserIdUsuario, e.FriendsIdUsuario })
                    .HasName("PK__TablaAmi__3FAD006678165C8C");

                entity.Property(e => e.UserIdUsuario).HasColumnName("User_IdUsuario");

                entity.Property(e => e.FriendsIdUsuario).HasColumnName("Friends_IdUsuario");

                entity.HasOne(d => d.FriendsIdUsuarioNavigation)
                    .WithMany(p => p.TablaAmigoFriendsIdUsuarioNavigation)
                    .HasForeignKey(d => d.FriendsIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TablaAmig__Frien__3B75D760");

                entity.HasOne(d => d.UserIdUsuarioNavigation)
                    .WithMany(p => p.TablaAmigoUserIdUsuarioNavigation)
                    .HasForeignKey(d => d.UserIdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TablaAmig__User___3A81B327");
            });

            modelBuilder.Entity<TablaComentarios>(entity =>
            {
                entity.HasKey(e => e.IdComentario)
                    .HasName("pk_comentario");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserComentario)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPublicacionNavigation)
                    .WithMany(p => p.TablaComentarios)
                    .HasForeignKey(d => d.IdPublicacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TablaPublicaciones_TablaComentarios");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TablaComentarios)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TablaUsuario_TablaComentarios");
            });

            modelBuilder.Entity<TablaPublicaciones>(entity =>
            {
                entity.HasKey(e => e.IdPublicacion)
                    .HasName("pk_publicacion");

                entity.Property(e => e.Cuerpo)
                    .IsRequired()
                    .HasMaxLength(1500)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FotoPublicacion)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(80)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TablaPublicaciones)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_usuario_publicacion");
            });

            modelBuilder.Entity<TablaUsuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("pk_usuario");

                entity.Property(e => e.Apellido)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Inactivo')");

                entity.Property(e => e.FotoPerfil)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(14)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
