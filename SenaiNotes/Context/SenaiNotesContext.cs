using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SenaiNotes.Models;

namespace SenaiNotes.Context;

public partial class SenaiNotesContext : DbContext
{
    public SenaiNotesContext()
    {
    }

    public SenaiNotesContext(DbContextOptions<SenaiNotesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Lembrete> Lembretes { get; set; }

    public virtual DbSet<Lixeira> Lixeiras { get; set; }

    public virtual DbSet<Nota> Notas { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-EUMC23F\\SQLEXPRESS;Initial Catalog=SenaiNotes;User Id=sa;Password=Senai@134;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__categori__DB875A4F96CA6C28");

            entity.ToTable("categorias");

            entity.HasIndex(e => e.Nome, "UQ__categori__6F71C0DCE4465D89").IsUnique();

            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Lembrete>(entity =>
        {
            entity.HasKey(e => e.LembreteId).HasName("PK__lembrete__725CCFFDF0611EBF");

            entity.ToTable("lembretes");

            entity.Property(e => e.LembreteId).HasColumnName("lembrete_id");
            entity.Property(e => e.Ativo)
                .HasDefaultValue(true)
                .HasColumnName("ativo");
            entity.Property(e => e.DataLembrete)
                .HasColumnType("datetime")
                .HasColumnName("data_lembrete");
            entity.Property(e => e.Descricao)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descricao");
            entity.Property(e => e.NotaId).HasColumnName("nota_id");

            entity.HasOne(d => d.Nota).WithMany(p => p.Lembretes)
                .HasForeignKey(d => d.NotaId)
                .HasConstraintName("FK__lembretes__nota___5812160E");
        });

        modelBuilder.Entity<Lixeira>(entity =>
        {
            entity.HasKey(e => e.LixeiraId).HasName("PK__lixeira__F25ADF1A2A9E3235");

            entity.ToTable("lixeira");

            entity.HasIndex(e => e.NotaId, "UQ__lixeira__333C1C4033DDEA1B").IsUnique();

            entity.Property(e => e.LixeiraId).HasColumnName("lixeira_id");
            entity.Property(e => e.DataExclusao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_exclusao");
            entity.Property(e => e.NotaId).HasColumnName("nota_id");

            entity.HasOne(d => d.Nota).WithOne(p => p.Lixeira)
                .HasForeignKey<Lixeira>(d => d.NotaId)
                .HasConstraintName("FK__lixeira__nota_id__6477ECF3");
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.NotaId).HasName("PK__notas__333C1C412E0CD0BE");

            entity.ToTable("notas");

            entity.Property(e => e.NotaId).HasColumnName("nota_id");
            entity.Property(e => e.Arquivada)
                .HasDefaultValue(false)
                .HasColumnName("arquivada");
            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.Conteudo)
                .HasColumnType("text")
                .HasColumnName("conteudo");
            entity.Property(e => e.DataAtualizacao)
                .HasColumnType("datetime")
                .HasColumnName("data_atualizacao");
            entity.Property(e => e.DataCriacao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_criacao");
            entity.Property(e => e.Excluida)
                .HasDefaultValue(false)
                .HasColumnName("excluida");
            entity.Property(e => e.Imagem)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imagem");
            entity.Property(e => e.Titulo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titulo");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Nota)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK__notas__categoria__5535A963");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Nota)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__notas__usuario_i__5165187F");

            entity.HasMany(d => d.Tags).WithMany(p => p.Nota)
                .UsingEntity<Dictionary<string, object>>(
                    "NotaTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__nota_tags__tag_i__5FB337D6"),
                    l => l.HasOne<Nota>().WithMany()
                        .HasForeignKey("NotaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__nota_tags__nota___5EBF139D"),
                    j =>
                    {
                        j.HasKey("NotaId", "TagId").HasName("PK__nota_tag__4715766A9E657C85");
                        j.ToTable("nota_tags");
                        j.IndexerProperty<int>("NotaId").HasColumnName("nota_id");
                        j.IndexerProperty<int>("TagId").HasColumnName("tag_id");
                    });
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__tags__4296A2B665C637CF");

            entity.ToTable("tags");

            entity.HasIndex(e => e.Nome, "UQ__tags__6F71C0DC6EFC6F3D").IsUnique();

            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__usuarios__2ED7D2AF9C47F258");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Email, "UQ__usuarios__AB6E616457E7EF92").IsUnique();

            entity.HasIndex(e => e.NomeUsuario, "UQ__usuarios__CCB80B0A17F097A3").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.DataCriacao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("data_criacao");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.NomeUsuario)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome_usuario");
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("senha");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
