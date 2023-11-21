using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyVaccinesWeb.Services.AdminsService;

namespace MyVaccinesWeb.Models;

public partial class ProceduresContext : DbContext
{
    public ProceduresContext()
    {
    }

    public ProceduresContext(DbContextOptions<ProceduresContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<KeyWord> KeyWords { get; set; }

    public virtual DbSet<MyPatient> MyPatients { get; set; }

    public virtual DbSet<MyProcedure> MyProcedures { get; set; }

    public virtual DbSet<PatientsType> PatientsTypes { get; set; }

    public virtual DbSet<ProceduresDone> ProceduresDones { get; set; }

    public virtual DbSet<ProceduresKeyWord> ProceduresKeyWords { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vaccine> Vaccines { get; set; }

    public virtual DbSet<VaccinesMaker> VaccinesMakers { get; set; }

    public virtual DbSet<VaccinesType> VaccinesTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.\\DEV;Database=Procedures;Trusted_Connection=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_admins");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<KeyWord>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Word)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<MyPatient>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Patronymic)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Surname)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Type).WithMany(p => p.MyPatients)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_MyPatients_PatientsTypes");
        });

        modelBuilder.Entity<MyProcedure>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date).HasColumnType("date");

            entity.HasOne(d => d.Patient).WithMany(p => p.MyProcedures)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_MyProcedures_MyPatients");

            entity.HasOne(d => d.User).WithMany(p => p.MyProcedures)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_MyProcedures_Users");

            entity.HasOne(d => d.Vaccine).WithMany(p => p.MyProcedures)
                .HasForeignKey(d => d.VaccineId)
                .HasConstraintName("FK_MyProcedures_Vaccines");
        });

        modelBuilder.Entity<PatientsType>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<ProceduresDone>(entity =>
        {
            entity.ToTable("ProceduresDone");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActualDate).HasColumnType("date");

            entity.HasOne(d => d.Procedure).WithMany(p => p.ProceduresDones)
                .HasForeignKey(d => d.ProcedureId)
                .HasConstraintName("FK_ProceduresDone_MyProcedures");
        });

        modelBuilder.Entity<ProceduresKeyWord>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasOne(d => d.KeyWord).WithMany(p => p.ProceduresKeyWords)
                .HasForeignKey(d => d.KeyWordId)
                .HasConstraintName("FK_ProceduresKeyWords_KeyWords");

            entity.HasOne(d => d.Procedure).WithMany(p => p.ProceduresKeyWords)
                .HasForeignKey(d => d.ProcedureId)
                .HasConstraintName("FK_ProceduresKeyWords_MyProcedures");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        modelBuilder.Entity<Vaccine>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasOne(d => d.Maker).WithMany(p => p.Vaccines)
                .HasForeignKey(d => d.MakerId)
                .HasConstraintName("FK_Vaccines_VaccinesMakers");

            entity.HasOne(d => d.Type).WithMany(p => p.Vaccines)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Vaccines_VaccinesTypes");
        });

        modelBuilder.Entity<VaccinesMaker>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");

            entity.HasOne(d => d.Country).WithMany(p => p.VaccinesMakers)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_VaccinesMakers_Countries");
        });

        modelBuilder.Entity<VaccinesType>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
