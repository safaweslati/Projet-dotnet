using System;
using System.Collections.Generic;
using Insat.Models;
using Microsoft.EntityFrameworkCore;

namespace Insat.Data;

public partial class ClubsInsatContext : DbContext
{
    public ClubsInsatContext()
    {
    }

    public ClubsInsatContext(DbContextOptions<ClubsInsatContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=C:\\Users\\safaw\\source\\repos\\Insat\\ClubsInsat.db;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Clubs_Id").IsUnique();

            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasMany(d => d.Studs).WithMany(p => p.Clubs)
                .UsingEntity<Dictionary<string, object>>(
                    "StudClub",
                    r => r.HasOne<Student>().WithMany()
                        .HasForeignKey("Stud")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Club>().WithMany()
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("ClubId", "Stud");
                        j.ToTable("StudClub");
                    });
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasIndex(e => e.Id, "IX_Events_Id").IsUnique();

            entity.Property(e => e.Img).HasColumnName("img");

            entity.HasOne(d => d.Club).WithMany(p => p.Events).HasForeignKey(d => d.ClubId);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Ninscri);

            entity.Property(e => e.Ninscri)
                .ValueGeneratedNever()
                .HasColumnName("NInscri");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasColumnName("last_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
