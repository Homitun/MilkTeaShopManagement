using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MilkTeaShop.DAL.Models;

public partial class MilkTeaShopManagementContext : DbContext
{
    public MilkTeaShopManagementContext()
    {
    }

    public MilkTeaShopManagementContext(DbContextOptions<MilkTeaShopManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillInfo> BillInfos { get; set; }

    public virtual DbSet<Drink> Drinks { get; set; }

    public virtual DbSet<DrinkCategory> DrinkCategories { get; set; }

    public virtual DbSet<TableDrink> TableDrinks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    } 

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json",true,true)
                .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];
        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__Account__66DCF95D530BB6C6");

            entity.ToTable("Account");

            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("userName");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasColumnName("displayName");
            entity.Property(e => e.PassWord)
                .HasMaxLength(1000)
                .HasColumnName("passWord");
            entity.Property(e => e.Type).HasColumnName("type");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Bill__3213E83F135E75AB");

            entity.ToTable("Bill");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateCheckIn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("dateCheckIn");
            entity.Property(e => e.DateCheckOut).HasColumnName("dateCheckOut");
            entity.Property(e => e.IdTable).HasColumnName("idTable");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.IdTableNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.IdTable)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__status__59063A47");
        });

        modelBuilder.Entity<BillInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BillInfo__3213E83FE08073A4");

            entity.ToTable("BillInfo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.IdBill).HasColumnName("idBill");
            entity.Property(e => e.IdDrink).HasColumnName("idDrink");

            entity.HasOne(d => d.IdBillNavigation).WithMany(p => p.BillInfos)
                .HasForeignKey(d => d.IdBill)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillInfo__count__5CD6CB2B");

            entity.HasOne(d => d.IdDrinkNavigation).WithMany(p => p.BillInfos)
                .HasForeignKey(d => d.IdDrink)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillInfo__idDrin__5DCAEF64");
        });

        modelBuilder.Entity<Drink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Drink__3213E83FD76534B9");

            entity.ToTable("Drink");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValue("Chua dat ten")
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Drinks)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Drink__price__5441852A");
        });

        modelBuilder.Entity<DrinkCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DrinkCat__3213E83F935931F2");

            entity.ToTable("DrinkCategory");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValue("Chua dat ten")
                .HasColumnName("name");
        });

        modelBuilder.Entity<TableDrink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TableDri__3213E83F0EBD23FD");

            entity.ToTable("TableDrink");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasDefaultValue("Chua dat ten")
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasDefaultValue("Trong")
                .HasColumnName("status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
