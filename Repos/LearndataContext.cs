using LearnAPI.Modal;
using LearnAPI.Repos.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnAPI.Repos;


// This class represents the database context for the application
// It derives from DbContext provided by Entity Framework Core
public partial class LearndataContext : DbContext
{
    // Default constructor
    public LearndataContext()
    {
    }


    // Constructor that accepts DbContextOptions to configure the context
    public LearndataContext(DbContextOptions<LearndataContext> options)
        : base(options)
    {
    }

    // DbSet properties represent collections of the specified entities in the database
    public virtual DbSet<TblCustomer> TblCustomers { get; set; }

    public virtual DbSet<TblMenu> TblMenus { get; set; }

    public virtual DbSet<TblOtpManager> TblOtpManagers { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblProductimage> TblProductimages { get; set; }

    public virtual DbSet<TblPwdManger> TblPwdMangers { get; set; }

    public virtual DbSet<TblRefreshtoken> TblRefreshtokens { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblRolepermission> TblRolepermissions { get; set; }

    public virtual DbSet<TblTempuser> TblTempusers { get; set; }


    public virtual DbSet<TblUser> TblUsers { get; set; }
    // This DbSet represents a custom model that doesn't directly map to a database table
    public virtual DbSet<Customermodal> customerdetail { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuring the entity TblTempuser
        modelBuilder.Entity<TblTempuser>(entity =>
        {
            // Defining the primary key for TblTempuser
            entity.HasKey(e => e.Id).HasName("tbl_tempuser1");
        });

        // Call the partial method to allow further customization
        OnModelCreatingPartial(modelBuilder);
    }

    // Partial method for additional configuration, if needed
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
