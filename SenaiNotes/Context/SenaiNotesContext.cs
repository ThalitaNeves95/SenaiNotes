using System;
using System.Collections.Generic;
using APISenaiNotes.Models;
using Microsoft.EntityFrameworkCore;

namespace APISenaiNotes.Context;

public partial class SenaiNotesContext : DbContext
{
    public SenaiNotesContext(DbContextOptions<SenaiNotesContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Nota> Notas { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:senainotestakoyakis.database.windows.net,1433;Initial Catalog=SenaiNotes;Persist Security Info=False;User ID=backend;Password=senai@134;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditoriaGeral>(entity =>
        {
            entity.HasKey(e => e.IdAuditoria).HasName("PK__Auditori__7FD13FA0301700A8");

            entity.ToTable("AuditoriaGeral");

            entity.Property(e => e.DataAcao).HasColumnType("datetime");
            entity.Property(e => e.NomeTabela)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TipoAcao)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__categori__DB875A4F3098E312");

            entity.ToTable("categorias", tb => tb.HasTrigger("trg_audit_categorias"));

            entity.HasIndex(e => e.Nome, "UQ__categori__6F71C0DC9394E750").IsUnique();

            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Lembrete>(entity =>
        {
            entity.HasKey(e => e.LembreteId).HasName("PK__lembrete__725CCFFD1F375035");

            entity.ToTable("lembretes", tb => tb.HasTrigger("trg_audit_lembretes"));

            entity.Property(e => e.LembreteId).HasColumnName("lembrete_id");
            entity.Property(e => e.Ativo).HasColumnName("ativo");
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
                .HasConstraintName("FK__lembretes__nota___6B24EA82");
        });

        modelBuilder.Entity<Lixeira>(entity =>
        {
            entity.HasKey(e => e.LixeiraId).HasName("PK__lixeira__F25ADF1ACB278127");

            entity.ToTable("lixeira", tb => tb.HasTrigger("trg_audit_lixeira"));

            entity.HasIndex(e => e.NotaId, "UQ__lixeira__333C1C4048E1F615").IsUnique();

            entity.Property(e => e.LixeiraId).HasColumnName("lixeira_id");
            entity.Property(e => e.DataExclusao)
                .HasColumnType("datetime")
                .HasColumnName("data_exclusao");
            entity.Property(e => e.NotaId).HasColumnName("nota_id");

            entity.HasOne(d => d.Nota).WithOne(p => p.Lixeira)
                .HasForeignKey<Lixeira>(d => d.NotaId)
                .HasConstraintName("FK__lixeira__nota_id__778AC167");
        });

        modelBuilder.Entity<Nota>(entity =>
        {
            entity.HasKey(e => e.NotaId).HasName("PK__notas__333C1C41C0012B84");

            entity.ToTable("notas", tb => tb.HasTrigger("trg_audit_notas"));

            entity.Property(e => e.NotaId).HasColumnName("nota_id");
            entity.Property(e => e.Arquivada).HasColumnName("arquivada");
            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.Conteudo).HasColumnName("conteudo");
            entity.Property(e => e.DataAtualizacao)
                .HasColumnType("datetime")
                .HasColumnName("data_atualizacao");
            entity.Property(e => e.DataCriacao)
                .HasColumnType("datetime")
                .HasColumnName("data_criacao");
            entity.Property(e => e.Excluida).HasColumnName("excluida");
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
                .HasConstraintName("FK__notas__categoria__68487DD7");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Nota)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__notas__usuario_i__6477ECF3");

            entity.HasMany(d => d.Tags).WithMany(p => p.Nota)
                .UsingEntity<Dictionary<string, object>>(
                    "NotaTag",
                    r => r.HasOne<Tag>().WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__nota_tags__tag_i__72C60C4A"),
                    l => l.HasOne<Nota>().WithMany()
                        .HasForeignKey("NotaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__nota_tags__nota___71D1E811"),
                    j =>
                    {
                        j.HasKey("NotaId", "TagId").HasName("PK__nota_tag__4715766AD25FEEEF");
                        j.ToTable("nota_tags", tb => tb.HasTrigger("trg_audit_nota_tags"));
                        j.IndexerProperty<int>("NotaId").HasColumnName("nota_id");
                        j.IndexerProperty<int>("TagId").HasColumnName("tag_id");
                    });
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__tags__4296A2B6481EAB0D");

            entity.ToTable("tags", tb => tb.HasTrigger("trg_audit_tags"));

            entity.HasIndex(e => e.Nome, "UQ__tags__6F71C0DCE3E4D525").IsUnique();

            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__usuarios__2ED7D2AF40D4D382");

            entity.ToTable("usuarios", tb => tb.HasTrigger("trg_audit_usuarios"));

            entity.HasIndex(e => e.Email, "UQ__usuarios__AB6E61640C351AE6").IsUnique();

            entity.HasIndex(e => e.NomeUsuario, "UQ__usuarios__CCB80B0A67C605AE").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");
            entity.Property(e => e.DataCriacao)
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
