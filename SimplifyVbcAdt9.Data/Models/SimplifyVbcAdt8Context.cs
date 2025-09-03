using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace SimplifyVbcAdt9.Data.Models;

public partial class SimplifyVbcAdt8Context : DbContext
{
    public SimplifyVbcAdt8Context(DbContextOptions<SimplifyVbcAdt8Context> options)
        : base(options)
    {
        string projectPath = AppDomain.CurrentDomain.BaseDirectory;
        IConfigurationRoot configuration =
            new ConfigurationBuilder()
                .SetBasePath(projectPath)
        .AddJsonFile(MyConstants.AppSettingsFile)
        .Build();
        Database.SetCommandTimeout(9000);
        MyConnectionString =
            configuration.GetConnectionString(MyConstants.ConnectionString);
    }

    public string MyConnectionString { get; set; }

    public virtual DbSet<HumanaCensusMaster> HumanaCensusMasters { get; set; }
    public virtual DbSet<qy_GetHumanaCensusConfigOutputColumns> qy_GetHumanaCensusConfigOutputColumnsList { get; set; }
    public virtual DbSet<dd_HumanaCensusOutputColumns> dd_TruncateHumanaCensusOutputColumnsList { get; set; }
    public virtual DbSet<di_FinalizeHumanaCensusOutputColumns> di_FinalizeHumanaCensusOutputColumnsList { get; set; }
    public virtual DbSet<qy_GetHumanaCensusMasterOutputColumns> qy_GetHumanaCensusMasterOutputColumnsList { get; set; }
    public virtual DbSet<qy_GetPointClickCareConfigOutputColumns> qy_GetPointClickCareConfigOutputColumnsList { get; set; }
    public virtual DbSet<dd_PointClickCareOutputColumns> dd_PointClickCareOutputColumnsList { get; set; }
    public virtual DbSet<di_PointClickCareOutputColumns> di_PointClickCareOutputColumnsList { get; set; }
    public virtual DbSet<qy_GetPointClickCareOutputColumns> qy_GetPointClickCareOutputColumnsList { get; set; }
    public virtual DbSet<dd_EthinOutputColumns> dd_EthinOutputColumnsList { get; set; }
    public virtual DbSet<qy_GetEthinConfigOutputColumns> qy_GetEthinConfigOutputColumnsList { get; set; }

    public virtual DbSet<di_EthinOutputColumns> di_EthinOutputColumnsList { get; set; }
    public virtual DbSet<qy_GetEthinOutputColumns> qy_GetEthinOutputColumnsList { get; set; }

    public virtual DbSet<qy_GetHumanaConfigOutputColumns> qy_GetHumanaConfigOutputColumnsList { get; set; }
    public virtual DbSet<dd_HumanaOutputColumns> dd_HumanaOutputColumnsList { get; set; }
    public virtual DbSet<di_HumanaOutputColumns> di_HumanaOutputColumnsList { get; set; }

    public virtual DbSet<qy_GetHumanaOutputColumns> qy_GetHumanaOutputColumnsList { get; set; }

    public virtual DbSet<dd_HumanaObsOutputColumns> dd_HumanaObsOutputColumnsList { get; set; }
    public virtual DbSet<di_HumanaObsOutputColumns> di_HumanaObsOutputColumnsList { get; set; }
    public virtual DbSet<qy_GetHumanaObsOutputColumns> qy_GetHumanaObsOutputColumnsList { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<qy_GetHumanaObsOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<di_HumanaObsOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<dd_HumanaObsOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<qy_GetHumanaOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<di_HumanaOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<dd_HumanaOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<qy_GetHumanaConfigOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<qy_GetEthinOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<di_EthinOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<qy_GetEthinConfigOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<dd_EthinOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<qy_GetPointClickCareOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<di_PointClickCareOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<dd_PointClickCareOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<qy_GetPointClickCareConfigOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<qy_GetHumanaCensusMasterOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<di_FinalizeHumanaCensusOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<dd_HumanaCensusOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<qy_GetHumanaCensusConfigOutputColumns>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<HumanaCensusMaster>(entity =>
        {
            entity.ToTable("HumanaCensusMaster");

            entity.Property(e => e.HumanaCensusMasterId).HasColumnName("HumanaCensusMasterID");
            entity.Property(e => e.AdmitDate).HasColumnType("datetime");
            entity.Property(e => e.AuthStatus).HasMaxLength(300);
            entity.Property(e => e.Category).HasMaxLength(300);
            entity.Property(e => e.DateOfBirth).HasMaxLength(300);
            entity.Property(e => e.DischargeDate).HasColumnType("datetime");
            entity.Property(e => e.Disposition).HasMaxLength(300);
            entity.Property(e => e.Dummy1).HasMaxLength(50);
            entity.Property(e => e.DxDescription).HasMaxLength(300);
            entity.Property(e => e.FacilityOrPractitioner).HasMaxLength(300);
            entity.Property(e => e.NewYorN)
                .HasMaxLength(1)
                .HasColumnName("NewYOrN");
            entity.Property(e => e.PaneledProvider).HasMaxLength(300);
            entity.Property(e => e.PatEligible).HasMaxLength(300);
            entity.Property(e => e.PatEligibleThrough).HasMaxLength(300);
            entity.Property(e => e.PatientName).HasMaxLength(300);
            entity.Property(e => e.Readmit).HasMaxLength(300);
            entity.Property(e => e.ReadmitRisk).HasMaxLength(300);
            entity.Property(e => e.SdohDetails).HasMaxLength(300);
            entity.Property(e => e.SrfDetails).HasMaxLength(300);
            entity.Property(e => e.SrfFlagYorN)
                .HasMaxLength(1)
                .HasColumnName("SrfFlagYOrN");
            entity.Property(e => e.StayType).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
